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
        public int categoryID { get; set; }
        public int brandID { get; set; }
        public string nameProduct { get; set; }
        public int Price { get; set; }
        public string Desciption { get; set; }

        public DateTime CreateDate { get; set; }
        public string thumbnail { get; set; }
        public string imagePath { get; set; }
        public string Alias { get; set; }
        public virtual Category? category { get; set; }

    }

}
