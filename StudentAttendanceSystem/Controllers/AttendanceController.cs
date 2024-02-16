using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Helpers;
using StudentAttendanceSystem.Data;


namespace StudentAttendanceSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDBContext _db;
        public AttendanceController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(int secId)
        {
            var section = _db.Sections
                .Include(s => s.Class)
                .FirstOrDefault(s => s.Id == secId);

            if (section == null) return NotFound();
            
            ViewBag.SecId = secId;

            var attendances = _db.Attendances
                .Include(a => a.Student)
                .Where(a =>  a.Student.SectionId == secId) 
                .GroupBy(a => a.AttendanceDate.Date)
                .Select(group => new AttendanceGroup {
                    Date = group.Key.Date,
                    Attendances = group.ToList()
                })
                .OrderByDescending(group => group.Date)
                .ToList();

            var model = new AttendanceHistoryViewModel
            {
                Section = section,
                Attendances = attendances
            };

            return View(model);
        }

        public IActionResult Create(int secId)
        {
            if (secId == 0 || secId == null) return NotFound();
            ViewBag.SecId = secId;
            var model = _db.Sections
                .Include(s => s.Class)
                .Include(s => s.Students)
                .FirstOrDefault(s => s.Id == secId);

            if(model == null) return NotFound();

            var attendanceModels = model.Students.Select(s => new AttendanceModel { StudentId = s.Id, IsPresent = false }).ToList();
            var viewModel = new AttendanceViewModel
            {
                Section = model,
                AttendanceModels = attendanceModels
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(AttendanceViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            foreach (var attendance in model.AttendanceModels)
            {
                // add server timestamp
                attendance.AttendanceDate = DateTime.Now;
                _db.Attendances.Add(attendance);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", new { secId = model.Section.Id });
        }

        public IActionResult Edit(int slug)
        {
            if(slug == null || slug == 0) return NotFound();
            var model = _db.Attendances.Find(slug);
            model.Student = _db.Students.FirstOrDefault(s => s.Id == model.StudentId);
            if(model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AttendanceModel model)
        {
            if (!ModelState.IsValid) return View();
            var student = _db.Students.FirstOrDefault(s => s.Id == model.StudentId);
            if(student == null) return NotFound();
                        
            _db.Update(model);
            _db.Entry(model).Property(p => p.AttendanceDate).IsModified = false;
            _db.SaveChanges();

            return RedirectToAction("Index", new { secId = student.SectionId });
        }

        public IActionResult Delete(int slug)
        {
            if (slug == null || slug == 0) return NotFound();
            var model = _db.Attendances.Find(slug);
            if (model == null) return NotFound();
            model.Student = _db.Students
                .FirstOrDefault(s => s.Id == model.StudentId);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(AttendanceModel model)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == model.StudentId);
            if (student == null) return NotFound();

            _db.Remove(model);
            _db.SaveChanges();

            return RedirectToAction("Index", new { secId = student.SectionId });
        }
    }
}

