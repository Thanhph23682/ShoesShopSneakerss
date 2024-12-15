// APPDATA/Models/BillDetails.cs
namespace APPDATA.Models
{
    public class BillDetails
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public virtual Bill? Bills { get; set; }
        public virtual Products? Product { get; set; }
    }
}
