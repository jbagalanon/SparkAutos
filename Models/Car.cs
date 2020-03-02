using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace SparkAuto.Models
{
    public class Car
    {

        [Key]
        public int  Id { get; set; }

        [Required]
        public string VIN { get; set; }

        [Required]
        public string Make { get; set; }
        public string Model { get; set; }
        public string Style { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public double Miles { get; set; }
        public string Color { get; set; }


        //Initialize the FK here

        public string UserID { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
