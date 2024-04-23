namespace Cloud_MVC_Tutorial.Models
{//namespace begin
    public class Enrollment
    {//Enrollment class begin
        public int EnrollmentId { get; set; }

        public int? CourseId { get; set; }

        public int? StudentId { get; set; }

        //Added two more variables
        public DateTime EnrollmentDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        // Navigation properties

        //foreign key linkage to other tables
        public Course? Course { get; set; }

        public Student? Student { get; set; }
    }//Enrollment class end
}//namespace end
