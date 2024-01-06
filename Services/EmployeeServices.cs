using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly List<Employee> _employees;
        private readonly IEmployeeServices _employeeServices;

        //constructor
        public EmployeeServices()
        {
            _employees = new List<Employee>();
            _employeeServices  = new EmployeeServices();
        }

        public EmployeeResponse AddEmployee(EmployeeAddRequest? employeeAddRequest)
        {
            //Throwing Exception when argument is not provided
            if(employeeAddRequest == null) throw new ArgumentNullException(nameof(employeeAddRequest));

            //Model Validation
            ValidationHelper.ModelValidation(employeeAddRequest);

            Employee employee = employeeAddRequest.ToEmployee();

            employee.EmployeeID=Guid.NewGuid();

            //Adding Employee to database
            _employees.Add(employee);

            return employee.ToEmployeeResponse();

        }

        public bool DeleteEmployee(Guid? EmployeeId)
        {
            if (EmployeeId == null)
                throw new ArgumentNullException(nameof(EmployeeId));

            Employee? employee = _employees.FirstOrDefault(employee => employee.EmployeeID==EmployeeId);
            if(employee == null)
                return false;

            _employees.RemoveAll(employee => employee.EmployeeID == EmployeeId);

            return true;

        }

        public List<EmployeeResponse> GetAllEmployees()
        {
            return _employees.Select(employee => employee.ToEmployeeResponse()).ToList();
        }

        public EmployeeResponse UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest)
        {
            if(employeeUpdateRequest == null)
                throw new ArgumentException(nameof(employeeUpdateRequest));

            //Model Validation
            ValidationHelper.ModelValidation(employeeUpdateRequest);

            //retrieving the matching employee
            Employee? matchingEmployee = _employees.FirstOrDefault(employee => employee.EmployeeID == employeeUpdateRequest.EmployeeID);

            if (matchingEmployee == null)
                throw new ArgumentException("No employee found");

            matchingEmployee.EmployeeName = employeeUpdateRequest?.EmployeeName;
            matchingEmployee.Position = employeeUpdateRequest?.Position;
            matchingEmployee.Email = employeeUpdateRequest?.Email;
            matchingEmployee.Gender = employeeUpdateRequest?.Gender.ToString();

            return matchingEmployee.ToEmployeeResponse();
        }
    }
}