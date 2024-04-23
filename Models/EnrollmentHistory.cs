namespace Cloud_MVC_Tutorial.Models
{//namespace begin
    public class EnrollmentHistory
    {//EnrollmentHistory Class begin
        public int EnrollmentHistoryId { get; set; }

        public int EnrollmentId { get; set; }

        public DateTime ChangeDate { get; set; }

        public Enrollment? Enrollment { get; set; }

    }//EnrollmentHistory Class end
}//namespace end
