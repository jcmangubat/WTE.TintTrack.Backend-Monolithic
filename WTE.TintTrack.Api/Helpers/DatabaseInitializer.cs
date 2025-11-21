using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using WTE.TintTrack.Business.DataImporter;
using WTE.TintTrack.Business.DataImporter.Models;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.PropertySpecifications;
using WTE.TintTrack.Business.Infrastructure;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Helpers;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Infrastructure;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Helpers;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        try
        {
            // ApplicationDbContext Migration and Seeding
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var appSettings = serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>().Value;

            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }

            await ApplicationDbContextSeed.SeedAsync(serviceProvider);

            // TenantDbContext Migration and Seeding
            await InitializeTenantsAsync(serviceProvider, appSettings);
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();
            logger.LogError(ex, "An error occurred during migration or seeding.");
        }
    }

    private static async Task InitializeTenantsAsync(IServiceProvider serviceProvider, ApplicationSettings appSettings)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<DatabaseInitializer>>();
        var migrationService = serviceProvider.GetRequiredService<TenantMigrationService>();

        try
        {
            logger.LogInformation("Starting tenant database migrations");

            // Use TenantMigrationService to migrate all tenants dynamically
            // This will fetch tenants from ApplicationDbContext and migrate each one
            var result = await migrationService.MigrateAllTenantsAsync(continueOnError: true);

            if (result.Success)
            {
                logger.LogInformation(
                    "Tenant migrations completed successfully. Migrated {SuccessCount} tenant(s) in {Duration}ms",
                    result.SuccessCount,
                    result.Duration.TotalMilliseconds);
            }
            else
            {
                logger.LogWarning(
                    "Tenant migrations completed with errors. Success: {SuccessCount}, Failed: {FailureCount}, Duration: {Duration}ms",
                    result.SuccessCount,
                    result.FailureCount,
                    result.Duration.TotalMilliseconds);

                // Log details of failed tenants
                foreach (var kvp in result.TenantResults.Where(r => !r.Value.Success))
                {
                    logger.LogError(
                        "Failed to migrate tenant {TenantCode}: {Error}",
                        kvp.Key,
                        kvp.Value.ErrorMessage);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Fatal error during tenant database initialization");
            throw;
        }
    }

    private static async Task RunImportAsync(TenantDbContext tenantContext)
    {
        var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var csvPath = Path.Combine(assemblyFolder, "Data\\WTE001\\wiz_contacts_1109339501_1733043545_1190.csv");
        var csvCustomers = CSVDataLoader.LoadCSV<CSVContact>(csvPath);

        if (csvCustomers != null && csvCustomers.Count > 0)
        {
            foreach (var csvCust in csvCustomers)
            {
                Console.WriteLine($"\r\nChecking customer > {csvCust.Code} : {csvCust.Name}");
                try
                {
                    var customer = await tenantContext.Customers.FirstOrDefaultAsync(p => p.Code == csvCust.Code.ToString());
                    if (customer == null)
                    {
                        Console.WriteLine($"___________ Creating customer ... {csvCust.Name}");
                        customer = new Customer()
                        {
                            Id = Guid.NewGuid(),
                            Code = csvCust.Code.ToString(),
                            Name = csvCust.Name, //$"{csvCust.FirstName} {csvCust.LastName}",
                            Company = csvCust.Company,
                            Phone = csvCust.Phone,
                            Phone2 = csvCust.Phone2,
                            Email = csvCust.Email,
                            StreetAddress = csvCust.Address,
                            AddressLine2 = csvCust.Address2,
                            City = csvCust.City,
                            StateOrRegion = csvCust.State,
                            PostalCode = csvCust.ZipCode,
                            CountryISOCode = "USA",
                            CustomerStatus = CustomerStatusEnum.Client,
                            DateCreated = csvCust.CreatedAt,
                            DateModified = DateTime.Now,
                            CreatedBy = csvCust.CreatedBy,
                            IsImported = true,
                            IsActive = true
                        };
                        tenantContext.Customers.Add(customer);
                        await tenantContext.SaveChangesAsync();
                    }

                    var contactCode = CodeGenerator.GenerateUniqueCode($"{csvCust.Code}-{csvCust.Name}", FieldLengths.General.CODE);
                    var contact = await tenantContext.Contacts.FirstOrDefaultAsync(p => p.Code == contactCode);
                    if (contact == null)
                    {
                        Console.WriteLine($"___________ Creating contact ... {csvCust.FirstName} {csvCust.LastName}");
                        contact = new Contact()
                        {
                            Id = Guid.NewGuid(),
                            Code = contactCode,
                            ContactType = ContactTypesEnum.Customer,
                            FirstName = csvCust.FirstName,
                            LastName = csvCust.LastName,
                            Phone = csvCust.Phone,
                            AltPhone = csvCust.Phone2,
                            Email = csvCust.Email,
                            IsImported = true,
                            IsActive = true,
                            DateCreated = csvCust.CreatedAt,
                            DateModified = DateTime.Now,
                            CountryISOCode = "USA",
                            AddressLine2 = csvCust.Address2,
                            City = csvCust.City,
                            JobTitle = "",
                            PostalCode = csvCust.ZipCode,
                            Notes = csvCust.Message,
                            StateOrRegion = csvCust.State,
                            StreetAddress = csvCust.Address,
                            Tags = (csvCust.Tags ?? "").Split(',')
                        };
                        tenantContext.Contacts.Add(contact);
                    }

                    var customerContact = await tenantContext.CustomerContacts.FirstOrDefaultAsync(p => p.ContactId == contact.Id && p.CustomerId == customer.Id);
                    if (customerContact == null)
                    {
                        Console.WriteLine($"___________ Associating contact to customer ... {csvCust.FirstName} {csvCust.LastName}");
                        tenantContext.CustomerContacts.Add(new CustomerContact()
                        {
                            Id = Guid.NewGuid(),
                            ContactId = contact.Id,
                            CustomerId = customer.Id,
                            RelationshipType = Consts.CustomerContactRelationshipTypesEnum.PrimaryContact,
                            IsActive = true,
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,

                        });
                    }

                    if (!string.IsNullOrEmpty(csvCust.VehicleMake))
                    {
                        var propCode = CodeGenerator.GenerateUniqueCode($"{csvCust.Code}{csvCust.Name}-{csvCust.VehicleMake}-{csvCust.VehicleModel}", FieldLengths.General.CODE);
                        var autoProp = await tenantContext.CustomerProperties.FirstOrDefaultAsync(p => p.Code == propCode);

                        if (autoProp == null)
                        {
                            Console.WriteLine($"___________ Creating customer vehicle property ... {csvCust.VehicleYear} {csvCust.VehicleMake} {csvCust.VehicleModel}");

                            tenantContext.CustomerProperties.Add(new AutomotiveProperty()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = customer.Id,
                                Code = CodeGenerator.GenerateUniqueCode($"{csvCust.Code}-{csvCust.VehicleMake}-{csvCust.VehicleModel}", FieldLengths.General.CODE),
                                Name = $"{csvCust.VehicleYear}-{csvCust.VehicleMake}-{csvCust.VehicleModel}",
                                PropertyType = Consts.PropertyTypesEnum.Automotive,
                                Color = "N/A",
                                LicensePlate = "N/A",
                                Make = csvCust.VehicleMake,
                                Model = csvCust.VehicleModel,
                                Year = csvCust.VehicleYear ?? 0,
                                Description = $"{csvCust.Name} {csvCust.VehicleYear} {csvCust.VehicleMake} {csvCust.VehicleModel}",
                                Mileage = 0,
                                VIN = "N/A"
                            });
                        }
                    }

                    if (!string.IsNullOrEmpty(csvCust.Message))
                    {
                        var custInqry = await tenantContext.Inquiries.FirstOrDefaultAsync(p => p.Details == csvCust.Message);
                        if (custInqry == null)
                        {
                            Console.WriteLine($"___________ Creating customer inquiry...");
                            tenantContext.Inquiries.Add(new Inquiry
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = customer.Id,
                                ConsultationDate = csvCust.CreatedAt,
                                Subject = GenerateSubjectLine(csvCust.Message),
                                Details = csvCust.Message,
                                PropertyType = Consts.PropertyTypesEnum.Automotive,
                                LeadSource = (csvCust.CreatedBy ?? "").Contains("Window Tints") ? Consts.LeadSourcesEnum.Website : Consts.LeadSourcesEnum.InPerson,
                                SpecialRequests = csvCust.Message
                            });
                        }
                    }

                    await tenantContext.SaveChangesAsync();
                    Console.WriteLine($"___________ Done!");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }
    }

    internal static string GenerateSubjectLine(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return "No description provided";

        // Extract important words (e.g., first 10 words)
        var words = description.Split(' ');
        return string.Join(" ", words.Take(10)) + (words.Length > 10 ? "..." : string.Empty);
    }
}
