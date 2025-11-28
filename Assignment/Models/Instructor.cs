using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class Instructor
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public decimal Salary { get; set; }

        public string Address { get; set; }

        public decimal HourRate { get; set; }
        public decimal Bonus { get; set; }

        public int Dept_ID { get; set; }
    }
}
