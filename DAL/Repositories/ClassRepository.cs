using Microsoft.EntityFrameworkCore;
using Models;
using StudentAttendanceSystem.Data;
using BLL.Util;

namespace DAL.Repositories
{
    using Helpers;

    public static class ClassRepository
    {
        public static List<ClassModel> GetAllClasses
            (this ApplicationDBContext context)
        {
            return context.Classes.ToList<ClassModel>();
        }

        public static DBUpdateStatus AddClass(
            this ApplicationDBContext context,
            ClassModel model
        )
        {
            try
            { context.Classes.Add(model); } 
            catch (DbUpdateException ex) { return ex.GetExceptionStatus(); }
            return DBUpdateStatus.Success;
        }

        public static ClassModel? GetClassById(
            this ApplicationDBContext context,
            int? id
        )
        {
            if (id == null || id == 0) return null;
            return context.Classes.Find(id);
        }

        public static DBUpdateStatus UpdateClass(
            this ApplicationDBContext context,
            ClassModel model
        )
        {
            try { context.Classes.Update(model); }
            catch (DbUpdateException ex) { return ex.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }

        public static DBUpdateStatus DeleteClass(
            this ApplicationDBContext context,
            ClassModel model 
        )
        {
            try { context.Classes.Remove(model) ; }
            catch (DbUpdateException ex) { return ex.GetExceptionStatus(); }

            return DBUpdateStatus.Success;
        }
    }
}

