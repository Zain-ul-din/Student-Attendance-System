using Microsoft.AspNetCore.Mvc;
using StudentAttendanceSystem.Data;
using Models;
using System.Diagnostics;

namespace StudentAttendanceSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDBContext _db;

        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<ClassModel> classes = _db.Classes.ToList<ClassModel>();
            return View(classes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClassModel model)
        {
            if (!ModelState.IsValid) return View();
            _db.Classes.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Edit(int? slug)
        {
            if(slug == null || slug == 0) return RedirectToAction("Index");
            ClassModel? model = _db.Classes.Find(slug);
            return model != null ? View(model) : NotFound();
        }

        [HttpPost]
        public IActionResult Edit(ClassModel model)
        {
            if (!ModelState.IsValid) return View();
            _db.Classes.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? slug)
        {
            if (slug == null || slug == 0) return RedirectToAction("Index");
            ClassModel? model = _db.Classes.Find(slug);
            return model != null ? View(model) : NotFound();
        }
        
        [HttpPost]
        public IActionResult Delete(ClassModel model)
        {
            _db.Classes.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
