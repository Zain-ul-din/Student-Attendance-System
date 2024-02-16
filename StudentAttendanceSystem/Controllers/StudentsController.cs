using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;
using Models;

namespace StudentAttendanceSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDBContext _db;

        public StudentsController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(int secId)
        {
            StudentsViewModel model = new StudentsViewModel
            {
                Section = _db.Sections.Include(s => s.Class)
                    .FirstOrDefault(m => m.Id == secId),
                Students = _db.Students.Include(s => s.Section).ToList()
            };

            return View(model);
        }

        public IActionResult Create(int secId)
        {
            ViewBag.SecId = secId;
            return View();
        }

        public IActionResult Attendance(int stdId)
        {
            var student = _db.Students.Find(stdId);

            if (student == null) return null;

            var attendances = _db
                .Attendances
                .Include(att => att.Student)
                .Where(att => att.StudentId == stdId)
                .ToList();

            var model = new StudentAttendanceViewModel { 
                Student = student ,
                Attendances = attendances
            };

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(StudentModel model)
        {
            if (!ModelState.IsValid) return View();
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index", new { secId = model.SectionId });
        }

        public IActionResult Edit(int? stdId)
        {
            if (stdId == 0 || stdId == null) return NotFound();
            StudentModel? model = _db.Students.Find(stdId);
            if(model == null) return NotFound();
            ViewBag.SecId = model.SectionId;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StudentModel model)
        {
            if (!ModelState.IsValid) return View();
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index", new {
                secId = model.SectionId
            });
        }

        public IActionResult Delete(int stdId)
        {
            if (stdId == 0 || stdId == null) return NotFound();
            StudentModel? model = _db.Students.Find(stdId);

            if(model == null) return NotFound();

            ViewBag.SecId = model.SectionId;
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(StudentModel model)
        {
            _db.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("Index", new
            {
                secId = model.SectionId
            });
        }
    }

}

