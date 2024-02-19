using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;
using Models;
using DAL.Repositories;
using BLL.Util;

namespace StudentAttendanceSystem.Controllers
{
    public class StudentsController(ApplicationDBContext db) : Controller
    {

        public IActionResult Index(int secId)
        {
            var section = db.GetSectionByIdIncludingClass(secId);
            if (section == null) return NotFound();

            List<StudentModel>? students = db.GetStudentsIncludingSections(secId);
            if(students == null) return NotFound();

            StudentsViewModel model = new()
            {
                Section = section,
                Students = students
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
            var student = db.GetStudentById(stdId);
            if (student == null) return NotFound();
            
            var attendances = db.GetAttendanceOfStudent(stdId);
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
            var res  = db.CreateStudent(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if(res == DBUpdateStatus.Success)
                return RedirectToAction("Index", new { secId = model.SectionId });

            return View(model);
        }

        public IActionResult Edit(int? stdId)
        {
            if (stdId == 0 || stdId == null) return NotFound();
            StudentModel? model = db.GetStudentById(stdId);
            if(model == null) return NotFound();
            ViewBag.SecId = model.SectionId;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StudentModel model)
        {
            if (!ModelState.IsValid) return View();

            var res = db.UpdateStudent(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if(res == DBUpdateStatus.Success)
                return RedirectToAction("Index", new {
                    secId = model.SectionId
                });
            
            return View(model);
        }

        public IActionResult Delete(int? stdId)
        {
            StudentModel? model = db.GetStudentById(stdId);
            if(model == null) return NotFound();
            ViewBag.SecId = model.SectionId;
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(StudentModel model)
        {
            var res = db.DeleteStudent(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if (res == DBUpdateStatus.Success)
                return RedirectToAction("Index", new
                { secId = model.SectionId });

            return View();
        }
    }

}

