using AutoMapper;
using System.Text;
using System.Text.Json;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Domain.Entities;
using SMEAppHouse.Core.CodeKits.Encryptions;
using WTE.TintTrack.Domain.Shared.BillingProfile.Abstractions;

namespace WTE.TintTrack.Core.Application.Mappings;

public class CustomMappingProfileForUserBilling : Profile
{
    private static string _secretKey = "_r!deTheDIGITALW@t3rBOAT_";

    public CustomMappingProfileForUserBilling()
    {
        CreateMap<UserBillingProfile, UserBillingProfileDto>()
            .ForMember(dest => dest.UserCode, opt => opt.MapFrom(src => src.User != null ? src.User.UserCode : string.Empty))
            .ForMember(dest => dest.BillingDetails,
                       opt => opt.MapFrom(src =>
                           string.IsNullOrEmpty(src.BillingDetailsJson)
                               ? null
                               : DeserializeAndDecrypt<IBillingDetails>(src.BillingDetailsJson)));

        CreateMap<UserBillingProfileDto, UserBillingProfile>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.BillingDetailsJson,
                       opt => opt.MapFrom(src =>
                           src.BillingDetails == null
                               ? null
                               : EncryptAndSerialize(src.BillingDetails)));
    }

    private static T? DeserializeAndDecrypt<T>(string encryptedJson)
    {
        var decryptedJson = Cryptor.DecryptStringMD5(encryptedJson, _secretKey);
        return JsonSerializer.Deserialize<T>(decryptedJson);
    }

    private static string EncryptAndSerialize(IBillingDetails billingDetails)
    {
        var jsonString = JsonSerializer.Serialize(billingDetails);
        return Cryptor.EncryptStringMD5(jsonString, _secretKey);
    }

    private static string Encrypt(string plainText)
        => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    private static string Decrypt(string cipherText)
        => Encoding.UTF8.GetString(Convert.FromBase64String(cipherText));
}