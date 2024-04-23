using System.ComponentModel.DataAnnotations;

namespace Cloud_MVC_Tutorial.Models
{//namespace begin
    public class Student
    {//student class begin
        [Key] public int StudentId { get; set; } //primary key for student table
        //String is mean't to be nullable to build database 
        public string? StudentName { get; set; }
        public int? Age { get; set; }
        //Navigate to enrollments
        public List<Enrollment>? Enrollments { get; set; }
      //linking to enrolllment table

    }//student class end
}//namespace end
