using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;
using Models;
using DAL.Repositories;
using BLL.Util;

namespace StudentAttendanceSystem.Controllers
{
    public class ClassController(ApplicationDBContext db) : Controller
    {
        public IActionResult Index(int? classId)
        {
            ClassModel? model = db.GetClassByIdIncludingSections(classId);
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
           var res = db.CreateSection(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

           if(res == DBUpdateStatus.Success)
              return RedirectToAction("Index", new { classId = model.ClassId });
    
           return View();
        }

        public IActionResult Edit(int? slug)
        {
            SectionModel? secModel = db.GetSectionById(slug);
            return secModel != null ?  View(secModel) : NotFound();
        }

        [HttpPost]
        public IActionResult Edit(SectionModel model)
        {
            if (!ModelState.IsValid) return View();
            var res = db.UpdateSection(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if (res == DBUpdateStatus.Success)
                return RedirectToAction("Index", new { classId = model.ClassId });

            return View();
        }

        public IActionResult Delete(int? slug)
        {
            SectionModel? secModel = db.GetSectionById(slug);
            return secModel != null ? View(secModel) : NotFound();
        }

        [HttpPost]
        public IActionResult Delete(SectionModel model)
        {
            var res = db.DeleteSection(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if (res == DBUpdateStatus.Success)
                return RedirectToAction("Index", new { classId = model.ClassId });

            return View();
        }
    }
}
