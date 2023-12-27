using AppTest3.Models.Repositories;
using AppTest3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AppTest3.Controllers
{
    
    [Authorize(Roles = "admin,manager")]
    public class SchoolController : Controller
    {
        readonly ISchoolRepository SchoolRepository;
        //private readonly IWebHostEnvironment hostingEnvironment;

        public SchoolController(ISchoolRepository SchooRepository, IWebHostEnvironment hostingEnvironment)
        {
            SchoolRepository = SchooRepository;
          //  this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        // GET: SchoolController
        public ActionResult Index()
        {
            var School = SchoolRepository.GetAll();
            return View(School);
        }

        // GET: SchoolController/Details/5
        public ActionResult Details(int id)
        {
            var School = SchoolRepository.GetById(id);
            return View(School);
        }

        // GET: SchoolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(School s)
        {
            try
            {
                SchoolRepository.Add(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolController/Edit/5
        public ActionResult Edit(int id)
        {
            var School = SchoolRepository.GetById(id);
            return View(School);
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(School s)
        {
            try
            {
                SchoolRepository.Edit(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolController/Delete/5
        public ActionResult Delete(int id)
        {
            var School = SchoolRepository.GetById(id);
            return View(School);
        }

        // POST: SchoolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, School s)
        {
            try
            {
                SchoolRepository.Delete(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

}

