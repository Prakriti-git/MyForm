using Form.Data;
using Form.Models;
using Form.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;

namespace Form.Controllers
{
    //Creating a constructor of the controller.
    public class StudentController : Controller

    //Assigning a private field.
    {
        private readonly ApplicationDbContext applicationDbContext;

        public StudentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        //Index page.
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var students = await applicationDbContext.Students.ToListAsync();

            if (HttpContext.Session.GetString("StudentsSession") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        //The code for register area is written below.

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        //We are using HttpPost request for saving he input data into the database.
        public async Task<IActionResult> Register(Register register)
        {



        //ModelState.IsValid means that if there is no error validation in the form, execute the code in the if statement.
           
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    Name = register.Name,
                    Phone = register.Phone,
                    Email = register.Email,
                    Age = register.Age,
                    Password = register.Password,
                   
                };

                await applicationDbContext.Students.AddAsync(student);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(register);

        }


        //Creating the login page for myform

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {

            var myStudent = applicationDbContext.Students.Where(x => x.Email == login.StudentEmail && x.Password == login.StudentPassword).FirstOrDefault();

            if (myStudent != null)
            {
                HttpContext.Session.SetString("StudentSession", myStudent.Email);
                return RedirectToAction("Dashboard");
            }
             
            // student null 

            else
            {
                ViewBag.Message = "Login failed, Please use correct email and password.";
            }

           
            return View();

        }







        //Contact view and controller

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        
        public async Task<IActionResult> Contact(Contact contact)
        {

            if (ModelState.IsValid)
            {
                var newContact = new Contact()
                {
                   
                    Name = contact.Name,
                    Email = contact.Email,
                    Description = contact.Description,

                };

                //await applicationDbContext.Contact.AddAsync(newContact);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contact);


        }


        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var students = await applicationDbContext.Students.ToListAsync();
            return View(students);
        }


    }
}
