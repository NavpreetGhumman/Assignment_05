using Assignment_05.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace Assignment_05.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/Index
        public ActionResult Index()
        {
            return View();
        }


        //Get:/Teacher/List
        //Acquire information about the teachers and send it to the List.cshtml
        public ActionResult List(string SearchKey = null)
        {
            //We will get the information from ListTeachers method
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> TeacherInfo = controller.ListTeachers(SearchKey);
            return View(TeacherInfo);
        }

        //Get:/Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        //Get:/Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        // GET: /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: /Teacher/New
        public ActionResult Add()
        {
            return View();
        }

        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            //Identify that thismethod is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have access to create method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            //Server side validation for preventing form to submit with empty fields.


            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = TeacherFname;
            NewTeacher.teacherlname = TeacherLname;
            NewTeacher.employeenumber = EmployeeNumber;
            NewTeacher.hiredate = HireDate;
            NewTeacher.salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>
        [HttpGet]
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }




        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher/Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmploeeNumber">The updated employeenumber of the teacher.</param>
        /// <param name="HireDate">The updated hiredate of the teacher.</param>
        /// <param name="Salary">The updated salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///"TeacherFname":"Simon",
        ///"TeacherLname":"Borer",
        ///"EmployeeNumber":"T055",
        ///"HireDate":"2020-11-08",
        ///"Salary":"60.5"
        ///}
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string teacherfname, string teacherlname, string employeenumber, DateTime hiredate, decimal salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.teacherfname = teacherfname;
            TeacherInfo.teacherlname = teacherlname;
            TeacherInfo.employeenumber = employeenumber;
            TeacherInfo.hiredate = hiredate;
            TeacherInfo.salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }

    }
}