using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Helpers
{
    public class AttendanceGroup
    {
        public DateTime Date { get; set; }
        public List<AttendanceModel> Attendances { get; set; }
    }
}
