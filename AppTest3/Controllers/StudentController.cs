using AppTest3.Models.Repositories;
using AppTest3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace AppTest3.Controllers
{
    [Authorize(Roles = "admin,manager")]
    public class StudentController : Controller
    {

        readonly IStudentRepository StudentRepository;
        readonly ISchoolRepository SchoolRepository;


        public StudentController(ISchoolRepository SchooRepository, IStudentRepository StudRepository)
        {
            SchoolRepository = SchooRepository;
            StudentRepository = StudRepository;

        }

        // GET: StudentController
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.SchoolID = new SelectList(SchoolRepository.GetAll(), "SchoolID", "SchoolName");

            var Students = StudentRepository.GetAll();
            return View(Students);
        }
        //search
        public ActionResult Search(string name, int? Schoolid)
        {
            var result = StudentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = StudentRepository.FindByName(name);
            else
            if (Schoolid != null)
                result = StudentRepository.GetStudentsBySchoolID(Schoolid);
            ViewBag.SchoolID = new SelectList(StudentRepository.GetAll(), "SchoolID", "SchoolName");
            return View("Index", result);
        }


        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var Students = StudentRepository.GetById(id);
            return View(Students);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(SchoolRepository.GetAll(), "SchoolID", "SchoolName"); 
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student s)
        {
            try
            {
                ViewBag.SchoolID = new SelectList(SchoolRepository.GetAll(), "SchoolID", "SchoolName");
                StudentRepository.Add(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var Students = StudentRepository.GetById(id);
            return View(Students);

        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student s)
        {
            try
            {
                StudentRepository.Edit(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var student = StudentRepository.GetById(id);
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete( Student s)
        {
            try
            {
                StudentRepository.Delete(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





    }

}