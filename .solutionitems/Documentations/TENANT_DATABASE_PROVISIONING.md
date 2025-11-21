# Tenant Database Provisioning

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes the automatic tenant database provisioning system that ensures new tenants receive fully functional databases when they subscribe to the platform.

## Architecture

The tenant database provisioning system consists of three main components:

1. **TenantDatabaseCreator** - Creates and migrates tenant databases
2. **TenantService.ApproveTenantAsync** - Triggers database creation during tenant approval
3. **TenantMigrationService** - Handles migrations and can create databases if missing

## Components

### 1. TenantDatabaseCreator

**Location:** `_Common/WTE.TintTrack.Integration/TenantDatabaseCreator.cs`

**Purpose:** Creates tenant databases and applies all migrations to ensure schema is up to date.

**Key Features:**
- Uses `EnsureCreatedAsync()` to create the database if it doesn't exist
- Applies all pending migrations using `MigrateAsync()`
- Comprehensive logging for troubleshooting
- Proper error handling with detailed exception messages

**Usage:**
```csharp
var connectionString = appSettings.TenantConnStrTemplate
    .Replace("{TENANTCODE}", tenantCode);

await databaseCreator.CreateDatabaseAsync(connectionString);
```

**Process:**
1. Creates database if it doesn't exist
2. Checks for pending migrations
3. Applies all pending migrations
4. Logs each step for observability

### 2. Tenant Approval Integration

**Location:** `_Core/WTE.TintTrack.Core.Application/Services/TenantService.cs`

**Method:** `ApproveTenantAsync(string tenantCode, bool force = false)`

**Process Flow:**
1. Validates tenant code
2. Retrieves tenant and subscription information
3. **Verifies subscription payment** ← CRITICAL STEP
   - Checks if subscription has at least one fully paid invoice
   - Verifies total successful payments >= invoice amount (including late fees)
   - Throws `ServiceOperationException` if payment not verified
4. **Creates and provisions tenant database** ← Only after payment verified
5. Activates subscription
6. Activates tenant

**Payment Verification:**
- Method: `VerifySubscriptionPaymentAsync(TenantSubscription subscription)`
- Loads subscription with invoices and payments using `GetByIdWithInvoicesAndPaymentsAsync()`
- Checks each invoice for successful payments
- Calculates total due (invoice amount + late fees)
- Returns `true` if at least one invoice is fully paid
- Comprehensive logging for audit trail

**Database Provisioning:**
- Builds connection string from `TenantConnStrTemplate`
- Calls `TenantDatabaseCreator.CreateDatabaseAsync()`
- Handles errors gracefully with rollback capability
- Logs all operations for audit trail

**Error Handling:**
- Payment verification failures prevent approval with clear error message
- Database provisioning failures are caught and wrapped in `ServiceOperationException`
- Prevents tenant activation if payment not verified or database creation fails
- Maintains data consistency

### 3. TenantMigrationService Enhancement

**Location:** `WTE.TintTrack.Api/Helpers/TenantMigrationService.cs`

**Method:** `MigrateTenantAsync(string tenantCode)`

**Enhancement:**
- Now creates database if it doesn't exist before migrating
- Handles missing databases gracefully
- Provides detailed error messages

**Process:**
1. Checks database connectivity
2. If database doesn't exist, creates it using `EnsureCreatedAsync()`
3. Applies pending migrations
4. Returns detailed result with success/failure status

## Tenant Subscription Flow

### Complete Flow

1. **User Registration** (`AccountController.Register`)
   - User provides tenant name
   - Tenant entity created with status `PendingApproval`
   - Subscription created with status `ForReview`
   - User account created
   - User-Tenant relationship established

2. **Invoice Creation** (`TenantSubscriptionInvoiceController.AddInvoice`)
   - Admin or system creates invoice for subscription
   - Invoice amount includes subscription plan cost
   - Invoice status set to appropriate status (e.g., `Pending`, `Unpaid`)

3. **Payment Processing** (`TenantSubscriptionPaymentController`)
   - Payment is recorded with `PaymentStatusEnum.Successful`
   - Payment amount is tracked against invoice
   - Multiple payments can be applied to a single invoice (partial payments)

4. **Admin Approval** (`TenantController.ApproveTenant`)
   - Admin calls approval endpoint
   - `TenantService.ApproveTenantAsync()` is invoked
   - **Payment verification occurs first** ← CRITICAL
     - System checks if subscription has at least one fully paid invoice
     - Verifies total successful payments >= invoice amount (including late fees)
     - If payment not verified, approval fails with clear error message
   - **Database is automatically created and migrated** ← Only after payment verified
   - Subscription status changes to `Active`
   - Tenant status changes to `Active`

5. **Tenant Ready**
   - Tenant database is fully provisioned
   - All migrations applied
   - Tenant can immediately start using the system

## Configuration

### Connection String Template

The system uses `ApplicationSettings.TenantConnStrTemplate` to build tenant-specific connection strings:

```json
{
  "ApplicationSettings": {
    "TenantConnStrTemplate": "Server=YOUR_SERVER;Database=WTE.TintTrackCRM.{TENANTCODE}-ENV;..."
  }
}
```

The `{TENANTCODE}` placeholder is replaced with the actual tenant code during database creation.

### Database Naming Convention

- **Development:** `WTE.TintTrackCRM.{TENANTCODE}-DEV`
- **Staging:** `WTE.TintTrackCRM.{TENANTCODE}-STAGING`
- **Production:** `WTE.TintTrackCRM.{TENANTCODE}-PROD`

## Error Handling

### Payment Verification Failures

If payment is not verified during tenant approval:
- `ServiceOperationException` is thrown with clear error message
- Error message: "Subscription payment must be verified before tenant approval"
- Tenant approval is **not completed**
- Tenant remains in `PendingApproval` status
- Subscription remains in `ForReview` status
- Admin must ensure invoice is created and payment is recorded with `PaymentStatusEnum.Successful`
- Admin can retry approval after payment is verified

**Payment Verification Requirements:**
- Subscription must have at least one invoice
- At least one invoice must have successful payments totaling >= invoice amount (including late fees)
- Payments must have `PaymentStatusEnum.Successful` status

### Database Creation Failures

If database creation fails during tenant approval (after payment verification):
- Exception is caught and logged
- `ServiceOperationException` is thrown with detailed error message
- Tenant approval is **not completed**
- Tenant remains in `PendingApproval` status
- Subscription remains in `ForReview` status (payment verified but database failed)
- Admin can retry approval after fixing the database issue

### Migration Failures

If migrations fail:
- Error is logged with full exception details
- Migration result includes error message
- Tenant database may be in inconsistent state
- Manual intervention may be required

## Logging

All payment verification and database provisioning operations are logged with appropriate log levels:

- **Information:** Normal operations (payment verification, database creation, migrations applied)
- **Warning:** Non-critical issues (payment not verified, database already exists, no pending migrations)
- **Error:** Failures (payment verification errors, database creation failed, migration errors)

**Example Log Messages:**

**Successful Flow:**
```
[Information] Verifying payment for subscription {SubscriptionId} before approving tenant ABC123
[Information] Subscription {SubscriptionId} has fully paid invoice INV-001. Total due: 1000.00, Payments: 1000.00
[Information] Payment verified for subscription {SubscriptionId}. Proceeding with database provisioning for tenant ABC123
[Information] Provisioning database for tenant ABC123 during approval process
[Information] Creating tenant database...
[Information] Database created successfully. Applying migrations...
[Information] Applying 5 migration(s): InitialCreate, AddCustomers, AddOrders, ...
[Information] Successfully applied 5 migration(s) to tenant database
[Information] Successfully provisioned database for tenant ABC123
[Information] Successfully approved and activated tenant ABC123
```

**Payment Verification Failure:**
```
[Information] Verifying payment for subscription {SubscriptionId} before approving tenant ABC123
[Warning] Invoice INV-001 for subscription {SubscriptionId} is not fully paid. Total due: 1000.00, Payments: 500.00
[Warning] Subscription {SubscriptionId} does not have any fully paid invoices
[Warning] Cannot approve tenant ABC123: Subscription payment not verified. Subscription must have at least one fully paid invoice before approval.
```

## Testing

### Manual Testing

1. **Create New Tenant:**
   ```http
   POST /api/core/account/register
   {
     "email": "newtenant@example.com",
     "password": "SecurePass123!",
     "tenantName": "New Company Inc"
   }
   ```

2. **Approve Tenant:**
   ```http
   POST /api/core/tenant/{tenantCode}/approve
   ```

3. **Verify Database:**
   - Check SQL Server for new database: `WTE.TintTrackCRM.{TENANTCODE}-DEV`
   - Verify all tables exist
   - Check migration history table

### Automated Testing

Consider adding integration tests for:
- Database creation during tenant approval
- Migration application
- Error handling scenarios
- Rollback behavior

## Migration Strategy

### Initial Database Creation

When a tenant database is first created:
1. Database is created empty
2. `EnsureCreatedAsync()` creates all tables from model
3. `MigrateAsync()` applies all migrations (may be empty if EnsureCreated handled everything)
4. Migration history is tracked in `__EFMigrationsHistory` table

### Subsequent Migrations

For existing tenant databases:
1. `TenantMigrationService.MigrateTenantAsync()` checks for pending migrations
2. Applies only pending migrations
3. Updates migration history

### Bulk Migration

Use `TenantMigrationService.MigrateAllTenantsAsync()` to:
- Migrate all tenant databases at once
- Continue on error (configurable)
- Get summary of all migration results

## Best Practices

1. **Always use migrations** - Never use `EnsureCreatedAsync()` alone in production
2. **Test database creation** - Verify provisioning works in each environment
3. **Monitor logs** - Watch for database creation failures
4. **Backup before approval** - Consider backing up master database before bulk approvals
5. **Connection string security** - Use environment variables or Key Vault for production connection strings

## Troubleshooting

### Database Creation Fails

**Symptoms:**
- Tenant approval fails with database error
- Logs show "Failed to provision tenant database"

**Possible Causes:**
- SQL Server connection issues
- Insufficient permissions
- Invalid connection string template
- Database name conflicts

**Solutions:**
- Verify SQL Server is accessible
- Check SQL Server user permissions (CREATE DATABASE)
- Validate connection string template
- Ensure tenant code doesn't contain invalid characters

### Migrations Fail

**Symptoms:**
- Database created but migrations fail
- Tenant database has incomplete schema

**Solutions:**
- Check migration files for errors
- Verify migration history table exists
- Manually apply migrations if needed
- Review migration logs for specific errors

## Related Documentation

- [Tenant Migration Strategy](./TENANT_MIGRATION_STRATEGY.md)
- [Configuration Best Practices](./CONFIGURATION_BEST_PRACTICES.md)
- [Database Context Lifecycle](./DBCONTEXT_LIFECYCLE.md)

## Summary

The tenant database provisioning system ensures that:

✅ **Payment verification is mandatory** - Subscription must have fully paid invoice before approval  
✅ New tenants automatically receive databases when approved (only after payment verification)  
✅ Databases are created with proper schema (via migrations)  
✅ All migrations are applied automatically  
✅ Errors are handled gracefully with proper logging  
✅ System maintains data consistency  
✅ Business rules are enforced (payment before provisioning)  

**The backend is now ready for new tenant subscriptions with proper payment verification!**

## Business Rules

1. **Payment First:** Database provisioning only occurs after payment verification
2. **Fully Paid:** At least one invoice must be fully paid (including late fees)
3. **Successful Payments:** Only payments with `PaymentStatusEnum.Successful` are counted
4. **No Partial Activation:** Tenant cannot be activated without verified payment and successful database provisioning

