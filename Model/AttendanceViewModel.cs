using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class AttendanceViewModel
    {
        public List<AttendanceModel> AttendanceModels { get; set; }
        [ValidateNever] public SectionModel Section { get; set; }
    }
}


