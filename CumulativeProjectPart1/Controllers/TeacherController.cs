using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProjectPart1.Models;
using System.Diagnostics;

namespace CumulativeProjectPart1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);


            return View(SelectedTeacher);
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]

        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");

        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, Decimal Salary)
        {
         //Validation

            //If the user leaves the teachers first name or last name blank they will be redirected back to the form 
            if (TeacherFname == "" || TeacherLname == "")
            {
                return RedirectToAction("New");

                

            } 

            else
            {
                // Creates New Teacher Object
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                TeacherDataController controller = new TeacherDataController();
                controller.AddTeacher(NewTeacher);

                return RedirectToAction("List");


            }

        
        }
        //GET : /Teacher/Update/{id}
   
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        //POST : /Teacher/Update

        /// <summary>
        /// <param> name="id"/> Id of the Teacher to Update </param>
        /// <param name="TeacherFname"/> The updated first name of the teacher </param>
        /// <param name="TeacherLname"/> The updated last name of the teacher </param>
        /// <param name="EmployeeNumber"/> The updated employee number of the teacher </param>
        /// <param name="Hiredate"/> The updated hire date of the teacher </param>
        /// <param name="Salary"/> The updated salary of the teacher </param>
        /// </summary>
        /// <returns>A webpage that provides information about the teacher</returns>
        /// <example>POST: /Teacher/Update/{8}
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        /// "TeacherFname":"Justin",
        /// "TeacherLname":"Abante",
        /// "EmployeeNumber":"T2342",
        /// }
        /// </example>
        /// 
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, Decimal Salary)
        {

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }


}