using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentAttendanceSystem.Models
{
    public class AttendanceViewModel
    {
        public List<AttendanceModel> AttendanceModels { get; set; }
        [ValidateNever] public SectionModel Section { get; set; }
    }
}


