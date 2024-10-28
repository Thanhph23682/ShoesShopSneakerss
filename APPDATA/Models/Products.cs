using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public string? NameProduct { get; set; }
        public int Price { get; set; }
        public string? Desciption { get; set; }

        public DateTime CreateDate { get; set; }
        public string? Thumbnail { get; set; }
        public string? ImagePath { get; set; }
        public string? Alias { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Brand? Brand { get; set; }
        public ICollection<ProductVariant>? ProductVariants { get; set; }
        public ICollection<CartItems>? CartItems { get; set; }


    }

}
