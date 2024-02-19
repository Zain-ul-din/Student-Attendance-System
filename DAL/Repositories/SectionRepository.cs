using BLL.Util;
using DAL.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using StudentAttendanceSystem.Data;

namespace DAL.Repositories
{
    public static class SectionRepository
    {
        public static DBUpdateStatus CreateSection(
            this ApplicationDBContext context,
            SectionModel model
        )
        {
            try { 
                context.Sections.Add(model);
                context.SaveChanges();
            }
            catch (DbUpdateException ex) { return ex.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }

        public static DBUpdateStatus UpdateSection(
            this ApplicationDBContext context, 
            SectionModel model
        )
        {
            try { 
                context.Sections.Update(model);
                context.SaveChanges();
            } 
            catch (DbUpdateException ex) { return ex.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }

        public static DBUpdateStatus DeleteSection(
            this ApplicationDBContext context,
            SectionModel model
        )
        {
            try { 
                context.Sections.Remove(model);
                context.SaveChanges();
            }
            catch (DbUpdateException ex) { return ex.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }

        public static SectionModel? GetSectionById(
            this ApplicationDBContext context, 
            int? id
        )
        {
            if (id == null || id == 0) return null;
            return context.Sections.Find(id);
        }

        public static SectionModel? GetSectionByIdIncludingClass(
            this ApplicationDBContext context,
            int? id 
        )
        {
            if (id == null || id == 0) return null;
            return context.Sections.Include(s => s.Class)
                    .FirstOrDefault(m => m.Id == id);
        }

        public static SectionModel? GetSectionByIdIncludingClassAndStudents(
            this ApplicationDBContext context, int? secId 
        )
        {
            if(secId == null || secId == 0) return null;
            return context.Sections
                .Include(s => s.Class)
                .Include(s => s.Students)
                .FirstOrDefault(s => s.Id == secId);
        }
    }
}
