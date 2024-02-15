using StudentAttendanceSystem.Models;

namespace StudentAttendanceSystem.Util
{
    public class AttendanceGroup
    {
        public DateTime Date { get; set; }
        public List<AttendanceModel> Attendances { get; set; }
    }
}
