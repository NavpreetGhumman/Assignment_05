using Assignment_05.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Assignment_05.Controllers
{
    public class TeacherDataController : ApiController
    {
        //The database context class which allow us to access the MySQL database.
        private SchoolDbContext school = new SchoolDbContext();

        //The controller will access the teachers table of our school database
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example> Get api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of all information about teachers
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //OPen the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) " +
                "or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) " +
                "like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Info
            List<Teacher> TeacherInfo = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.teacherid = TeacherId;
                NewTeacher.teacherfname = TeacherFname;
                NewTeacher.teacherlname = TeacherLname;
                NewTeacher.employeenumber = EmployeeNumber;
                NewTeacher.hiredate = HireDate;
                NewTeacher.salary = salary;

                //Add teachers info to the list
                TeacherInfo.Add(NewTeacher);
            }


            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher's Information
            return TeacherInfo;
        }

        /// <summary>
        /// Find Teacher from the MySQL Database through an id. Non-Detrerministic.
        /// </summary>
        /// <param name="id">The author Id</param>
        /// <returns>Teacher Object containing information about teacher with a matching id.Empty Teacher Object if Id doesn't match in the system</returns>
        ///<example>api/TeacherData/FindTeacher/6 -> return Teacher Object</example>
        ///<example>api/TeacherData/FindTeacher/9 -> return Teacher Object</example>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers  where teachers.teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.teacherid = TeacherId;
                NewTeacher.teacherfname = TeacherFname;
                NewTeacher.teacherlname = TeacherLname;
                NewTeacher.employeenumber = EmployeeNumber;
                NewTeacher.hiredate = HireDate;
                NewTeacher.salary = Salary;

            }
            return NewTeacher;
        }

        /// <summary>
        /// Deletes Teacher from the connected MySQL database if Teacher Id exists. Does not maintain integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the teacher</param>
        /// <example>Post: api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]

        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from Teachers where Teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();
        }



        /// <summary>
        /// Adds new teacher in the connected database  
        /// </summary>
        /// <param name="NewTeacher">An Object with fields that maps to the column of the teacher's table</param>
        ///<example>
        ///POST: api/TeacherData/AddTeacher
        ///FROM DATA /POST DATA/ REQUEST BODY
        ///{
        ///"TeacherFname":"Simon",
        ///"TeacherLname":"Borer",
        ///"EmployeeNumber":"T055",
        ///"HireDate":"2020-11-08",
        ///"Salary":"60.5"
        ///}
        ///</example>


        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Insert into Teachers(teacherfname, teacherlname, employeenumber, hiredate, salary)" +
                " values(@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.hiredate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();
        }

        /// <summary>
        /// Updates Teacher on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teachers table.</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/208 
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
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@HireDate, salary = @Salary  where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.teacherfname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.teacherlname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.employeenumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.hiredate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();
        }

    }
}
