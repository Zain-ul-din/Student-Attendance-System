using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StudentAttendanceSystem.Models
{
    public class SectionModel
    {
        /*
         * PK
        */ 
        public int Id { get; set; }

        /*
         * Section Name
        */
        [Required]
        [MaxLength(50)]
        [DisplayName("Section Name")]
        public string Name { get; set; }

        /*
         * foreign key of 'class' table
        */
        public int ClassId { get; set; }

        /*
         * Navigation Property for 'class' 
        */
        [AllowNull]
        public ClassModel? Class { get; set; }

        /*
         * Navigation Property for students in the section
        */
        public List<StudentModel>? Students { get; set; }

        /*
         * Navigation Property for attendance records
        */
        public List<AttendanceModel>? Attendances { get; set; }
    }
}


