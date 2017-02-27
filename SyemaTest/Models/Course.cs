using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SyemaTest.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual  Department Department { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Department> Departments { get; set; }

    }
}