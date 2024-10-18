using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public string? images { get; set; }
        public string? Alias { get; set; }
        public int status { get; set; }
        public virtual ICollection<Products>? products { get; set; }
    }
}
