namespace ManageBook.Models
{
    public class FinancialReport
    {
        public int ReportId { get; set; }
        public string ReportType { get; set; }  // 'Sales', 'Expenses'
        public decimal TotalAmount { get; set; }
        public DateTime ReportDate { get; set; }
    }

}
