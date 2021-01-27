using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SevenProTask.Models;
using SevenProTask.ViewModels;

namespace SevenProTask.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(CourseViewModel model)
        {
            model.IsCourses = Utilities.HomeUtility.IsCourses(_context);
            model.Courses = await _context.Courses.ToListAsync();
            var dateTimeArr = Utilities.HomeUtility.GetTimeIntervals();
            var days = Utilities.HomeUtility.GetDays();
            ViewData["Days"] = new SelectList(days);
            ViewData["TimeIntervals"] = new SelectList(dateTimeArr);
            return View(model);
        }

        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var course = new Course
            {
                CourseName = model.CourseName,
                Description = model.Description,
                Price = model.Price,
                Day = model.Day,
                TimeBegin = model.TimeBegin,
                TimeEnd = model.TimeEnd
            };
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}