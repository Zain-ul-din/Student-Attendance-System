using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Helpers;
using StudentAttendanceSystem.Data;
using DAL.Repositories;
using BLL.Util;

namespace StudentAttendanceSystem.Controllers
{
    public class AttendanceController(ApplicationDBContext db) : Controller
    {

        public IActionResult Index(int secId)
        {
            var section = db.GetSectionByIdIncludingClass(secId);
            if (section == null) return NotFound();
            
            ViewBag.SecId = secId;

            var attendances = db.GetAttendanceOfSection(secId);

            var model = new AttendanceHistoryViewModel
            {
                Section = section,
                Attendances = attendances
            };

            return View(model);
        }

        public IActionResult Create(int? secId)
        {
            ViewBag.SecId = secId;
            SectionModel? model = db.GetSectionByIdIncludingClassAndStudents(secId);
            if(model == null) return NotFound();

            var attendanceModels = model?.Students?
                .Select(s => new AttendanceModel { StudentId = s.Id, IsPresent = false }).ToList();
            var viewModel = new AttendanceViewModel
            {
                Section = model,
                AttendanceModels = attendanceModels ?? []
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(AttendanceViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            foreach (var attendance in model.AttendanceModels)
            {
                var res = db.CreateAttendance(attendance);
                if(res != DBUpdateStatus.Success)
                {
                    TempData["DBUpdateStatus"] = res.GetMsg();
                }
            }

            return RedirectToAction("Index", new { secId = model.Section.Id });
        }

        public IActionResult Edit(int slug)
        {
            var model = db.GetAttendanceById(slug);
            if (model == null) return NotFound();
            
            var student = db.GetAttendanceById(model.StudentId);
            if (student == null) return NotFound();
            
            model.Student = db.GetStudentById(model.StudentId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AttendanceModel model)
        {
            if (!ModelState.IsValid) return View();
            var student = db.GetStudentById(model.StudentId);
            if(student == null) return NotFound();

            var res = db.UpdateAttendance(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if (res == DBUpdateStatus.Success)
                return RedirectToAction("Index", new { secId = student.SectionId });
                        
            return View();
        }

        public IActionResult Delete(int slug)
        {
            var model = db.GetAttendanceById(slug);
            if (model == null) return NotFound();
            model.Student = db.GetStudentById(model.StudentId);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(AttendanceModel model)
        {
            var student = db.GetStudentById(model.StudentId);
            if (student == null) return NotFound();

            var res = db.DeleteAttendance(model);
            TempData["DBUpdateStatus"] = res.GetMsg();

            if (res == DBUpdateStatus.Success)
                return RedirectToAction("Index", new { secId = student.SectionId });
            
            return View();
        }
    }
}

