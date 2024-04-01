using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDBManager.Models.Data;
using ProjectDBManager.Models.Data.Entities;

namespace ProjectDBManager.Controllers
{
    public class ProjectEmployeesController : Controller
    {
        private readonly EDBContext _context;

        public ProjectEmployeesController(EDBContext context)
        {
            _context = context;
        }

        // GET: ProjectEmployees
        public async Task<IActionResult> Index()
        {
            var eDBContext = _context.ProjectEmployee.Include(p => p.Employee).Include(p => p.Project);
            return View(await eDBContext.ToListAsync());
        }

        // GET: ProjectEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectEmployee = await _context.ProjectEmployee
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (projectEmployee == null)
            {
                return NotFound();
            }

            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name");
            return View();
        }

        // POST: ProjectEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,EmployeeId")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", projectEmployee.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectEmployee = await _context.ProjectEmployee.FindAsync(id);
            if (projectEmployee == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", projectEmployee.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // POST: ProjectEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,EmployeeId")] ProjectEmployee projectEmployee)
        {
            if (id != projectEmployee.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectEmployeeExists(projectEmployee.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", projectEmployee.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectEmployee = await _context.ProjectEmployee
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (projectEmployee == null)
            {
                return NotFound();
            }

            return View(projectEmployee);
        }

        // POST: ProjectEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectEmployee = await _context.ProjectEmployee.FindAsync(id);
            if (projectEmployee != null)
            {
                _context.ProjectEmployee.Remove(projectEmployee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SearchEmployee(string query)
        {
            var employees = _context.Employees
                .Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query))
                .Select(e => new
                {
                    e.EmployeeId,
                    FullName = $"{e.FirstName} {e.LastName}"
                })
                .ToList();

            return PartialView("_EmployeeList", employees);
        }


        private bool ProjectEmployeeExists(int id)
        {
            return _context.ProjectEmployee.Any(e => e.ProjectId == id);
        }
    }
}
