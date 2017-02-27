﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SyemaTest.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        [Required]
        public int Email { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}