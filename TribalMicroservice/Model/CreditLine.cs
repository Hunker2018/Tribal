namespace TribalMicroservice.Model
{
    public class CreditLine
    {
        public string businessType { get; set; }
        public decimal cashBalance { get; set; }
        public decimal monthlyRevenue { get; set; }
        public decimal requestedCreditLine { get; set; }
        public DateTime requestedDate { get; set; }
        public decimal recommendedCreditLine { get; set; }
    }
}
