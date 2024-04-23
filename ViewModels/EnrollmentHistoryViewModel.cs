using Cloud_MVC_Tutorial.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace Cloud_MVC_Tutorial.ViewModels
{//namespace begin
    public class EnrollmentHistoryViewModel
    {//EnrollmentHistoryViewModel Class begin
        public List<Enrollment>? Enrollments { get; set; }

        public string? FilterCourseName { get; set; }
        public string? FilterStudentName { get; set; }
        public DateTime? FilterStartDate { get; set; }
        public DateTime? FilterEndDate { get; set; }


    }//EnrollmentHistoryViewModel Class end
}//namespace end
