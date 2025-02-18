using WTE.TintTrack.Domain.Shared.SmartyStreets;
using WTE.TintTrack.Domain.Shared.SmartyStreets.ValueObjects;

namespace WTE.TintTrack.Application.Shared.Interfaces;

public interface IAddressValidatorService
{
    Task<ValidatedAddress> ValidateAddressAsync(Address address);
}