using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class Stud_Course
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        public int CourseId { get; set; }

        
        public string Grade { get; set; }
    }
}
