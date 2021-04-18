using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativeProjectPart1.Models;
using MySql.Data.MySqlClient;

namespace CumulativeProjectPart1.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Context Class that allows us to access the School Database
        private SchoolDbContext School = new SchoolDbContext();

        //Objective:
        //Access data that is stored in the teachers table
        // <summary>
        // Returns a list of data stored in the teachers table in the school db
        // </summary>
        // <returns> A list of teachers </returns>
        // <example>
        // GET api/TeacherData/ListTeachers
        // </example>

     [HttpGet]
     [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

  public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database

            Conn.Open();

            //Establish a new command (query) for the database

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            //Allows us to search for teacher names based on a search key
            cmd.CommandText = "SELECT * FROM Teachers WHERE LOWER(teacherfname) LIKE LOWER (@key) OR LOWER(teacherlname) LIKE LOWER (@key) OR lower (CONCAT (teacherfname, ' ', teacherlname)) LIKE LOWER(@key)" ;

            cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through each row 
            while(ResultSet.Read())
            {

                //Access the data in the Teachers table
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

               
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
           

                Teachers.Add(NewTeacher);
            }
            //Close Connection from Server and Database
            Conn.Close();

            //Return final list of Names
                return Teachers;

        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database

            Conn.Open();

            //Establish a new command (query) for the database

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            cmd.CommandText = "SELECT * FROM Teachers WHERE teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loops through the rows
            while (ResultSet.Read())
            {
                //Accesses the columns in the teachers table
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            //Close Connection
            Conn.Close();

            return NewTeacher;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST : /api/TeacherData/DeleteTeacher/2</example>
        [HttpPost]
      

        public void DeleteTeacher(int id)
        {

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database

            Conn.Open();

            //Establish a new command (query) for the database

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            cmd.CommandText = "DELETE FROM Teachers WHERE teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
        [HttpPost]
        public void AddTeacher(Teacher Newteacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database

            Conn.Open();

            //Establish a new command (query) for the database

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES (@teacherfname,@teacherlname,@employeenumber,@hiredate,@salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", Newteacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", Newteacher.TeacherLname);
            cmd.Parameters.AddWithValue("@Employeenumber", Newteacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", Newteacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", Newteacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

    }
}
