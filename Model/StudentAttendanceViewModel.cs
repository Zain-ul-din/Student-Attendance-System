namespace Models
{
    public class StudentAttendanceViewModel
    {
        public StudentModel Student { get; set; }
        public List<AttendanceModel> Attendances { get; set; }
    }
}
