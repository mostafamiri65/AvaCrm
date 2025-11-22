namespace AvaCrm.Application.DTOs.CustomerManagement.Customers
{
    public class CustomerStatsDto
    {
		public int TotalInteractions { get; set; }
        public int InteractionsThisMonth { get; set; }
        public int TotalOrders { get; set; }
        public int OrdersThisMonth { get; set; }
        public decimal TotalValue { get; set; }
        public decimal ValueThisMonth { get; set; }
        public double SatisfactionRate { get; set; } // درصد
        public double AverageResponseTime { get; set; } // بر حسب ساعت

        // آمار مقایسه‌ای
        public int InteractionGrowth { get; set; } // درصد رشد نسبت به ماه قبل
        public int OrderGrowth { get; set; }
        public int ValueGrowth { get; set; }
	}
}