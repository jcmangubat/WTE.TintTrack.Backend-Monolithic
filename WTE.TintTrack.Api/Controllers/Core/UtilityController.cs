using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Common.Helpers;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Domain.Shared.SmartyStreets.ValueObjects;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// Controller for handling utility and helper operations.
/// </summary>
/// <remarks>
/// This controller provides utility endpoints for retrieving system enumerations, validating addresses, and accessing reference data. It includes endpoints for retrieving various enum types such as contact methods, customer levels, property types, tint types, project types, tenant statuses, billing profile types, user roles, subscription statuses, payment statuses, invoice statuses, and other system constants, enabling clients to access standardized reference data and perform utility operations.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UtilityController(ILogger<UtilityController> logger, IMapper mapper,
                    IMessageProviderService messageProviderService,
                    IAddressValidatorService addressValidatorService)
    : LoggingMappedControllerBase<UtilityController>(logger, mapper, messageProviderService)
{
    private readonly IAddressValidatorService _addressValidatorService = addressValidatorService;

    /// <summary>
    /// Validates a given address by sending it to the address validation service.
    /// </summary>
    /// <param name="address">The address to be validated, provided in the request body.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the validated address details if the validation is successful. 
    /// Returns a 200 OK response with the validated address data in JSON format.
    /// </returns>
    /// <remarks>
    /// This endpoint uses an HTTP POST request to send an address to the address validation service.
    /// It validates the address using <see cref="_addressValidatorService"/> and returns the result.
    /// </remarks>
    [HttpPost("validate-address")]
    public async Task<IActionResult> ValidateAddress([FromBody] Address address)
    {
        var validatedAddress = await _addressValidatorService.ValidateAddressAsync(address);
        return Ok(validatedAddress);
    }

    /// <summary>
    /// Retrieves the available contact methods for leads.
    /// </summary>
    /// <returns>A dictionary of lead sources with their respective integer values.</returns>
    [HttpGet("contact-methods")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetLeadSources() => GetEnumDetails<LeadSourcesEnum>();

    /// <summary>
    /// Retrieves the available customer levels or statuses.
    /// </summary>
    /// <returns>A dictionary of customer statuses with their respective integer values.</returns>
    [HttpGet("customer-levels")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetCustomerStatuses() => GetEnumDetails<CustomerStatusEnum>();

    /// <summary>
    /// Retrieves the available customer contact relationship types.
    /// </summary>
    /// <returns>A dictionary of customer contact relationship types with their respective integer values.</returns>
    [HttpGet("customer-contact-relationship-types")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetCustomerContactRelationshipTypes() => GetEnumDetails<CustomerContactRelationshipTypesEnum>();

    /// <summary>
    /// Retrieves the available property types.
    /// </summary>
    /// <returns>A dictionary of property types with their respective integer values.</returns>
    [HttpGet("property-types")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetPropertyTypes() => GetEnumDetails<PropertyTypesEnum>();

    /// <summary>
    /// Retrieves the available tint types.
    /// </summary>
    /// <returns>A dictionary of tint types with their respective integer values.</returns>
    [HttpGet("tint-types")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetTintTypes() => GetEnumDetails<TintTypesEnum>();

    /// <summary>
    /// Retrieves the available project types.
    /// </summary>
    /// <returns>A dictionary of project types with their respective integer values.</returns>
    [HttpGet("project-types")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetProjectTypes() => GetEnumDetails<ProjectTypesEnum>();

    /// <summary>
    /// Retrieves the available invitation sources.
    /// </summary>
    /// <returns>A dictionary of invitation sources with their respective integer values.</returns>
    [HttpGet("invitation-sources")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetInvitationSources() => GetEnumDetails<InvitationSourcesEnum>();

    /// <summary>
    /// Retrieves the available active inclusion options.
    /// </summary>
    /// <returns>A dictionary of active inclusion options with their respective integer values.</returns>
    [HttpGet("ActiveInclusionOptions")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetActiveInclusionOptions() => GetEnumDetails<ActiveInclusionOptionsEnum>();

    /// <summary>
    /// Retrieves the available tenant statuses.
    /// </summary>
    /// <returns>A dictionary of tenant statuses with their respective integer values.</returns>
    [HttpGet("TenantStatuses")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetTenantStatuses() => GetEnumDetails<TenantStatusEnum>();

    /// <summary>
    /// Retrieves the available billing profile types.
    /// </summary>
    /// <returns>A dictionary of billing profile types with their respective integer values.</returns>
    [HttpGet("BillingProfileTypes")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetBillingProfileTypes() => GetEnumDetails<BillingProfileTypesEnum>();

    /// <summary>
    /// Retrieves the available user roles.
    /// </summary>
    /// <returns>A dictionary of user roles with their respective integer values.</returns>
    [HttpGet("UserRoles")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetUserRoles() => GetEnumDetails<UserRolesEnum>();

    /// <summary>
    /// Retrieves the available subscription statuses.
    /// </summary>
    /// <returns>A dictionary of subscription statuses with their respective integer values.</returns>
    [HttpGet("SubscriptionStatuses")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetSubscriptionStatuses() => GetEnumDetails<SubscriptionStatusEnum>();

    /// <summary>
    /// Retrieves the available billing cycles.
    /// </summary>
    /// <returns>A dictionary of billing cycles with their respective integer values.</returns>
    [HttpGet("BillingCycles")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetBillingCycles() => GetEnumDetails<BillingCyclesEnum>();

    /// <summary>
    /// Retrieves the available payment statuses.
    /// </summary>
    /// <returns>A dictionary of payment statuses with their respective integer values.</returns>
    [HttpGet("PaymentStatuses")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetPaymentStatuses() => GetEnumDetails<PaymentStatusEnum>();

    /// <summary>
    /// Retrieves the available invoice statuses.
    /// </summary>
    /// <returns>A dictionary of invoice statuses with their respective integer values.</returns>
    [HttpGet("InvoiceStatuses")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetInvoiceStatuses() => GetEnumDetails<InvoiceStatusEnum>();

    /// <summary>
    /// Retrieves the available tenant invitation statuses.
    /// </summary>
    /// <returns>A dictionary of tenant invitation statuses with their respective integer values.</returns>
    [HttpGet("TenantInvitationStatuses")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetTenantInvitationStatuses() => GetEnumDetails<TenantInvitationStatusEnum>();

    /*/// <summary>
    /// Retrieves the available contact types.
    /// </summary>
    /// <returns>A dictionary of contact types with their respective integer values.</returns>
    [HttpGet("ContactTypes")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetContactTypes() => GetEnumDetails<ContactTypesEnum>();*/

    /// <summary>
    /// Retrieves the available tax exemption reasons.
    /// </summary>
    /// <returns>A dictionary of tax exemption reasons with their respective integer values.</returns>
    [HttpGet("TaxExemptionReasons")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetTaxExemptionReasons() => GetEnumDetails<TaxExemptionReasonsEnum>();

    /// <summary>
    /// Retrieves the available recipient types.
    /// </summary>
    /// <returns>A dictionary of recipient types with their respective integer values.</returns>
    [HttpGet("RecipientTypes")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetRecipientTypes() => GetEnumDetails<RecipientTypesEnum>();

    /// <summary>
    /// Retrieves the available feature access permissions.
    /// </summary>
    /// <returns>A dictionary of feature access permissions with their respective integer values.</returns>
    [HttpGet("FeatureAccessPermissions")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetFeatureAccessPermissions() => GetEnumDetails<FeatureAccessPermissionsEnum>();

    /// <summary>
    /// Retrieves the available features.
    /// </summary>
    /// <returns>A dictionary of features with their respective integer values.</returns>
    [HttpGet("Features")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetFeatures() => GetEnumDetails<FeaturesEnum>();

    /// <summary>
    /// Retrieves the available roles for a user assigned as a member of a proposal, representing the possible actions that can be performed on the proposal.
    /// </summary>
    /// <returns>
    /// A dictionary where the keys are the names of the proposal member roles (as strings) and the values are their corresponding integer values.
    /// </returns>
    [HttpGet("OfferDocumentRecipientRoles")]
    [ProducesResponseType<DefaultApiResponse<Dictionary<string, int>>>(StatusCodes.Status200OK)]
    public IActionResult GetOfferDocumentRecipientRoles() => GetEnumDetails<OfferDocumentRecipientRolesEnum>();

    /// <summary>
    /// Retrieves the available length units.
    /// </summary>
    /// <returns>A list of length units with their respective properties.</returns>
    [HttpGet("LengthUnits")]
    [ProducesResponseType<DefaultApiResponse<IEnumerable<LengthUnit>>>(StatusCodes.Status200OK)]
    public IActionResult GetLengthUnits()
    {
        var values = Enum.GetValues(typeof(LengthUnitsEnum))
                         .Cast<LengthUnitsEnum>()
                         .Select(e => new LengthUnit
                         {
                             Name = e.ToString(),
                             Value = Convert.ToInt32(e),
                             DisplayName = e.GetDisplayName(), // Extension method to fetch Display attribute
                             ShortName = e.GetShortName() // Extension method to fetch ShortName attribute
                         })
                         .ToList();
        return CreateApiResponse(new DefaultApiResponse<List<LengthUnit>>(values));
    }

    private IActionResult GetEnumDetails<T>() where T : Enum
    {
        return CreateApiResponse(new DefaultApiResponse<Dictionary<string, int>>(
            Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(value => new
            {
                Name = Enum.GetName(typeof(T), value),
                Value = Convert.ToInt32(value)
            })
            .Where(item => item.Name != null) // Exclude null names
            .ToDictionary(item => item.Name!, item => item.Value) // Use null-forgiving operator
            ));
    }

}
