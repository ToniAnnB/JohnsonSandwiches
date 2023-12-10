namespace JSandwiches.MVC.Models.ViewModels
{
    public class SummaryVM
    {
        public string TopSeller { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public int numMonthlyRevenue { get; set; }
        public decimal DailyRevenue { get; set; }
        public int numDailyRevenue { get; set; }
    }
}
