using Form.Data;
using Form.Models;
using Form.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace Form.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public StudentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        public IActionResult Index()
        {
            return View();
        }

        //Creating the view page for myform

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
          
            if(ModelState.IsValid) { 
                var student = new Student()
                {
                    Name = register.Name,
                    Phone = register.Phone,
                    Email = register.Email,
                    Age= register.Age
                };

                await applicationDbContext.Students.AddAsync(student);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(register);

        }
        
        
    }
}
