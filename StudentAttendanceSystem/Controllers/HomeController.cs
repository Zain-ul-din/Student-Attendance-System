using Microsoft.AspNetCore.Mvc;
using StudentAttendanceSystem.Data;
using Models;
using System.Diagnostics;
using DAL.Repositories;
using BLL.Util;

namespace StudentAttendanceSystem.Controllers
{
    public class HomeController(ApplicationDBContext db) : Controller
    {
        
        public IActionResult Index() => View(db.GetAllClasses());
        
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ClassModel model)
        {
            if (!ModelState.IsValid) return View();
            var res = db.AddClass(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if(res == DBUpdateStatus.Success)
                return RedirectToAction("Index");

            return View();
        }

        public IActionResult Edit(int? slug)
        {
            var model = db.GetClassById(slug);
            return model != null ? View(model) : NotFound();
        }

        [HttpPost]
        public IActionResult Edit(ClassModel model)
        {
            if (!ModelState.IsValid) return View();
            var res = db.UpdateClass(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if(res == DBUpdateStatus.Success)
                return RedirectToAction("Index");

            return View();
        }

        public IActionResult Delete(int? slug)
        {
            var model = db.GetClassById(slug);
            return model != null ? View(model) : NotFound();
        }
        
        [HttpPost]
        public IActionResult Delete(ClassModel model)
        {
            var res = db.DeleteClass(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if (res == DBUpdateStatus.Success)
                return RedirectToAction("Index");

            return RedirectToAction("Index");
        }
    
        public IActionResult About() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
