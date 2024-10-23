using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class FinancialReport
{
    public int ReportId { get; set; }

    public string ReportType { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public DateTime? ReportDate { get; set; }
}
