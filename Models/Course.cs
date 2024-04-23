namespace Cloud_MVC_Tutorial.Models
{//namespace begin
    public class Course
    {//Course class begin
        public int CourseId { get; set; } //primary key
        public string? CourseName { get; set; }
        public string? CourseDescription { get; set; }

        public List<Enrollment>? Enrollments { get; set; }
        //linking to enrollment table - for navigation
    }//Course class end
}//namespace end
