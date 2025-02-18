namespace WTE.TintTrack.Common.Constants;

public static class FieldLengths
{
    public static class General
    {
        public const int CODE = 8; // can accommodate over 2.8 trillion unique records

        public const int LENGTH3 = 3;
        public const int LENGTH5 = 5;
        public const int LENGTH10 = 10;
        public const int LENGTH15 = 15;
        public const int LENGTH20 = 20;
        public const int LENGTH30 = 30;
        public const int LENGTH50 = 50;
        public const int LENGTH60 = 60;
        public const int LENGTH70 = 70;
        public const int LENGTH80 = 80;
        public const int LENGTH95 = 95;
        public const int LENGTH100 = 100;
        public const int LENGTH110 = 110;
        public const int LENGTH120 = 120;
        public const int LENGTH130 = 130;
        public const int LENGTH140 = 140;
        public const int LENGTH150 = 150;
        public const int LENGTH200 = 200;
        public const int LENGTH250 = 250;
        public const int LENGTH300 = 300;
        public const int LENGTH500 = 500;

        public const int Name = LENGTH80;
        public const int PhoneNumber = LENGTH30;
        public const int EmailAddress = LENGTH200;
        public const int URL = 2048;
        public const int CreditCard = 16;
        public const int Cvc = 4;

        public const int ExtremelyShort = LENGTH5;
        public const int ExtraShort = LENGTH10;
        public const int Short = LENGTH20;
        public const int Medium = LENGTH50;
        public const int Long = 150;
        public const int ExtraLong = LENGTH300;
        public const int SuperLong = 1000;
        public const int HyperLong = 2400;
        public const int SummaryParagraph = 800;
        public const int Password = 12;
        public const int CountryName = LENGTH50;
    }

    public static class GeneralAddress
    {
        public const int StreetAddress = 150;
        public const int AddressLine2 = 100;
        public const int City = 50;
        public const int StateOrRegionOrProvince = 50;
        public const int PostalOrZIPCode = 20;
        public const int CountryISOCode = 3;
        public const int FullAddressLength = General.ExtraLong;
    }

    public static class UserBillingProfile
    {
        public const int BillingAddress = General.LENGTH130;
        public const int BillingDetailsJson = General.SuperLong;
    }

    public static class TenantSubscriptionInvoice
    {
        public const int InvoiceNo = 14;
        public const int InvoiceCode = 15;
        public const int Currency = General.ExtraShort;
        public const int Notes = General.ExtraLong;
    }

    public static class Tenant
    {
        public const int TenantCode = General.CODE;
        public const int Name = General.Name;
        public const int Description = General.SummaryParagraph;
        public const int Email = General.EmailAddress;
        public const int ContactNumber = General.PhoneNumber;
        public const int ConnectionString = General.ExtraLong;
        public const int Domain = General.URL;
        public const int CountryOfHost = General.CountryName;
    }

    public static class TenantInvitation
    {
        public const int EmailAddress = General.EmailAddress;
        public const int FullName = General.Name;
    }

    public static class SubscriptionPlanDiscount
    {
        public const int Code = General.CODE;
        public const int Name = General.Name;
    }

    public static class SubscriptionPlanFeature
    {
        public const int Code = General.CODE;
        public const int Name = General.LENGTH20;
        public const int Description = General.ExtraLong;
    }

    public static class ApplicationUser
    {
        public const int UserCode = General.CODE;
        public const int UserName = General.EmailAddress;
        public const int FirstName = General.LENGTH20;
        public const int LastName = General.LENGTH20;
        public const int CompanyRole = 40;
        public const int ProfileImageUrl = General.URL;
        public const int TimeZone = 64;

    }



    public static class Message
    {
        public const int Subject = 60;
        public const int ContentText = General.HyperLong;
        public const int ContentHtml = General.HyperLong;
        public const int Name = General.Medium;
        public const int EmailAddress = General.EmailAddress;
        public const int MobilePhoneNo = General.PhoneNumber;
    }

    public static class SubscriptionPlan
    {
        public const int Name = General.Name;
        public const int PlanCode = General.CODE;
    }

    public static class Token
    {
        //public const int SessionCode = General.LENGTH10;
        public const int RefreshToken = 36; // Guid length
    }

    public static class Contact
    {
        public const int Code = General.CODE;
        public const int FirstName = General.LENGTH30;
        public const int LastName = General.LENGTH30;
        public const int Phone = General.PhoneNumber;
        public const int Mobile = General.PhoneNumber;
        public const int AltPhone = General.PhoneNumber;
        public const int Email = General.EmailAddress;

        public const int StreetAddress = GeneralAddress.StreetAddress;
        public const int AddressLine2 = GeneralAddress.AddressLine2;
        public const int City = GeneralAddress.City;
        public const int StateOrRegion = GeneralAddress.StateOrRegionOrProvince;
        public const int PostalCode = GeneralAddress.PostalOrZIPCode;
        public const int CountryISOCode = GeneralAddress.CountryISOCode;

        public const int Tags = General.ExtraLong;
        public const int Website = General.URL;
        public const int JobTitle = General.LENGTH30;
        public const int Notes = General.SummaryParagraph;

    }

    public static class Customer
    {
        public const int Code = 12;
        public const int Name = General.Name;
        public const int Company = General.Name;
        public const int Phone = General.PhoneNumber;
        public const int Phone2 = General.PhoneNumber;
        public const int Email = General.EmailAddress;
        public const int StreetAddress = GeneralAddress.StreetAddress;
        public const int AddressLine2 = GeneralAddress.AddressLine2;
        public const int City = GeneralAddress.City;
        public const int StateOrRegion = GeneralAddress.StateOrRegionOrProvince;
        public const int PostalCode = GeneralAddress.PostalOrZIPCode;
        public const int CountryISOCode = GeneralAddress.CountryISOCode;
        public const int CreatedBy = General.LENGTH120;
    }

    public static class Inquiry
    {
        public const int Subject = General.LENGTH80;
        public const int Details = General.SummaryParagraph;
        public const int SpecialRequests = General.SummaryParagraph;
        public const int ProposalCode = General.CODE;
        public const int SalesRepUserCode = ApplicationUser.UserCode;
    }

}
