using BLL.Util;
using DAL.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using StudentAttendanceSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public static class StudentsRepository
    {
        public static List<StudentModel>? GetStudentsIncludingSections(
            this ApplicationDBContext context, int secId)
        {
            var res = context.Students
                .Where(x => x.SectionId == secId)
                .Include(s => s.Section).ToList();
            return res;
        }

        public static StudentModel? GetStudentById(this ApplicationDBContext context, int? stdId)
        {
            if (stdId == null || stdId == 0) return null;
            return context.Students.Find(stdId);
        }

        public static DBUpdateStatus CreateStudent(this ApplicationDBContext context, StudentModel student)
        {
            try { 
                context.Students.Add(student);
                context.SaveChanges();
            }
            catch (DbUpdateException exc) { return exc.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }

        public static DBUpdateStatus UpdateStudent(this ApplicationDBContext context, StudentModel student)
        {
            try{ 
                context.Students.Update(student);
                context.SaveChanges();
            }
            catch (DbUpdateException exc) { return exc.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }

        public static DBUpdateStatus DeleteStudent(this ApplicationDBContext context, StudentModel model)
        {
            try { 
                context.Remove(model);
                context.SaveChanges();
            } 
            catch(DbUpdateException exc) { return exc.GetExceptionStatus(); }
            return DBUpdateStatus.Success;
        }
    }
}
