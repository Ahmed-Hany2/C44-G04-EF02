using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        public int Duration { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Top_ID { get; set; }
    }
}
