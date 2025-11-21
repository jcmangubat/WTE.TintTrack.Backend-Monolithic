using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

namespace WTE.TintTrack.Business.Domain.Extensions;

public static class InvoiceExtensions
{

    /// <summary>
    /// Method to check if the invoice is fully paid (without computed fields)
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    public static bool CheckIfFullyPaid(this Invoice invoice)
    {
        // Calculate the total balance to be paid
        var totalAmount = invoice.Subtotal + invoice.TaxAmount - invoice.DiscountAmount + invoice.Adjustments.Sum(a => a.AdjustmentAmount) - invoice.CreditMemos.Sum(c => c.CreditAmount) + invoice.LateFees.Sum(l => l.LateFeeAmount);

        // Calculate the total payments made so far
        var totalPayments = invoice.Payments.Sum(p => p.Amount);

        // Set IsPaid flag based on whether the invoice has been fully paid
        var IsPaid = totalPayments >= totalAmount;

        // You can also set the PaidDate if fully paid
        /*if (IsPaid && !invoice.PaidDate.HasValue)
            invoice.PaidDate = DateTime.Now;*/

        return IsPaid;
    }
}
