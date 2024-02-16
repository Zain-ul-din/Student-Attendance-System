using Models.Helpers;

namespace Models
{
    
    public class AttendanceHistoryViewModel
    {
        public SectionModel Section { get; set; }
        public List<AttendanceGroup> Attendances { get; set; }
    }
}

