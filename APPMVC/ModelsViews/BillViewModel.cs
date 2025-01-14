namespace APPMVC.ModelsViews
{
    public class BillViewModel
    {
        public int BillID { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public int Status { get; set; }
        public List<BillDetailViewModel> Details { get; set; }
    }
}
