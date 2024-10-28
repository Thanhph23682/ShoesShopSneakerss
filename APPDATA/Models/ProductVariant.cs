using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class ProductVariant
    {
        [Key]
        public int ID { get; set; }
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public int ColorID { get; set; }
        public int SizeID { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? UpdateUser { get; set; }
        public virtual Color? Color { get; set; }
        public virtual Size? Size { get; set; }
        public virtual OrderDetail? OrderDetail { get; set; }
        public virtual Products? Products { get; set; }


    }
}
