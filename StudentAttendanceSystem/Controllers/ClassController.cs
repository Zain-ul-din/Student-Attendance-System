using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;
using Models;

namespace StudentAttendanceSystem.Controllers
{
    public class ClassController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ClassController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? classId)
        {
            ClassModel? model = _db.Classes
                .Include(c => c.Sections)
                .FirstOrDefault(c => c.Id == classId);
            if (model == null) return NotFound();
            ViewBag.ClassId = classId;
            return View(model);
        }

        public IActionResult Create(int? classId) 
        { 
            ViewBag.ClassId = classId;
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(SectionModel model)
        {
           if (!ModelState.IsValid) return View();
           _db.Sections.Add(model);
           _db.SaveChanges();
           return RedirectToAction("Index", new { classId = model.ClassId });
        }

        public IActionResult Edit(int? slug)
        {
            if(slug == null || slug == 0) return NotFound();
            SectionModel? secModel =  _db.Sections.Find(slug);
            return secModel != null ?  View(secModel) : NotFound();
        }

        [HttpPost]
        public IActionResult Edit(SectionModel model)
        {
            if (!ModelState.IsValid) return View();
            _db.Sections.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index", new { classId = model.ClassId });
        }

        public IActionResult Delete(int? slug)
        {
            if (slug == null || slug == 0) return NotFound();
            SectionModel? model = _db.Sections.Find(slug);
            return model != null ? View(model) : NotFound();
        }

        [HttpPost]
        public IActionResult Delete(SectionModel model)
        {
            _db.Sections.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("Index", new { classId = model.ClassId });
        }
    }
}
