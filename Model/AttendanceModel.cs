using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{

    public class AttendanceModel
    {
        /*
         * PK
        */
        public int Id { get; set; }

        /*
         * Time at attendance was marked
        */
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Attendance Date")]
        public DateTime AttendanceDate { get; set; }

        /*
         * Returns True if student were present
        */
        public bool IsPresent { get; set; }

        /*
         * Foreign key of 'student' table
        */
        [Required]
        public int StudentId { get; set; }

        /*
         * Navigation Property for 'student' 
        */
        public StudentModel? Student { get; set; }
    }
}
