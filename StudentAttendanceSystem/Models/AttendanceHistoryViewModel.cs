using StudentAttendanceSystem.Util;

namespace StudentAttendanceSystem.Models
{
    public class AttendanceHistoryViewModel
    {
        public SectionModel Section { get; set; }
        public List<AttendanceGroup> Attendances { get; set; }
    }
}

