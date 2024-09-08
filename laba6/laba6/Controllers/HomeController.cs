using laba6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace laba6.Controllers
{
    public class HomeController : Controller
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Bulat\Desktop\Students.accdb;");

        public IActionResult Index()
        {
            
            DataTable courses = new DataTable("Курсы");
            OleDbDataAdapter adapter1 = new OleDbDataAdapter("SELECT Курс FROM Студенты", connection);
            adapter1.Fill(courses);
            
            //OleDbDataAdapter adapter1 = new OleDbDataAdapter("SELECT Курс FROM Студенты", connection);
            //DataSet dataSet = new DataSet();

            //adapter1.Fill(dataSet, "Студенты");
            //DataTable courses = dataSet.Tables[0];

            var t = from StudentsRow in courses.AsEnumerable() 
                    orderby StudentsRow["Курс"] 
                    select StudentsRow["Курс"];

            ViewBag.Course = t.Distinct();

            return View();
        }

        public IActionResult Count(int course)
        {
            DataTable students = new DataTable("Студенты");
            OleDbDataAdapter adapter2 = new OleDbDataAdapter("SELECT * FROM Студенты", connection);
            adapter2.Fill(students);

            //OleDbDataAdapter adapter2 = new OleDbDataAdapter("SELECT * FROM Студенты", connection);
            //DataSet dataSet = new DataSet();
            //adapter2.Fill(dataSet, "Студенты");
            //DataTable students = dataSet.Tables[0];

            int count = (from StudentsRow in students.AsEnumerable()
                         where StudentsRow.Field<int>("Курс") == course
                         select StudentsRow.Field<int>("Курс")).Count();

            ViewBag.text = "Количество студентов на " + course + " курсе: " + count + " человек";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}