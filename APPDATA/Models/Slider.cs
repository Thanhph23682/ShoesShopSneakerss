using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Slider
    {
        [Key]
        public int SliderID { get; set; }
        public string? SliderImage { get; set; }
        public int SliderStatus { get; set; }
    }
}
