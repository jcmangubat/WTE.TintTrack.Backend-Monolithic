using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Requests;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// Controller for handling tenant subscription invoice operations.
/// </summary>
/// <remarks>
/// This controller manages invoices associated with tenant subscriptions, providing comprehensive billing functionality for subscription-based services. It enables tenants to retrieve their invoices, administrators to create and manage invoices, and supports invoice tracking by invoice number. The controller integrates with user services, billing profile services, tenant subscription services, and invoice services to generate invoices, track payment status, manage invoice details including amounts, due dates, late fees, and invoice status, providing a complete invoicing solution for subscription billing.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces("application/json")]
public class TenantSubscriptionInvoiceController(
                        ILogger<TenantSubscriptionInvoiceController> logger, IMapper mapper, IMessageProviderService messageProviderService,
                        IUserService userService,
                        IUserBillingProfileService userBillingProfileService,
                        ITenantSubscriptionService tenantSubscriptionService,
                        ITenantSubscriptionInvoiceService invoiceService,
                        IValidator<CreateTenantSubscriptionInvoiceRequest> createTenantSubscriptionInvoiceRequestValidator,
IValidator<UpdateTenantSubscriptionInvoiceRequest> updateTenantSubscriptionInvoiceRequestValidator)
    : LoggingMappedControllerBase<TenantSubscriptionInvoiceController>(logger, mapper, messageProviderService)
{
    private readonly IUserService _userService = userService;
    private readonly IUserBillingProfileService _userBillingProfileService = userBillingProfileService;
    private readonly ITenantSubscriptionService _tenantSubscriptionService = tenantSubscriptionService;
    private readonly ITenantSubscriptionInvoiceService _invoiceService = invoiceService;

    private readonly IValidator<CreateTenantSubscriptionInvoiceRequest> _createTenantSubscriptionInvoiceRequestValidator = createTenantSubscriptionInvoiceRequestValidator;
    private readonly IValidator<UpdateTenantSubscriptionInvoiceRequest> _updateTenantSubscriptionInvoiceRequestValidator = updateTenantSubscriptionInvoiceRequestValidator;

    /// <summary>
    /// Retrieves a list of invoices associated with the current user's tenant subscription.
    /// </summary>
    /// <remarks>
    /// This endpoint fetches all invoices related to the tenant subscription for the user, based on their
    /// current claims. It requires the tenant and subscription plan codes from the user's claims to determine
    /// which invoices to retrieve.
    /// </remarks>
    /// <returns>
    /// Returns an <see cref="IActionResult"/> containing a <see cref="DefaultApiResponse{T}"/> with a collection
    /// of <see cref="TenantSubscriptionInvoiceResponse"/> objects and a 200 OK status on success.
    /// </returns>
    /// <response code="200">A list of invoices for the specified tenant subscription was successfully retrieved.</response>
    /// <response code="401">The user is unauthorized to perform this action.</response>
    /// <response code="404">No invoices found for the specified tenant subscription.</response>
    [HttpGet("invoices")]
    [Authorize(Policy = AuthPoliciesEnum.TenantOwnerPolicy)]
    [ProducesResponseType<IEnumerable<TenantSubscriptionInvoiceResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInvoicesAsync()
    {
        var claimsInfo = GetUserClaimsInfo();
        var tenantSubscriptionInvoicesDto = await _invoiceService.GetInvoicesByTenantSubscriptionAsync(claimsInfo.TenantCode, claimsInfo.SubscriptionPlanCode);
        var tenantSubscriptionInvoicesResponses = Mapper.Map<IEnumerable<TenantSubscriptionInvoiceResponse>>(tenantSubscriptionInvoicesDto);

        return CreateApiResponse(new DefaultApiResponse<IEnumerable<TenantSubscriptionInvoiceResponse>>(tenantSubscriptionInvoicesResponses, "Success"));
    }


    /// <summary>
    /// Retrieves a specific invoice by its invoice number.
    /// </summary>
    /// <param name="invoiceNo">The invoice number.</param>
    /// <returns>The tenant subscription invoice DTO if found; otherwise, null.</returns>
    [Authorize(Policy = AuthPoliciesEnum.TenantOwnerPolicy)]
    [HttpGet("invoiceno/{invoiceNo}")]
    [ProducesResponseType<TenantSubscriptionInvoiceResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInvoiceByInvoiceNoAsync(string invoiceNo)
    {
        var tenantSubscriptionInvoiceDto = await _invoiceService.GetInvoiceByInvoiceNoAsync(invoiceNo);
        var tenantSubscriptionInvoiceResponse = Mapper.Map<TenantSubscriptionInvoiceResponse>(tenantSubscriptionInvoiceDto);
        return CreateApiResponse(new DefaultApiResponse<TenantSubscriptionInvoiceResponse>(tenantSubscriptionInvoiceResponse, "Success"));
    }

    /// <summary>
    /// Adds a new tenant subscription invoice based on the provided request data.
    /// </summary>
    /// <param name="invoiceRequest">
    /// A <see cref="CreateTenantSubscriptionInvoiceRequest"/> object containing details of the invoice to add.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> that represents the result of the asynchronous operation:
    /// a 200 OK response containing the created <see cref="TenantSubscriptionInvoiceDto"/> upon success,
    /// or a 400 Bad Request response with validation errors if the provided invoice data is invalid.
    /// </returns>
    /// <remarks>
    /// This method requires the caller to have global administrator privileges 
    /// (defined by the <see cref="AuthPoliciesEnum.GlobalAdminPolicy"/> policy).
    /// It validates the <paramref name="invoiceRequest"/> and then creates an invoice entry associated 
    /// with the user's active billing profile and the tenant's active subscription.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{TenantSubscriptionInvoiceDto}"/> with the added invoice information.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ValidationFailureApiResponse{TenantSubscriptionInvoiceRequest}"/> if validation fails 
    /// for the provided <paramref name="invoiceRequest"/>.
    /// </response>
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
    [HttpPost]
    [ProducesResponseType<DefaultApiResponse<TenantSubscriptionInvoiceDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<CreateTenantSubscriptionInvoiceRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddInvoiceAsync([FromBody] CreateTenantSubscriptionInvoiceRequest invoiceRequest)
    {
        var validationResult = await _createTenantSubscriptionInvoiceRequestValidator.ValidateAsync(invoiceRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<CreateTenantSubscriptionInvoiceRequest>(invoiceRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        await ValidateCreateTenantSubscriptionInvoiceRequestAsync(invoiceRequest);

        if (await _invoiceService.AnyByInvoiceCodeAsync(invoiceRequest.InvoiceCode))
        {
            var validationResponse = new ValidationFailureApiResponse<CreateTenantSubscriptionInvoiceRequest>(invoiceRequest,
                new ValidationResult(new List<ValidationFailure> {
                    new() {
                        PropertyName = nameof(CreateTenantSubscriptionInvoiceRequest.InvoiceCode),
                        ErrorMessage="Invoice already exist."
                    }
                }
            ));
            return CreateApiResponse(validationResponse);
        }

        var claimsInfo = GetUserClaimsInfo();
        var activeTenantSubscriptionDto = await _tenantSubscriptionService.GetActiveSubscriptionByTenantAsync(claimsInfo.TenantCode);
        var userBillingProfileDto = await _userBillingProfileService.GetActiveBillingProfileByUserCodeAsync(claimsInfo.UserCode);

        var tenantSubscriptionInvoiceDto = Mapper.Map<TenantSubscriptionInvoiceDto>(invoiceRequest);
        tenantSubscriptionInvoiceDto.TenantSubscriptionId = activeTenantSubscriptionDto.Id;
        tenantSubscriptionInvoiceDto.BillingProfileId = userBillingProfileDto.Id;
        tenantSubscriptionInvoiceDto.InvoiceNo = await TryGetInvoiceNoAsync(claimsInfo.TenantCode);
        await _invoiceService.AddInvoiceAsync(tenantSubscriptionInvoiceDto);

        return CreateApiResponse(new DefaultApiResponse<TenantSubscriptionInvoiceDto>(tenantSubscriptionInvoiceDto, "Success"));
    }

    private async Task ValidateCreateTenantSubscriptionInvoiceRequestAsync(CreateTenantSubscriptionInvoiceRequest invoiceRequest)
    {
        //invoiceRequest
        //if(!await _userService.Any())
        // invoiceRequest.UserCode
        // invoiceRequest.TenantCode 

        /*
            public required decimal Amount { get; set; }
            public required string Currency { get; set; }
            public required DateTime DueDate { get; set; }
            public string? Notes { get; set; }
            public required InvoiceStatusEnum InvoiceStatus { get; set; }
            public decimal? LateFeeAmount { get; set; }

         */
    }

    /// <summary>
    /// Updates an existing tenant subscription invoice based on the provided request data.
    /// </summary>
    /// <param name="invoiceUpdateRequest">
    /// A <see cref="CreateTenantSubscriptionInvoiceRequest"/> object containing the updated details of the invoice.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the result of the asynchronous operation:
    /// a 200 OK response with the updated <see cref="TenantSubscriptionInvoiceDto"/> upon success,
    /// or a 400 Bad Request response if the validation of the provided invoice data fails.
    /// </returns>
    /// <remarks>
    /// This method requires the caller to have global administrator privileges 
    /// as specified by the <see cref="AuthPoliciesEnum.GlobalAdminPolicy"/> policy.
    /// It validates the <paramref name="invoiceUpdateRequest"/> before applying the updates.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{TenantSubscriptionInvoiceDto}"/> with the updated invoice information.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ValidationFailureApiResponse{TenantSubscriptionInvoiceRequest}"/> if the provided
    /// <paramref name="invoiceUpdateRequest"/> data fails validation.
    /// </response>
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
    [HttpPut]
    [ProducesResponseType<DefaultApiResponse<TenantSubscriptionInvoiceResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<UpdateTenantSubscriptionInvoiceRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateInvoiceAsync([FromBody] UpdateTenantSubscriptionInvoiceRequest invoiceUpdateRequest)
    {
        var validationResult = await _updateTenantSubscriptionInvoiceRequestValidator.ValidateAsync(invoiceUpdateRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UpdateTenantSubscriptionInvoiceRequest>(invoiceUpdateRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var existingInvoice = await _invoiceService.GetInvoiceByInvoiceNoAsync(invoiceUpdateRequest.InvoiceNo);
        if (existingInvoice == null)
        {
            var validationResponse = new ValidationFailureApiResponse<UpdateTenantSubscriptionInvoiceRequest>(invoiceUpdateRequest,
                new ValidationResult(new List<ValidationFailure> {
                    new() {
                        PropertyName = nameof(UpdateTenantSubscriptionInvoiceRequest.InvoiceNo),
                        ErrorMessage = "Invoice number to update does not exist."
                    }
                }
            ));
            return CreateApiResponse(validationResponse);
        }

        var claimsInfo = GetUserClaimsInfo();

        if (invoiceUpdateRequest.Amount.HasValue)
            existingInvoice.Amount = invoiceUpdateRequest.Amount ?? 0;

        if (!string.IsNullOrEmpty(invoiceUpdateRequest.Currency))
            existingInvoice.Currency = invoiceUpdateRequest.Currency;

        if (invoiceUpdateRequest.DueDate.HasValue)
            existingInvoice.DueDate = invoiceUpdateRequest.DueDate.Value;

        if (!string.IsNullOrEmpty(invoiceUpdateRequest.Notes))
            existingInvoice.Notes = invoiceUpdateRequest.Notes;

        if (invoiceUpdateRequest.InvoiceStatus.HasValue)
            existingInvoice.InvoiceStatus = invoiceUpdateRequest.InvoiceStatus.Value;

        if (invoiceUpdateRequest.LateFeeAmount.HasValue)
            existingInvoice.LateFeeAmount = invoiceUpdateRequest.LateFeeAmount ?? 0;

        var tenantSubscriptionInvoiceResponse = Mapper.Map<TenantSubscriptionInvoiceResponse>(existingInvoice);

        await _invoiceService.UpdateInvoiceAsync(existingInvoice);
        return CreateApiResponse(new DefaultApiResponse<TenantSubscriptionInvoiceResponse>(tenantSubscriptionInvoiceResponse, "Success"));
    }

    /// <summary>
    /// Deletes an invoice by its number.
    /// </summary>
    /// <param name="invoiceNo">The number of the invoice to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
    [HttpDelete("{invoiceNo}")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteInvoiceAsync(string invoiceNo)
    {
        await _invoiceService.DeleteInvoiceAsync(invoiceNo);
        return CreateApiResponse(new DefaultApiResponse<string>(invoiceNo, "Success"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantCode"></param>
    /// <returns></returns>
    private async Task<string> TryGetInvoiceNoAsync(string tenantCode)
    {
        var invPrefix = $"INV-{tenantCode}-";
        var lastNumSuffix = "0".PadRight(5, '0');
        var latestInvoiceDto = await _invoiceService.GetLatestInvoiceAsync(tenantCode);
        if (latestInvoiceDto != null)
            lastNumSuffix = latestInvoiceDto.InvoiceNo.Replace(invPrefix, string.Empty);
        var lastNum = int.Parse(lastNumSuffix);
        return $"{invPrefix}{lastNum.ToString().PadRight(5, '0')}";
    }
}