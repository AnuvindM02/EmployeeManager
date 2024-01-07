using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly EmployeesDbContext _db;

        //constructor
        public EmployeeServices(EmployeesDbContext employeesDbContext)
        {
            _db = employeesDbContext;
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
            _db.Employees.Add(employee);
            _db.SaveChanges();

            return employee.ToEmployeeResponse();

        }

        public bool DeleteEmployee(Guid? EmployeeId)
        {
            if (EmployeeId == null)
                throw new ArgumentNullException(nameof(EmployeeId));

            Employee? employee = _db.Employees.FirstOrDefault(employee => employee.EmployeeID==EmployeeId);
            if(employee == null)
                return false;

            _db.Employees.Remove(_db.Employees.First(employee => employee.EmployeeID == EmployeeId));
            _db.SaveChanges();

            return true;
        }

        public List<EmployeeResponse> GetAllEmployees()
        {
            /*List<EmployeeResponse> lists = new List<EmployeeResponse>() { new EmployeeResponse() { Email="anuvindm02@gmail.com",EmployeeID=Guid.NewGuid(),
            EmployeeName="anuvind",Gender=GenderOptions.Male.ToString(), Position="developer"} };
            return lists;*/
            return _db.Employees.Select(employee => employee.ToEmployeeResponse()).ToList();
        }

        public EmployeeResponse GetEmployeeById(Guid? EmployeeId)
        {
            if (EmployeeId == null)
                return null;

            Employee? employee = _db.Employees.FirstOrDefault(emp => emp.EmployeeID == EmployeeId);

            if (employee == null)
                return null;

            return employee.ToEmployeeResponse();
        }

        
        public EmployeeResponse UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest)
        {
            if(employeeUpdateRequest == null)
                throw new ArgumentException(nameof(employeeUpdateRequest));

            //Model Validation
            ValidationHelper.ModelValidation(employeeUpdateRequest);

            //retrieving the matching employee
            Employee? matchingEmployee = _db.Employees.FirstOrDefault(employee => employee.EmployeeID == employeeUpdateRequest.EmployeeID);

            if (matchingEmployee == null)
                throw new ArgumentException("No employee found");

            matchingEmployee.EmployeeName = employeeUpdateRequest?.EmployeeName;
            matchingEmployee.Position = employeeUpdateRequest?.Position;
            matchingEmployee.Email = employeeUpdateRequest?.Email;
            matchingEmployee.Gender = employeeUpdateRequest?.Gender.ToString();

            _db.SaveChanges();

            return matchingEmployee.ToEmployeeResponse();
        }

        public List<EmployeeResponse> GetFilteredEmployees(string searchBy, string? searchstring)
        {
            List<EmployeeResponse> allEmployees = GetAllEmployees();
            List<EmployeeResponse> matchingEmployees = allEmployees;

            if (string.IsNullOrEmpty(searchstring) || string.IsNullOrEmpty(searchBy))
                return matchingEmployees;

            switch (searchBy)
            {
                case nameof(EmployeeResponse.EmployeeName):
                    matchingEmployees = allEmployees.Where(emp =>
                    (!string.IsNullOrEmpty(emp.EmployeeName)?
                    emp.EmployeeName.Contains(searchstring, StringComparison.OrdinalIgnoreCase):true)).ToList();
                    break;

                case nameof(EmployeeResponse.Position):
                    matchingEmployees = allEmployees.Where(emp =>
                    (!string.IsNullOrEmpty(emp.Position) ?
                    emp.Position.Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(EmployeeResponse.Email):
                    matchingEmployees = allEmployees.Where(emp =>
                    (!string.IsNullOrEmpty(emp.Email) ?
                    emp.Email.Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(EmployeeResponse.Gender):
                    matchingEmployees = allEmployees.Where(emp =>
                    (!string.IsNullOrEmpty(emp.Gender) ?
                    emp.Gender.Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
            }

            return matchingEmployees;

        }

        public List<EmployeeResponse> GetSortedEmployees(List<EmployeeResponse> allEmployees, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allEmployees;

            List<EmployeeResponse> sortedEmployees = (sortBy, sortOrder) switch
            {
                (nameof(EmployeeResponse.EmployeeName), SortOrderOptions.ASC) => allEmployees.OrderBy(emp => emp.EmployeeName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(EmployeeResponse.EmployeeName), SortOrderOptions.DESC) => allEmployees.OrderByDescending(emp => emp.EmployeeName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(EmployeeResponse.Position), SortOrderOptions.ASC) => allEmployees.OrderBy(emp => emp.Position, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(EmployeeResponse.Position), SortOrderOptions.DESC) => allEmployees.OrderByDescending(emp => emp.Position, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(EmployeeResponse.Email), SortOrderOptions.ASC) => allEmployees.OrderBy(emp => emp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(EmployeeResponse.Email), SortOrderOptions.DESC) => allEmployees.OrderByDescending(emp => emp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(EmployeeResponse.Gender), SortOrderOptions.ASC) => allEmployees.OrderBy(emp => emp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(EmployeeResponse.Gender), SortOrderOptions.DESC) => allEmployees.OrderByDescending(emp => emp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
                _ => allEmployees
            };

            return sortedEmployees;
        }
    }
}