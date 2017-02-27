using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SyemaTest.Models;

namespace SyemaTest.Controllers
{
    public class DepartmentsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Departments
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.data = db.Courses.ToList();
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department, List<int> SelectedCourses)
        {
            
            if (ModelState.IsValid)
            {
                foreach (var item in SelectedCourses)
                {
                    var course = db.Courses.Find(item);
                    department.Courses.Add(course);
                }
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            ViewBag.data = db.Courses.ToList();
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department, List<int> SelectedCourses)
        {
         
            if (ModelState.IsValid)
            {
                var departmentToBeUpdated = db.Departments.FirstOrDefault(x => x.Id == department.Id);
                departmentToBeUpdated.Name = department.Name;
                departmentToBeUpdated.Courses.Clear();

                foreach (var item in SelectedCourses)
                {
                    var course = db.Courses.Find(item);
                    departmentToBeUpdated.Courses.Add(course);
                }

                db.Entry(departmentToBeUpdated).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            var departmentToBeUpdated = db.Departments.FirstOrDefault(x => x.Id == department.Id);
            departmentToBeUpdated.Name = department.Name;
            departmentToBeUpdated.Courses.Clear();
            db.Departments.Remove(departmentToBeUpdated);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult SendCourse(int ? id)
        {
            
            var courses = db.Departments.Find(id).Courses.Select(x => new
            {
                x.Id, x.Name
            }).ToList();
            return Json(new {data = courses }, JsonRequestBehavior.AllowGet);
        }
    }
}
