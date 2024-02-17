using BLL.Util;
using DAL.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
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
            try { context.Sections.Add(model); }
            catch (DbUpdateException ex) { return ex.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }

        public static DBUpdateStatus UpdateSection(
            this ApplicationDBContext context, 
            SectionModel model
        )
        {
            try { context.Sections.Update(model); } 
            catch (DbUpdateException ex) { return ex.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }

        public static DBUpdateStatus DeleteSection(
            this ApplicationDBContext context,
            SectionModel model
        )
        {
            try { context.Sections.Remove(model); }
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
    }
}
