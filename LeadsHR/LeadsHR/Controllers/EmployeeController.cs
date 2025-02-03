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

            var employeeEducationInfos = await _unitOfWork.EducationInfos.FindAsync(e => e.EmployeeId == id);

            var viewModel = new EmployeeEditViewModel
            {
                Employee = employee,
                EducationInfos = employeeEducationInfos.ToList()
            };

            return View(viewModel);
        }



        // Edit Employee - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEditViewModel model)
        {
            if (id != model.Employee.EmployeeId)
                return BadRequest();

            //if (!ModelState.IsValid)
            //{
            //    // If ModelState is invalid, return the same view with existing data
            //    model.EducationInfos = (await _unitOfWork.EducationInfos
            //                           .FindAsync(e => e.EmployeeId == model.Employee.EmployeeId))
            //                           .ToList();
            //    return View(model);
            //}

            // Fetch the existing employee from the database
            var employeeInDb = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employeeInDb == null)
                return NotFound();

            // Update employee details
            employeeInDb.FirstName = model.Employee.FirstName;
            employeeInDb.LastName = model.Employee.LastName;
            employeeInDb.Email = model.Employee.Email;
            employeeInDb.Division = model.Employee.Division;
            employeeInDb.Building = model.Employee.Building;
            employeeInDb.Title = model.Employee.Title;
            employeeInDb.Room = model.Employee.Room;

            // Handle education information updates
            var existingEducationInfos = employeeInDb.EducationInfos.ToList();

            // Remove education info that no longer exists in the updated list
            foreach (var existingEdu in existingEducationInfos)
            {
                if (!model.EducationInfos.Any(e => e.EducationInfoId == existingEdu.EducationInfoId))
                {
                    _unitOfWork.EducationInfos.Remove(existingEdu);
                }
            }

            // Add or update education info
            foreach (var edu in model.EducationInfos)
            {
                var existingEdu = existingEducationInfos
                    .FirstOrDefault(e => e.EducationInfoId == edu.EducationInfoId);

                if (existingEdu != null)
                {
                    // Update existing education info
                    existingEdu.Degree = edu.Degree;
                    existingEdu.Institution = edu.Institution;
                    existingEdu.YearOfPassing = edu.YearOfPassing;
                    existingEdu.CGPA = edu.CGPA;
                }
                else
                {
                    // Add new education info
                    employeeInDb.EducationInfos.Add(new EducationInfo
                    {
                        EmployeeId = model.Employee.EmployeeId,
                        Degree = edu.Degree,
                        Institution = edu.Institution,
                        YearOfPassing = edu.YearOfPassing,
                        CGPA = edu.CGPA
                    });
                }
            }

            await _unitOfWork.SaveAsync();  // Save changes

            return RedirectToAction(nameof(Index));
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
