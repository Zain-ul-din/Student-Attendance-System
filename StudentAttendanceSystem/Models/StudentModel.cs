using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentAttendanceSystem.Models
{

    [Index(nameof(RollNumber), IsUnique = true)]
    public class StudentModel
    {
        public int Id { get; set; }

        /*
         * Student's full name
        */
        [Required]
        [DisplayName("Student Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        /*
         * Student's roll number
        */
        [Required]
        [DisplayName("Student RollNo")]
        [MaxLength(100)]
        public string RollNumber { get; set; }

        /*
         * Foreign key of 'section' table
        */
        [Required]
        public int SectionId { get; set; }

        /*
         * Navigation Property for 'section' 
        */
        public SectionModel? Section { get; set; }
    }
}
