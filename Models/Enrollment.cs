namespace Cloud_MVC_Tutorial.Models
{//namespace begin
    public class Enrollment
    {//Enrollment class begin
        public int EnrollmentId { get; set; }

        public int? CourseId { get; set; }

        public int? StudentId { get; set; }

        // Navigation properties

        //foreign key linkage to other tables
        public Course? Course { get; set; }

        public Student? Student { get; set; }
    }//Enrollment class end
}//namespace end
