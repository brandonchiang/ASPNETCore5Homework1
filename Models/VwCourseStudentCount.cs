﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ASPNETCore5Homework1.Models
{
    public partial class VwCourseStudentCount
    {
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int? StudentCount { get; set; }
    }
}