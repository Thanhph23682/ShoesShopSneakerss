namespace APPMVC.ModelsViews
{
    public class BillData
    {
        public int? CustomerId { get; set; }
        public List<BillItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
