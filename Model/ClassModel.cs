using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{

    [Index(nameof(Name), IsUnique = true)]
    public class ClassModel
    {
        /*
         * PK 
        */
        public int Id { get; set; }

        /*
         * Name of the class
        */
        [MaxLength(100)]
        [DisplayName("Class Name")]
        [Required]
        public string Name { get; set; }

        /*
         *  since a single class can have multiple sections 
        */
        public List<SectionModel>? Sections { get; set; }


        /*
         * Navigation Property for students in the class
        */
        public List<StudentModel>? Students { get; set; }


        /*
         * Navigation Property for attendance records
        */
        public List<AttendanceModel>? Attendances { get; set; }

        /*
         * Generates Seed Data
        */
        public static ClassModel[] GenerateSeedData()
        {
            return new []
            {
                new ClassModel { Id = 1, Attendances = [], Sections = [], Name = "10th Class"  },
                new ClassModel { Id = 2, Attendances = [], Sections = [], Name = "9th Class"  },
                new ClassModel { Id = 3, Attendances = [], Sections = [], Name = "7th Class"  },
                new ClassModel { Id = 4, Attendances = [], Sections = [], Name = "8th Class"  },
                new ClassModel { Id = 5, Attendances = [], Sections = [], Name = "6th Class"  },
                new ClassModel { Id = 6, Attendances = [], Sections = [], Name = "5th Class"  }
            };
        }
    }
}

/**
 * 
 * Appendix:
    https://stackoverflow.com/questions/66702898/dataannotation-indexisunique-true-on-a-column-throws-error-attribute-inde
*/

