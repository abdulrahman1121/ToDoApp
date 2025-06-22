using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoApplication.Data;
using ToDoApplication.Models;
using ToDoApplication.Data;

namespace ToDoApplication.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedAt = DateTime.Now;
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Task task)
        {
            if (id != task.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportToXml()
        {
            var tasks = _context.Tasks.ToList();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Task>));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, tasks);
                var xml = stringWriter.ToString();
                return File(System.Text.Encoding.UTF8.GetBytes(xml), "application/xml", "Tasks.xml");
            }
        }
    }
}