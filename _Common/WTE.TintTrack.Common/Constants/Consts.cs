using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WTE.TintTrack.Common.Constants;

public static class Consts
{
    public enum LeadSourcesEnum
    {
        /// <summary>
        /// The customer contacted via phone.
        /// </summary>
        Phone,

        /// <summary>
        /// The customer submitted an inquiry through the website.
        /// </summary>
        Website,

        /// <summary>
        /// The customer made an in-person visit to the business location.
        /// </summary>
        InPerson,

        /// <summary>
        /// The customer contacted via email.
        /// </summary>
        Email,

        /// <summary>
        /// The customer contacted through social media platforms.
        /// </summary>
        SocialMedia,

        /// <summary>
        /// The customer contacted through another method not covered by the above options.
        /// </summary>
        Other
    }


    public enum CustomerStatusEnum
    {
        /// <summary>
        /// Initial stage, when the customer shows interest but hasn’t yet committed.
        /// </summary>
        Prospect = 0,

        /// <summary>
        /// Prospect who has been qualified and shows potential for conversion.
        /// </summary>
        Lead = 1,

        /// <summary>
        /// Qualified lead with active engagement in sales discussions or negotiations.
        /// </summary>
        Opportunity = 2,

        /// <summary>
        /// Customer who has completed at least one transaction or contract.
        /// </summary>
        Client = 3,

        /// <summary>
        /// Client who has engaged in multiple transactions or contracts.
        /// </summary>
        RepeatClient = 4,

        /// <summary>
        /// Formerly active client who hasn’t engaged in recent business for a specific period.
        /// </summary>
        LapsedClient = 5,

        /// <summary>
        /// A client who is no longer actively engaging but may still receive communications for re-engagement.
        /// </summary>
        Inactive = 6
    }

    public enum CustomerContactRelationshipTypesEnum
    {
        /// <summary>
        /// The main contact person for a customer account.
        /// </summary>
        PrimaryContact,

        /// <summary>
        /// The contact responsible for receiving and managing billing information.
        /// </summary>
        BillingContact,

        /// <summary>
        /// A contact for handling support or service-related inquiries.
        /// </summary>
        SupportContact,

        /// <summary>
        /// An emergency contact for urgent matters.
        /// </summary>
        EmergencyContact,

        /// <summary>
        /// A technical contact, often for IT-related matters.
        /// </summary>
        TechnicalContact,

        /// <summary>
        /// A decision-maker or authority figure for the customer.
        /// </summary>
        DecisionMaker,

        /// <summary>
        /// A contact associated with contract or legal matters.
        /// </summary>
        LegalContact,

        /// <summary>
        /// A representative for sales inquiries or opportunities.
        /// </summary>
        SalesContact,

        /// <summary>
        /// A contact for industry related matters.
        /// </summary>
        ConsultancyContact,
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PropertyTypesEnum
    {
        Automotive = 1,          // Vehicles, including windshields, windows, mirrors, etc.
        Architectural = 2,       // Buildings, windows, doors, skylights, etc.
        Residential = 3,         // Specifically for residential properties (e.g., homes, apartments)
        Commercial = 4,          // Commercial properties like storefronts, office partitions, etc.
        Specialty = 5,           // Specialized glass types like smart glass, fire-resistant, etc.
        GlassFilm = 6,           // For any tinting or film applications (e.g., UV, heat-reflective)
        EnergyEfficient = 7,     // Energy-saving glass options like IGUs and Low-E coatings
        Custom = 8,              // Custom or unique glass applications (e.g., curved, colored glass)
        Signage = 9,             // Glass used for signage, branding, and displays
        Outdoor = 10,            // Glass used in outdoor settings (e.g., railings, pool fences)
        Other = 11               // For any properties that don't fall under the predefined categories
    }

    public enum TintTypesEnum
    {
        /// <summary>
        /// Standard tint type, typically the most basic and affordable option.
        /// </summary>
        Standard = 1,

        /// <summary>
        /// Ceramic tint type, offering better heat rejection and UV protection.
        /// </summary>
        Ceramic = 2,

        /// <summary>
        /// Reflective tint type, designed to reflect more light and reduce glare.
        /// </summary>
        Reflective = 3,

        /// <summary>
        /// Dyed tint type, providing a darker look and improving privacy, but offers less heat rejection compared to ceramic.
        /// </summary>
        Dyed = 4,

        /// <summary>
        /// Carbon tint type, offers high heat rejection and UV protection, providing a stylish matte finish.
        /// </summary>
        Carbon = 5,

        /// <summary>
        /// Metalized tint type, known for reflecting heat and reducing glare, typically used for more durability.
        /// </summary>
        Metalized = 6,

        /// <summary>
        /// Photochromic tint type, also known as transition tint, which adjusts its darkness based on light exposure.
        /// </summary>
        Photochromic = 7,

        /// <summary>
        /// Nano-Ceramic tint type, offering superior heat rejection and UV protection with enhanced clarity.
        /// </summary>
        NanoCeramic = 8
    }

    public enum ProjectTypesEnum
    {
        /// <summary>
        /// Automotive Window Tinting: Applying tinted film to vehicle windows.
        /// </summary>
        AutomotiveWindowTinting = 1,

        /// <summary>
        /// Residential Window Tinting: Tinting windows in homes for privacy and energy efficiency.
        /// </summary>
        ResidentialWindowTinting = 2,

        /// <summary>
        /// Commercial Window Tinting: Window tinting for office buildings and commercial spaces.
        /// </summary>
        CommercialWindowTinting = 3,

        /// <summary>
        /// Automotive Paint Protection and Tinting: Combining paint protection films and window tinting.
        /// </summary>
        AutomotivePaintProtectionAndTinting = 4,

        /// <summary>
        /// Architectural Tinting for Glass Facades: Tinting glass facades of buildings for aesthetic and energy efficiency.
        /// </summary>
        ArchitecturalTintingForGlassFacades = 5,

        /// <summary>
        /// Smart Window Tinting: Electrochromic tinting that adjusts automatically based on sunlight or temperature.
        /// </summary>
        SmartWindowTinting = 6,

        /// <summary>
        /// Window Tinting for Privacy and Security: Tinting windows to enhance privacy and prevent breakage.
        /// </summary>
        WindowTintingForPrivacyAndSecurity = 7,

        /// <summary>
        /// Sunscreen and UV Protection Tinting: Tinting designed to block harmful UV rays and protect interiors.
        /// </summary>
        SunscreenAndUVProtectionTinting = 8,

        /// <summary>
        /// Solar Control Window Films: Films to reduce solar heat gain and improve building energy efficiency.
        /// </summary>
        SolarControlWindowFilms = 9,

        /// <summary>
        /// Decorative Tinting: Tints applied primarily for aesthetic purposes, such as patterns, colors, or branding.
        /// </summary>
        DecorativeTinting = 10,

        /// <summary>
        /// Protective Coatings and Tinting for Industrial Equipment: Tinting applied to industrial equipment to reduce glare and protect sensitive components.
        /// </summary>
        ProtectiveCoatingsAndTintingForIndustrialEquipment = 11,

        /// <summary>
        /// Window Tinting for Privacy Glass in Smart Homes: Using smart glass technology for on-demand privacy in homes.
        /// </summary>
        WindowTintingForPrivacyGlassInSmartHomes = 12
    }

    public enum InvitationSourcesEnum
    {
        FromUser,
        FromTenant
    }

    public enum ActiveInclusionOptionsEnum
    {
        ALL,
        ACTIVE_ONLY,
        INACTIVE_ONLY
    }

    public enum TenantStatusEnum
    {
        Active, Inactive, Suspended,
        PendingApproval
    }

    public enum BillingProfileTypesEnum
    {
        CreditCard,
        BankTransfer,
        PayPal,
        Invoice,
        Other
    }

    public enum UserRolesEnum
    {
        /// <summary>
        /// This role is designed for personnel who manage the CRM system on a company-wide 
        /// level, overseeing multiple tenants. A GlobalAdmin would have full administrative 
        /// privileges, including managing tenant accounts, CRM configurations, and global 
        /// reporting. They can perform actions across all tenant data and resources. This 
        /// role is typically reserved for the company's highest level of CRM administrators 
        /// or IT staff.
        /// </summary>
        GlobalAdmin,

        /// <summary>
        /// This role would represent a global technical support or customer service team member who 
        /// provides assistance to tenants across multiple branches. They may need access to all tenant 
        /// data to troubleshoot issues related to the CRM, billing, or operations but would not have 
        /// administrative or management rights. Their permissions: read-only access to tenants' customer 
        /// data and job statuses, plus tools for diagnosing issues but no ability to alter global settings.
        /// </summary>
        GlobalTechSupport,

        /// <summary>
        /// This role designed for personnel managing customer accounts
        /// </summary>
        GlobalAccountMgr,

        /// <summary>
        /// Users of these role have read-only access to all tenants within the CRM system. 
        /// This is useful for executives, auditors, or stakeholders who need to monitor 
        /// tenant performance, review global metrics, or generate system-wide reports but 
        /// without making any changes. They can view tenant data but have no write or administrative 
        /// permissions.
        /// </summary>
        GlobalViewer,

        /// <summary>
        /// This role represents the highest authority within a specific tenant (e.g., a franchisee 
        /// or branch owner). They have full administrative rights over their tenant, including managing 
        /// users (such as installers, technicians, or managers), overseeing projects (like window tint 
        /// installations), handling financials, and accessing tenant-specific reports. This role can 
        /// control all aspects of the business at the tenant level, from customer management to service 
        /// delivery.
        /// </summary>
        TenantOwner,

        TenantAccountAdmin,

        /// <summary>
        /// Personnel of this role might act as a local manager for a branch within the tint business, 
        /// handling day-to-day operations. This role would involve overseeing team members (installers, 
        /// consultants, technicians), managing appointments, and perhaps handling inventory or customer 
        /// relations. The TenantManager has fewer permissions than the TenantOwner, likely focusing more on operational and less on administrative functions like billing or contracts.
        /// </summary>
        TenantSystemAdmin,

        /// <summary>
        /// In a tint business, managing the inventory of tint materials, tools, and supplies is 
        /// critical. The TenantInventoryManager would focus on tracking stock levels, ordering new 
        /// materials, and ensuring installers have what they need for their jobs.
        /// Permissions: Access to inventory management features, order placement, and stock updates.
        /// No access to customer management, CRM, or administrative settings.
        /// </summary>
        TenantInventoryManager,

        /// <summary>
        /// This role could handle tenant-level financial operations, such as invoicing, 
        /// payment collection, and financial reporting for the branch. The TenantBillingManager 
        /// might handle customer payments, refunds, and other finance-related tasks. 
        /// Permissions: Access to billing, invoicing, and payment history.Limited or no access to 
        /// installation scheduling or customer service operations.
        /// </summary>
        TenantBillingManager,

        /// <summary>
        /// Personnel of this role might handle the scheduling and coordination of installers and 
        /// technicians. This role would involve ensuring the right personnel are assigned to the right 
        /// jobs, managing work calendars, and keeping the workflow organized.
        /// Permissions: Access to job schedules, installer availability, and appointment bookings. 
        /// Limited or no access to billing or customer management.
        /// </summary>
        TenantDispatcher,

        /// <summary>
        /// Personnel of this role likely represents an external advisor or a specialized role focused 
        /// on sales, consultation, or advisory services. They may have permissions to access customer 
        /// information, offer recommendations on services (like which type of tint to use), and work 
        /// closely with customers on planning and customization, but without the ability to modify 
        /// core system settings or manage the tenant's staff.
        /// </summary>
        TenantConsultant,

        /// <summary>
        /// This role is for personnel who handle the installation of tint products at customer sites. 
        /// Their permissions would likely focus on accessing customer orders, managing installation 
        /// schedules, and updating job statuses. They may not have access to CRM features unrelated 
        /// to the installation process, like customer acquisition or financial data.
        /// </summary>
        TenantInstaller,

        /// <summary>
        /// Personnel of this role provide support for ongoing maintenance or troubleshooting related 
        /// to the tint products after installation. They might handle customer service requests, 
        /// perform repairs or maintenance, and update the CRM with job status or outcomes. Their 
        /// role is likely more limited compared to TenantInstallers, focusing strictly on technical 
        /// tasks rather than the installation itself.
        /// </summary>
        TenantTechnician,

        /// <summary>
        /// This role is for someone focused specifically on sales and customer acquisition within 
        /// a tenant (branch). The TenantSalesRep would handle lead generation, customer interactions, 
        /// and closing deals (e.g., selling tint services, upselling premium products). This role may 
        /// not require administrative permissions but should be able to create and modify customer 
        /// records, generate quotes, and book installations.
        /// Their permissions: Access to CRM for customer management, sales pipeline, and scheduling. 
        /// No access to tenant settings, installer assignments, or technical operations.
        /// </summary>
        TenantSalesRep,

        /// <summary>
        /// Personnel of this role specifically is for handling tenant-level customer inquiries, 
        /// complaints, and post-service follow-ups. The TenantCustomerService role would focus on 
        /// addressing customer issues related to tint installations, scheduling follow-ups, and 
        /// handling complaints or warranty requests.
        /// Permissions: Access to customer records, job statuses, and the ability to update service 
        /// requests, but no access to tenant-level management or administrative features.
        /// </summary>
        TenantCustomerService,

        TenantViewer,
    }

    public enum SubscriptionStatusEnum
    {
        Active, Inactive, ForReview, InReview, InProvisioning, Cancelled
    }

    public enum BillingCyclesEnum
    {
        /// <summary>
        /// The subscription is billed on a daily basis.
        /// </summary>
        Daily,

        /// <summary>
        /// The subscription is billed on a weekly basis.
        /// </summary>
        Weekly,

        /// <summary>
        /// The subscription is billed every two weeks.
        /// </summary>
        BiWeekly,

        /// <summary>
        /// The subscription is billed once a month.
        /// </summary>
        Monthly,

        /// <summary>
        /// The subscription is billed every three months.
        /// </summary>
        Quarterly,

        /// <summary>
        /// The subscription is billed every six months.
        /// </summary>
        SemiAnnually,

        /// <summary>
        /// The subscription is billed once a year.
        /// </summary>
        Annually,

        /// <summary>
        /// The subscription is billed once every two years.
        /// </summary>
        Biennially,

        /// <summary>
        /// The subscription is billed once every three years.
        /// </summary>
        Triennially
    }


    public enum PaymentStatusEnum
    {
        /// <summary>
        /// Payment has been successfully completed.
        /// </summary>
        [Description("Payment has been successfully completed.")]
        Successful,

        /// <summary>
        /// Payment has failed due to an error (e.g., insufficient funds, card declined).
        /// </summary>
        [Description("Payment has failed due to an error (e.g., insufficient funds, card declined).")]
        Failed,

        /// <summary>
        /// Payment is currently in progress and awaiting confirmation.
        /// </summary>
        [Description("Payment is currently in progress and awaiting confirmation.")]
        Pending,

        /// <summary>
        /// Payment has been canceled by the user or the system.
        /// </summary>
        [Description("Payment has been canceled by the user or the system.")]
        Canceled,

        /// <summary>
        /// Payment has been refunded back to the user.
        /// </summary>
        [Description("Payment has been refunded back to the user.")]
        Refunded,

        /// <summary>
        /// Payment is disputed and under review by the payment provider.
        /// </summary>
        [Description("Payment is disputed and under review by the payment provider.")]
        Disputed
    }

    public enum InvoiceStatusEnum
    {
        Draft,
        Issued,
        Overdue,
        Paid,
        Cancelled
    }

    /// <summary>
    /// Enum representing the possible statuses of a tenant invitation.
    /// </summary>
    public enum TenantInvitationStatusEnum
    {
        /// <summary>
        /// The invitation has been created and sent but has not yet been responded to by the recipient.
        /// </summary>
        Pending,

        /// <summary>
        /// The recipient has accepted the invitation and joined the tenant.
        /// </summary>
        Accepted,

        /// <summary>
        /// The recipient has explicitly declined the invitation.
        /// </summary>
        Declined,

        /// <summary>
        /// The invitation has passed the expiration time and is no longer valid.
        /// </summary>
        Expired,

        /// <summary>
        /// The invitation has been canceled by the sender before it was accepted or declined.
        /// </summary>
        Revoked,

        /// <summary>
        /// The invitation has been resent to the recipient after a previous failure or request.
        /// </summary>
        Resent,

        /// <summary>
        /// The invitation process has been successfully completed, and the invitee has taken all necessary actions to join.
        /// </summary>
        Completed,

        /// <summary>
        /// There was an issue sending or processing the invitation.
        /// </summary>
        Failed,

        /// <summary>
        /// The recipient has viewed the invitation but has not yet taken further action (accepted or declined).
        /// </summary>
        Viewed
    }

    /*public enum ContactTypesEnum
    {
        Customer,       // Represents a current customer
        Lead,           // Represents an inquiry or prospect
        Referral,       // Contact referred by someone else
        Subscriber,     // Someone signed up for communications
        VIP,            // High-value customers
        Partner,        // Business or individual partners
        Distributor,    // Reseller or distributor of products
        EventAttendee,  // Attendee of events or workshops
    }*/

    public enum LengthUnitsEnum
    {
        [Display(Name = "Kilometer", ShortName = "km")]
        Kilometer,
        [Display(Name = "Meter", ShortName = "m")]
        Meter,
        [Display(Name = "Centimeter", ShortName = "cm")]
        Centimeter,
        [Display(Name = "Millimeter", ShortName = "mm")]
        Millimeter,
        [Display(Name = "Micrometer", ShortName = "µm")]
        Micrometer,
        [Display(Name = "Nanometer", ShortName = "nm")]
        Nanometer,
        [Display(Name = "Mile", ShortName = "mi")]
        Mile,
        [Display(Name = "Yard", ShortName = "yd")]
        Yard,
        [Display(Name = "Foot", ShortName = "ft")]
        Foot,
        [Display(Name = "Inch", ShortName = "in")]
        Inch
    }

    public enum RecipientTypesEnum
    {

        /// <summary>
        /// The primary recipient(s) of the email.
        /// </summary>
        PRIMARY,

        /// <summary>
        /// Secondary recipients who receive a copy of the email and 
        /// whose email addresses are visible to all recipients.
        /// </summary>
        CARBONCOPY,

        /// <summary>
        /// Secondary recipients who receive a copy of the email 
        /// but whose email addresses are not visible to other recipients.
        /// </summary>
        BLINDCARBONCOPY
    }

    public static class AuthPoliciesEnum
    {
        public const string GlobalAdminPolicy = "GlobalAdminPolicy";
        public const string GlobalAdminAccountPolicy = "GlobalAdminAccountPolicy";
        public const string GlobalTechnicalSupportPolicy = "GlobalTechnicalSupportPolicy";
        public const string TenantOwnerPolicy = "TenantOwnerPolicy";
        public const string TenantBillingManagementPolicy = "TenantBillingManagementPolicy";
        public const string TenantSystemAdminPolicy = "TenantSystemAdminPolicy";
    }

    public static UserRolesEnum[] InternalRoles = [
        UserRolesEnum.GlobalAdmin,
        UserRolesEnum.GlobalAccountMgr,
        UserRolesEnum.GlobalTechSupport,
        UserRolesEnum.GlobalViewer
    ];

    public enum FeatureAccessPermissionsEnum
    {
        CanView = 0,    // can view menu representing the feature including the paged records
        CanRead = 1,    // can open and view individual records
        CanPrint = 2,   // can open, view and print or export data
        CanWrite = 3,   // can create record of the same type of record
        CanUpdate = 4,  // can modify record
        CanDelete = 5   // can delete a record
    }

    public enum FeaturesEnum
    {
        UserManagement = 0,
        Reports = 1,
        Inventory = 2,
        Billing = 3
    }

    /// <summary>
    /// Represents various tax exemption reasons.
    /// </summary>
    public enum TaxExemptionReasonsEnum
    {
        /// <summary>
        /// Not exempt from taxes.
        /// </summary>
        NotExempt,

        /// <summary>
        /// Purchases made by the federal government.
        /// </summary>
        FederalGovernment,

        /// <summary>
        /// Purchases made by a state government entity.
        /// </summary>
        StateGovernment,

        /// <summary>
        /// Purchases made by a local government entity.
        /// </summary>
        LocalGovernment,

        /// <summary>
        /// Purchases made by a recognized tribal government.
        /// </summary>
        TribalGovernment,

        /// <summary>
        /// Purchases made by charitable organizations.
        /// </summary>
        CharitableOrganization,

        /// <summary>
        /// Purchases made by religious organizations.
        /// </summary>
        ReligiousOrganization,

        /// <summary>
        /// Purchases made by educational organizations, such as schools and universities.
        /// </summary>
        EducationalOrganization,

        /// <summary>
        /// Purchases made by hospitals or healthcare providers with exempt status.
        /// </summary>
        Hospital,

        /// <summary>
        /// Purchases for resale purposes.
        /// </summary>
        Resale,

        /// <summary>
        /// Purchases covered under a direct pay permit.
        /// </summary>
        DirectPayPermit,

        /// <summary>
        /// Purchases for use in multiple jurisdictions (points of use).
        /// </summary>
        MultiplePointsOfUse,

        /// <summary>
        /// Purchases related to direct mail services or campaigns.
        /// </summary>
        DirectMail,

        /// <summary>
        /// Purchases for agricultural production purposes.
        /// </summary>
        AgriculturalProduction,

        /// <summary>
        /// Purchases for industrial production or manufacturing processes.
        /// </summary>
        IndustrialProductionOrManufacturing,

        /// <summary>
        /// Purchases made by foreign diplomats or foreign entities with exemption status.
        /// </summary>
        ForeignDiplomat,

        /// <summary>
        /// Purchases made by nonresidents or foreign entities.
        /// </summary>
        NonResidentExemption,

        /// <summary>
        /// Purchases intended for export and not used domestically.
        /// </summary>
        ExportSales,

        /// <summary>
        /// Purchases of construction materials for qualifying projects.
        /// </summary>
        ConstructionExemption,

        /// <summary>
        /// Lease or rental of qualifying equipment under exempt conditions.
        /// </summary>
        LeaseOrRentalExemption,

        /// <summary>
        /// Purchases involving interstate commerce transactions.
        /// </summary>
        InterstateCommerce,

        /// <summary>
        /// Purchases made by nonprofit healthcare providers.
        /// </summary>
        NonProfitHealthcareProvider,

        /// <summary>
        /// Purchases related to scientific research or institutions.
        /// </summary>
        ScientificInstitution,

        /// <summary>
        /// Purchases made for disaster relief efforts in officially declared areas.
        /// </summary>
        DisasterReliefEfforts,

        /// <summary>
        /// Purchases of farm machinery, tools, or supplies for agricultural purposes.
        /// </summary>
        FarmMachineryOrSupplies,

        /// <summary>
        /// Purchases of raw materials used in production or manufacturing.
        /// </summary>
        RawMaterialsExemption,

        /// <summary>
        /// Purchases of machinery and equipment used directly in production processes.
        /// </summary>
        MachineryAndEquipment,

        /// <summary>
        /// Purchases made by diplomatic missions or international organizations.
        /// </summary>
        SalesToExemptBuyers,

        /// <summary>
        /// Purchases of tools and software used in qualifying IT or software development.
        /// </summary>
        SoftwareDevelopment,

        /// <summary>
        /// Purchases of materials related to nonprofit educational programs.
        /// </summary>
        EducationalMaterials
    }

    public enum TaskStatusEnum
    {
        Pending,
        InProgress,
        Completed,
        OnHold,
        Canceled
    }

    public enum PriorityEnums
    {
        Low = 1,       // Low priority
        Medium = 2,    // Medium priority
        High = 3,      // High priority
        Critical = 4   // Critical priority
    }

    public enum UnitOfMeasuresEnum
    {
        Meters,             // Linear measurement
        SquareMeters,       // Area measurement
        Rolls,              // Tint rolls
        Feet,               // Linear measurement in feet
        SquareFeet,         // Area measurement in square feet
        Inches,             // Linear measurement in inches
        SquareInches,       // Area measurement in square inches
        Centimeters,        // Linear measurement in centimeters
        SquareCentimeters,  // Area measurement in square centimeters
        Millimeters,        // Linear measurement in millimeters
        SquareMillimeters,  // Area measurement in square millimeters
        Kilograms,          // Weight measurement (e.g., accessories or heavy items)
        Grams,              // Smaller weight measurement
        Liters,             // Volume measurement (e.g., cleaning solutions)
        Milliliters,        // Smaller volume measurement
        Packs,              // Pre-packaged materials (e.g., adhesive packs)
        Pieces,             // Individual units (e.g., squeegees or blades)
        Boxes,              // Larger packaged materials
        Sheets              // Individual sheets of tint or related material
    }

    public enum PriceCalculationTypesEnum
    {
        Standard,       // Uses MarkupPercentage
        FixedPrice,     // Uses a fixed FinalPrice instead
        DynamicFormula  // Uses a stored formula in CustomFormula
    }

    public enum PriceTypesEnum
    {
        Retail = 1,      // Standard price for individual customers
        Wholesale = 2,   // Bulk pricing for wholesalers or B2B clients
        Discounted = 3,  // Special promotional or seasonal discount
        Contract = 4,    // Pre-negotiated contract price for specific customers
        Tiered = 5,      // Price that depends on quantity purchased
        SpecialOffer = 6,// Limited-time special pricing
        Clearance = 7,   // Discounted price for clearance or end-of-life products
        Custom = 8       // Custom pricing based on special agreements
    }

    public enum TaskAssigneeRolesEnum
    {
        ProjectManager,    // Oversees the entire project or activity
        TaskOwner,         // Directly responsible for the completion of a specific task or activity
        TeamMember,        // Contributes to the task or activity under the direction of the task owner
        Approver,          // Validates and approves the task or activity before it's considered complete
        Reviewer,          // Reviews the output and provides feedback, but does not approve
        Support,           // Provides technical or logistical support to assist with task completion
        Stakeholder,       // Interested party who is impacted by the activity but not directly involved
        QualityAssurance,  // Ensures quality through testing, checks, or validation of the task's results
        SubjectMatterExpert, // Subject Matter Expert, offers advice and solves complex problems
        Admin              // Manages access, configuration, and logistics for project activities
    }

    public enum AddressTypesEnum
    {
        /// <summary>
        /// A home address where an individual resides.
        /// </summary>
        Residential,

        /// <summary>
        /// The official address of a business or organization.
        /// </summary>
        Business,

        /// <summary>
        /// The address where mail is received, which may differ from the physical location.
        /// </summary>
        Mailing,

        /// <summary>
        /// The address where packages are sent, often used for e-commerce and logistics.
        /// </summary>
        Shipping,

        /// <summary>
        /// The address associated with credit cards and financial transactions.
        /// </summary>
        Billing,

        /// <summary>
        /// The legally registered address of a company for tax and compliance purposes.
        /// </summary>
        Registered,

        /// <summary>
        /// A long-term official address for an individual, often used for legal identification.
        /// </summary>
        Permanent,

        /// <summary>
        /// A short-term address used by students, travelers, or temporary workers.
        /// </summary>
        Temporary,

        /// <summary>
        /// The address used for tax filings and official correspondence with tax authorities.
        /// </summary>
        Tax,

        /// <summary>
        /// A real-world, physical location where a person or business is based.
        /// </summary>
        Physical,

        /// <summary>
        /// A digital or remote business address, often used for mail forwarding services.
        /// </summary>
        Virtual,

        /// <summary>
        /// An address in non-urban areas where street addresses may be unavailable.
        /// </summary>
        Rural,

        /// <summary>
        /// A rented post office box used to receive mail at a post office location.
        /// </summary>
        POBox,

        /// <summary>
        /// A military address used by armed forces personnel, following APO, FPO, or DPO formats.
        /// </summary>
        Military
    }

    public enum IndustryTypesEnum
    {
        Undefined = 0, // Default value when not specified

        // Primary Industries (Raw Materials)
        Agriculture,
        Forestry,
        Fishing,
        Mining,
        OilAndGas,

        // Secondary Industries (Manufacturing & Construction)
        Manufacturing,
        Construction,

        // Tertiary Industries (Services)
        Retail,
        Wholesale,
        Transportation,
        Logistics,
        Healthcare,
        Education,
        FinancialServices,
        Insurance,
        RealEstate,
        Hospitality,
        Tourism,
        FoodAndBeverage,
        MediaAndEntertainment,
        Telecommunications,
        LegalServices,
        Consulting,
        ITAndSoftware,
        CyberSecurity,
        Engineering,
        Architecture,
        Automotive,

        // Quaternary Industries (Knowledge & Research)
        ResearchAndDevelopment,
        Biotechnology,
        Aerospace,
        RenewableEnergy,
        Nanotechnology,

        // Quinary Industries (High-Level Decision-Making)
        Government,
        NonProfit,
        MilitaryAndDefense,
        PublicAdministration
    }

    public enum GendersEnum
    {
        Male,
        Female
    }

    public enum MaritalStatusEnum
    {
        /// <summary>
        /// The individual is not married and has never been married.
        /// </summary>
        Single,

        /// <summary>
        /// The individual is legally married.
        /// </summary>
        Married,

        /// <summary>
        /// The individual was previously married but is now legally divorced.
        /// </summary>
        Divorced,

        /// <summary>
        /// The individual is legally separated but not yet divorced.
        /// </summary>
        Separated,

        /// <summary>
        /// The individual's spouse has passed away, and they have not remarried.
        /// </summary>
        Widowed,

        /// <summary>
        /// The individual is in a legally recognized domestic partnership or civil union.
        /// </summary>
        DomesticPartnership,

        /// <summary>
        /// The individual is in a long-term relationship but not legally married.
        /// </summary>
        Cohabitating,

        /// <summary>
        /// The individual's marital status is unknown or not disclosed.
        /// </summary>
        Unknown
    }

    public enum OfferDocumentTypesEnum
    {
        Quote,
        Proposal,
        Estimate
    }

    /// <summary>
    /// Represents the roles a member can have in a proposal.
    /// </summary>
    public enum OfferDocumentRecipientRolesEnum
    {
        /// <summary>
        /// A member who is actively involved in the proposal process and may not necessarily approve it.
        /// </summary>
        Observer,

        /// <summary>
        /// A member who reviews the proposal and provides feedback or suggestions; may not necessarily approve it.
        /// </summary>
        Reviewer,

        /// <summary>
        /// A member who has the authority to approve the proposal.
        /// </summary>
        Approver
    }

    public enum OfferDocumentStatusEnum
    {
        Draft = 0,
        Submitted = 1,
        UnderReview = 2,
        ReviewedWithComment = 3,
        Approved = 4,
        Rejected = 5,
        Recalled = 6,
        Expired = 7,
        Archived = 8
    }

    public enum TintServiceTypesEnum
    {
        Automotive, Residential, Commercial
    }

    public enum ProjectStatusEnum
    {
        /// <summary>
        /// Project is being prepared but not finalized yet
        /// </summary>
        Draft = 0,

        /// <summary>
        /// Work is scheduled, but not yet started
        /// </summary>
        Scheduled = 1,

        /// <summary>
        /// Work is actively being done
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Project temporarily paused (e.g., waiting for customer or materials)
        /// </summary>
        OnHold = 3,

        /// <summary>
        /// Work completed successfully
        /// </summary>
        Completed = 4,

        /// <summary>
        /// Project was cancelled before completion
        /// </summary>
        Cancelled = 5
    }

    public enum WorkOrderStatusEnum
    {
        Pending = 0,          // Created but not yet scheduled
        Scheduled = 1,        // Assigned a date and/or technician
        InProgress = 2,       // Work is currently being executed
        Paused = 3,           // Temporarily halted (e.g., awaiting materials, customer confirmation)
        Completed = 4,        // Work finished successfully
        Cancelled = 5         // Work order was cancelled before completion
    }

    public enum BillingTypesEnum
    {
        FixedPrice = 1,       // A fixed price for the entire project or milestone.
        TimeAndMaterials = 2, // Based on hours worked and materials used.
        MilestoneBased = 3,   // Billing is based on project milestones.
        Retainer = 4,         // A recurring fee paid in advance for ongoing work.
        TimeAndMaterialsWithCap = 5 // Time and Materials with a maximum cost cap.
    }

    public enum PaymentTermsEnum
    {
        Net30 = 1,           // Payment is due in full 30 days after the invoice date.
        Net60 = 2,           // Payment is due in full 60 days after the invoice date.
        Net90 = 3,           // Payment is due in full 90 days after the invoice date.
        DueOnReceipt = 4,    // Payment is due immediately upon receipt of the invoice.
        MilestonePayments = 5, // Payment is due upon the completion of specific project milestones.
        Installments = 6,     // Payments are made in installments (e.g., monthly or quarterly).
        UponCompletion = 7,   // Payment is due once the project or work is completed.
        RetainerPayment = 8,  // A pre-paid or recurring payment for ongoing services.
        UponAcceptance = 9    // Payment is due upon formal acceptance of the work or deliverables.
    }

    /// <summary>
    /// Enum representing various payment methods.
    /// </summary>
    public enum PaymentMethodsEnum
    {
        BankTransfer,
        CreditCard,
        PayPal,
        Cash,
        Check,
        Other
    }

    public enum InvoiceSignatureTypesEnum
    {
        None = 0,              // No signature required
        Manual = 1,            // Manually signed (e.g., scanned signature, uploaded image)
        DigitalEmbedded = 2,   // Signed within the app via drawing or input
        DigitalThirdParty = 3, // Signed using external e-sign providers (e.g., DocuSign, AdobeSign)
        Acknowledged = 4       // Client just acknowledged without actual signature
    }

    public enum AdjustmentTypesEnum
    {
        Discount,
        PriceCorrection,
        Other
    }

    public enum CreditMemoStatusEnum
    {
        Applied,
        Pending,
        Reversed
    }
}