using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SMEAppHouse.Core.CodeKits;
using SMEAppHouse.Core.CodeKits.Extensions;
using System.Data;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Helpers;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Domain.Entities;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Infrastructure;

public static class ApplicationDbContextSeed
{
    /*
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var appSettings = (serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>()).Value; 
     */

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        await SeedSubscriptionPlansAndFeaturesAsync(context);

        await SeedRoles(serviceProvider);
        await SeedGlobalAdminAccount(serviceProvider);

        await SeedUsersAsync(serviceProvider);
        await SeedTenantOrganizations(serviceProvider, context);

        await CreateUserTenantRelationships(serviceProvider, context);

        await CreateTenantSubscriptionPlans(serviceProvider, context);

        await SeedApplicationFeaturePermissions(serviceProvider, context);
    }

    private static async Task CreateTenantSubscriptionPlans(IServiceProvider serviceProvider, ApplicationDbContext context)
    {
        var defSubscriptionPlan = context.SubscriptionPlans.FirstOrDefault(p => p.Level == 0); // set to free plan
        var tenantCodes = SeedData.Tenants.Select(p => p.TenantCode);

        foreach (var tenantCode in tenantCodes)
        {
            var existingTenant = await context.Tenants.FirstOrDefaultAsync(p => p.TenantCode == tenantCode);
            var existingTenantSubPlan = await context.TenantSubscriptions.FirstOrDefaultAsync(p => p.TenantId == existingTenant.Id && p.SubscriptionPlanId == defSubscriptionPlan.Id);
            if (existingTenantSubPlan == null)
            {
                var tenantSubscription = new TenantSubscription
                {
                    Id = Guid.NewGuid(),
                    SubscriptionPlanId = defSubscriptionPlan.Id,
                    SubscriptionStatus = SubscriptionStatusEnum.Active,
                    TenantId = existingTenant.Id
                };
                await context.TenantSubscriptions.AddAsync(tenantSubscription);
            }
        }
        await context.SaveChangesAsync();
    }

    private static async Task CreateUserTenantRelationships(IServiceProvider serviceProvider, ApplicationDbContext context)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var tenantOwnderRole = await roleManager.FindByNameAsync(UserRolesEnum.TenantOwner.ToString());

        foreach (var utRef in SeedData.TenantUserReference)
        {
            var user = await context.Users.FirstOrDefaultAsync(p => p.UserCode == utRef.UserCode);
            var tenant = await context.Tenants.FirstOrDefaultAsync(p => p.TenantCode == utRef.TenantCode);

            var existingUserTenant = await context.UserTenants
                                                    .Include(p => p.User)
                                                    .Include(p => p.Tenant)
                                                    .FirstOrDefaultAsync(p => p.User.UserCode == utRef.UserCode && p.Tenant.TenantCode == utRef.TenantCode);

            if (existingUserTenant == null)
            {
                var userTenant = new UserTenant()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    TenantId = tenant.Id,
                    UserIsOwner = true,
                    IsDefault = utRef.IsDefault,
                };

                await context.UserTenants.AddAsync(userTenant);

                var userTenantRole = new UserTenantRole
                {
                    Id = Guid.NewGuid(),
                    RoleId = tenantOwnderRole.Id,
                    UserTenantId = userTenant.Id,
                };

                await context.UserTenantRoles.AddAsync(userTenantRole);

                context.SaveChanges();
            }
        }
    }

    private static async Task SeedTenantOrganizations(IServiceProvider serviceProvider, ApplicationDbContext context)
    {
        foreach (var tenant in SeedData.Tenants)
        {
            var existingTenant = await context.Tenants.FirstOrDefaultAsync(p => p.TenantCode == tenant.TenantCode);
            if (existingTenant == null)
                await context.Tenants.AddAsync(tenant);
        }
        context.SaveChanges();
    }

    private static async Task SeedUsersAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var user in SeedData.TenantOwnerUsers)
        {
            var userTenantMapItem = SeedData.TenantUserReference.FirstOrDefault(p => p.UserCode == user.UserCode);
            var existingUser = await userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
                await userManager.CreateAsync(user, userTenantMapItem.Password);
        }
    }

    private static async Task SeedGlobalAdminAccount(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var globalAdmin = await userManager.FindByEmailAsync("jc.mangubat@hotmail.com");

        if (globalAdmin == null)
        {
            globalAdmin = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserCode = CodeGenerator.GenerateUniqueCode("jc.mangubat@hotmail.com", FieldLengths.ApplicationUser.UserCode),
                //FullName = "James Mangubat",
                UserName = "jc.mangubat@hotmail.com",
                Email = "jc.mangubat@hotmail.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(globalAdmin, "_b3@rStr3ngth_");

            await userManager.AddToRoleAsync(globalAdmin, UserRolesEnum.GlobalAdmin.ToString());
        }
        else
        {
            if (string.IsNullOrEmpty(globalAdmin.UserCode))
            {
                globalAdmin.UserCode = globalAdmin.Id.GenerateIdentityCode(FieldLengths.ApplicationUser.UserCode); //appSettings.UserCodeLength);
                await userManager.UpdateAsync(globalAdmin);
            }
        }
    }

    private static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var appSettings = serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>().Value;

        // Seed roles
        var roles = Enum.GetValues<UserRolesEnum>()
                        .Cast<UserRolesEnum>()
                        .Select(r => r.ToString())
                        .ToList();

        foreach (var role in roles)
        {
            var roleName = role.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new ApplicationRole()
                {
                    Id = Guid.NewGuid(),
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
        }
    }

    private static async Task SeedSubscriptionPlansAndFeaturesAsync(ApplicationDbContext context)
    {
        var subscriptionPlanCodes = SeedData.SubscriptionPlans.Select(p => p.PlanCode);
        if (!context.SubscriptionPlans.Any(p => subscriptionPlanCodes.Any(x => x == p.PlanCode)))
            await context.SubscriptionPlans.AddRangeAsync(SeedData.SubscriptionPlans);

        var planFeatureCodes = SeedData.SubscriptionPlanFeatures.Select(p => p.FeatureCode);
        if (!context.SubscriptionPlanFeatures.Any(f => planFeatureCodes.Any(x => x == f.FeatureCode)))
            await context.SubscriptionPlanFeatures.AddRangeAsync(SeedData.SubscriptionPlanFeatures);

        await context.SaveChangesAsync();

        var plans = await context.SubscriptionPlans.ToListAsync();
        var features = await context.SubscriptionPlanFeatures.ToListAsync();

        foreach (var plan in plans)
        {
            foreach (var feature in features)
            {
                if (await context.SubscriptionPlanFeatureAssociations
                    .Include(p => p.SubscriptionPlan)
                    .Include(p => p.SubscriptionPlanFeature)
                    .AnyAsync(p => p.SubscriptionPlan.PlanCode == plan.PlanCode &&
                                    p.SubscriptionPlanFeature.FeatureCode == feature.FeatureCode))
                    continue;

                await context.SubscriptionPlanFeatureAssociations.AddAsync(new SubscriptionPlanFeatureAssociation()
                {
                    SubscriptionPlanId = plan.Id,
                    SubscriptionPlanFeatureId = feature.Id
                });
            }
        }
        await context.SaveChangesAsync();
    }

    private static async Task SeedApplicationFeaturePermissions(IServiceProvider serviceProvider, ApplicationDbContext context)
    {
        /*var featureNames = Enum.GetNames(typeof(FeaturesEnum));
        var permissionLevels = Enum.GetNames(typeof(FeatureAccessPermissionsEnum));*/

        var permissions = new List<Permission>();
        var features = EnumExt.EnumToArray<FeaturesEnum>();
        var permissionLevels = EnumExt.EnumToArray<FeatureAccessPermissionsEnum>();

        foreach (var feature in features)
        {
            foreach (var level in permissionLevels)
            {
                var permissionName = $"{feature}.{level}";
                if (await context.Permissions.AnyAsync(p => p.Name == permissionName))
                    continue;

                permissions.Add(new Permission
                {
                    Id = Guid.NewGuid(),
                    Feature = feature,
                    PermissionLevel = level,
                    Name = permissionName,
                    Description = $"Allows {level.ToString().Replace("Can", string.Empty).ToLower()} access to the {feature} feature"
                });
            }
        }

        await context.AddRangeAsync(permissions);
        await context.SaveChangesAsync();
    }

    /*private static async Task SeedApplicationFeatures(IServiceProvider serviceProvider, ApplicationDbContext context)
    {
        context.ApplicationFeatures.AddRange(
            new ApplicationFeature
            {
                Id = Guid.NewGuid(),
                Code = CodeGenerator.GenerateUniqueCode("UserManagement", FieldLengths.General.CODE),
                Name = "User Management",
                Description = "Manage users in the system.",
                ParentId = null
            },
            new ApplicationFeature
            {
                Id = Guid.NewGuid(),
                Code = CodeGenerator.GenerateUniqueCode("ProductManagement", FieldLengths.General.CODE),
                Name = "Product Management",
                Description = "Manage products in the catalog.",
                ParentId = null
            },
            new ApplicationFeature
            {
                Id = Guid.NewGuid(),
                Code = CodeGenerator.GenerateUniqueCode("ViewReports", FieldLengths.General.CODE),
                Name = "View Reports",
                Description = "Access to reporting.",
                ParentId = null
            }
        );
    }*/

    internal static class SeedData
    {
        public static SubscriptionPlan[] SubscriptionPlans => [
                new SubscriptionPlan
                {
                    Id = Guid.NewGuid(),
                    Level= 0,
                    Name = "Free",
                    PlanCode = CodeGenerator.GenerateUniqueCode("FREE", FieldLengths.SubscriptionPlan.PlanCode),
                    Price = 0m,
                    MaxUsers = 1,
                    BillingCycle= BillingCyclesEnum.Monthly,
                },
                new SubscriptionPlan
                {
                    Id = Guid.NewGuid(),
                    Level= 1,
                    Name = "Basic",
                    PlanCode = CodeGenerator.GenerateUniqueCode("BASIC", FieldLengths.SubscriptionPlan.PlanCode),
                    Price = 9.99m,
                    MaxUsers = 50,
                    BillingCycle= BillingCyclesEnum.Monthly,
                },
                new SubscriptionPlan
                {
                    Id = Guid.NewGuid(),
                    Level= 2,
                    Name = "Standard",
                    PlanCode = CodeGenerator.GenerateUniqueCode("STANDARD", FieldLengths.SubscriptionPlan.PlanCode),
                    Price = 19.99m,
                    MaxUsers = 150,
                    BillingCycle= BillingCyclesEnum.Monthly
                },
                new SubscriptionPlan
                {
                    Id = Guid.NewGuid(),
                    Level= 3,
                    Name = "Professional",
                    PlanCode = CodeGenerator.GenerateUniqueCode("PROFESSIONAL", FieldLengths.SubscriptionPlan.PlanCode),
                    Price = 29.99m,
                    MaxUsers = 500,
                    BillingCycle= BillingCyclesEnum.Monthly
                },
                new SubscriptionPlan
                {
                    Id = Guid.NewGuid(),
                    Level= 4,
                    Name = "Enterprise",
                    PlanCode = CodeGenerator.GenerateUniqueCode("ENTERPRISE", FieldLengths.SubscriptionPlan.PlanCode),
                    Price = 99.99m,
                    BillingCycle= BillingCyclesEnum.Monthly,
                    MaxUsers = null, // Unlimited users
                }
            ];

        public static SubscriptionPlanFeature[] SubscriptionPlanFeatures
        {

            get
            {
                var featureNames = new List<string> { "Basic Support", "Advanced Analytics", "Custom Branding", "Priority Support" };
                var subscriptionPlanFeatures = new SubscriptionPlanFeature[]
                {
                new ()
                {
                    Id = Guid.NewGuid(),
                    FeatureCode = $"{CodeGenerator.GenerateUniqueCode(featureNames[0], FieldLengths.SubscriptionPlanFeature.Code)}" ,
                    Name = featureNames[0],
                    Description = "Email support with a 48-hour response time",
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    FeatureCode = $"{CodeGenerator.GenerateUniqueCode(featureNames[1], FieldLengths.SubscriptionPlanFeature.Code)}" ,
                    Name = featureNames[1],
                    Description = "Detailed reports and analytics for your business",
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    FeatureCode = $"{CodeGenerator.GenerateUniqueCode(featureNames[2], FieldLengths.SubscriptionPlanFeature.Code)}" ,
                    Name = featureNames[2],
                    Description = "Customize the interface with your company branding",
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    FeatureCode = $"{CodeGenerator.GenerateUniqueCode(featureNames[3], FieldLengths.SubscriptionPlanFeature.Code)}" ,
                    Name = featureNames[3],
                    Description = "24/7 phone and email support with a 2-hour response time",
                }
                };
                return subscriptionPlanFeatures;
            }
        }

        public static ApplicationUser[] TenantOwnerUsers =>
        [
            new ApplicationUser {
                    Id = Guid.NewGuid(),
                    UserCode = CodeGenerator.GenerateUniqueCode("jorge.bonilla@gmail.com", FieldLengths.ApplicationUser.UserCode),
                    //FullName = "Jorge Bonilla",
                    UserName = "jorge.bonilla@gmail.com",
                    PhoneNumber = "+44 20 7946 0958",
                    PhoneNumberConfirmed = false,
                    Email = "jorge.bonilla@gmail.com",
                    EmailConfirmed = true
            },
            new ApplicationUser{
                    Id = Guid.NewGuid(),
                    UserCode = CodeGenerator.GenerateUniqueCode("john.doe@ouder.com", FieldLengths.ApplicationUser.UserCode),
                    //FullName = "John Doe",
                    UserName = "john.doe@ouder.com",
                    PhoneNumber = "+91-9876543210",
                    PhoneNumberConfirmed = false,
                    Email = "john.doe@ouder.com",
                    EmailConfirmed = true
            },
            new ApplicationUser{
                    Id = Guid.NewGuid(),
                    UserCode = CodeGenerator.GenerateUniqueCode("jetermulo@gmail.com", FieldLengths.ApplicationUser.UserCode),
                    //FullName = "Jay Elemar Termulu",
                    UserName = "jetermulo@gmail.com",
                    PhoneNumber = "+63-9692561601",
                    PhoneNumberConfirmed = false,
                    Email = "jetermulo@gmail.com",
                    EmailConfirmed = true
            }
        ];

        public static Tenant[] Tenants => [
                new() {
                    Id = Guid.NewGuid(),
                    TenantCode = "WTE001",
                    Name = "WindowTintsEverything",
                    Description = "Jorge's WTE",
                    TenantStatus = TenantStatusEnum.Active
                },
                new() {
                    Id = Guid.NewGuid(),
                    TenantCode = "WTE002",
                    Name = "LA Tintify",
                    Description = "Jorge's Car Tinting Business",
                    TenantStatus = TenantStatusEnum.Active
                },
                new() {
                    Id = Guid.NewGuid(),
                    TenantCode = "LA2ND",
                    Name = "LA's Second Tint Master",
                    Description = "Dust Eater Tinter",
                    TenantStatus = TenantStatusEnum.Active
                },
                new() {
                    Id = Guid.NewGuid(),
                    TenantCode = "CAWE98",
                    Name = "Chicago Awesome",
                    Description = "Tinting branch hq'ed in Chicago",
                    TenantStatus = TenantStatusEnum.Active
                },
                new() {
                    Id = Guid.NewGuid(),
                    TenantCode = "JE279",
                    Name = "Jay Elemar",
                    Description = "Jay Elemar",
                    TenantStatus = TenantStatusEnum.Active
                }
        ];

        public static IEnumerable<(string UserCode, string TenantCode, string Password, bool? IsDefault)> TenantUserReference => [
            new (TenantOwnerUsers[0].UserCode, Tenants[0].TenantCode, "Fiesta$DeFamilia01!", true),
            new (TenantOwnerUsers[0].UserCode, Tenants[1].TenantCode, "Fiesta$DeFamilia01!", false),
            new (TenantOwnerUsers[1].UserCode, Tenants[2].TenantCode, "Fiesta$DeFamilia01!", true),
            new (TenantOwnerUsers[1].UserCode, Tenants[3].TenantCode, "Fiesta$DeFamilia01!", false),
            new (TenantOwnerUsers[2].UserCode, Tenants[4].TenantCode, "Elemar1243@", null),
        ];
    }
}