using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCore5Homework1.Models
{
    public class CourseUpdateModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int  Credits { get; set; }
        public DateTime DateModified { get; set; }
    }
}