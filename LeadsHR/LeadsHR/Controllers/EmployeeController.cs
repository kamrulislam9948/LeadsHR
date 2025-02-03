using LeadsHR.Models;
using LeadsHR.UnityOfWork;
using LeadsHR.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LeadsHR.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // List Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            var educationInfos = await _unitOfWork.EducationInfos.GetAllAsync();

            var viewModel = new EmployeeViewModel
            {
                Employees = employees,
                EducationInfos = educationInfos
            };

            return View(viewModel);
        }

        // Search Employee by ID
        public async Task<IActionResult> GetEmployeDetailsById(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // Create Employee - GET
        public IActionResult Create()
        {
            var model = new Employee
            {
                EducationInfos = new List<EducationInfo>() 
            };
            return View(model);
        }


        // Create Employee - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            // Ensure the EducationInfos list is initialized
            if (employee.EducationInfos == null)
            {
                employee.EducationInfos = new List<EducationInfo>();
            }

            if (employee.EmployeeId == 0)
            {
                // Add the employee with their educational information
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.SaveAsync();  // Save changes

                // Redirect to the Index action after successful creation
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, return the same view with the model
            return View(employee);
        }




        // Edit Employee - GET
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // Edit Employee - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _unitOfWork.Employees.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // Delete Employee - GET (Confirm Deletion)
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // Delete Employee - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _unitOfWork.Employees.GetByIdAsync(id).Result;
            if (employee == null)
                return NotFound();

            _unitOfWork.Employees.Remove(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
