using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCFEmployee_Client.ServiceReference1;

namespace WCFEmployee_Client.Controllers
{
    public class EmployeeController : Controller
    {
        private Service1Client serviceAClient;

        public EmployeeController()
        {
            serviceAClient = new Service1Client();
        }
        // GET: Employee
        public ActionResult Index(string searchString)
        {
            ViewBag.CurrentFilter = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                var employeeList = serviceAClient.SearchByDepartment(searchString);
                return View(employeeList);
            }

            return View(serviceAClient.GetAll());
        }
        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            var result = serviceAClient.AddEmployee(employee);
            if (result != null)
            {
                var list = serviceAClient.GetAll();
                return View("Index", list);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
