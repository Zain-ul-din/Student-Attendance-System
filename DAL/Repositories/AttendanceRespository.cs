using BLL.Util;
using DAL.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Helpers;
using StudentAttendanceSystem.Data;

namespace DAL.Repositories
{
    public static class AttendanceRespository
    {
        public static List<AttendanceGroup> GetAttendanceOfSection(
                this ApplicationDBContext context,
                int? secId
        )
        {
            if (secId == null || secId == 0) return [];
            return context.Attendances
                .Include(a => a.Student)
                .Where(a => a.Student.SectionId == secId)
                .GroupBy(a => a.AttendanceDate.Date)
                .Select(group => new AttendanceGroup
                {
                    Date = group.Key.Date,
                    Attendances = group.ToList()
                })
                .OrderByDescending(group => group.Date)
                .ToList();
        }

        public static List<AttendanceModel> GetAttendanceOfStudent(this ApplicationDBContext context,
            int stdId
        )
        {
            return context
                .Attendances
                .Include(att => att.Student)
                .Where(att => att.StudentId == stdId)
                .ToList();
        }

        public static DBUpdateStatus CreateAttendance(
            this ApplicationDBContext context, AttendanceModel attendance
        )
        {
            // adds server timestamp
            attendance.AttendanceDate = DateTime.Now;
            try { 
                context.Attendances.Add(attendance);
                context.SaveChanges();
            } 
            catch(DbUpdateException exc) { return exc.GetExceptionStatus(); }
            return DBUpdateStatus.Success;
        }

        public static AttendanceModel? GetAttendanceById(this ApplicationDBContext context, int? id)
        {
            if(id == null | id == 0) return null;
            return context.Attendances.Find(id);
        }

        public static DBUpdateStatus UpdateAttendance(this ApplicationDBContext context, AttendanceModel model)
        {
            try 
            { 
                context.Attendances.Update(model);
                context.Entry(model).Property(p => p.AttendanceDate).IsModified = false;
                context.SaveChanges();
            } 
            catch(DbUpdateException exc) { return exc.GetExceptionStatus(); }
            return DBUpdateStatus.Success;
        }

        public static DBUpdateStatus DeleteAttendance(this ApplicationDBContext context, AttendanceModel attendance)
        {
            try { 
                context.Attendances.Remove(attendance);
                context.SaveChanges();
            } catch(DbUpdateException exc) { return exc.GetExceptionStatus(); }
            return DBUpdateStatus.Success;
        }
    }
}
