using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly List<Employee> _employees;

        //constructor
        public EmployeeServices()
        {
            /*            _employees = new List<Employee>();
            */
            _employees = new List<Employee>{
                new Employee() { EmployeeName = "Anuvind", EmployeeID = Guid.Parse("5D3C8142-B806-414F-8D3F-2DE6ED422C09"),Email="anuvind@gmail.com",Gender="Male",Position="Junior Developer"},
                new Employee() { EmployeeName = "Avani", EmployeeID = Guid.Parse("40E13262-FB41-48F2-BBA7-A305ABB737F0"), Email = "avani@gmail.com", Gender = "Female", Position = "Tester" },
                new Employee() { EmployeeName = "Aguste", EmployeeID = Guid.Parse("8082ED0C-396D-4162-AD1D-29A13F929824"), Email = "aleddy0@booking.com", Gender = "Female", Position = "HR" },
                new Employee() { EmployeeName = "Jasmina", EmployeeID = Guid.Parse("06D15BAD-52F4-498E-B478-ACAD847ABFAA"), Email = "jsyddie1@miibeian.gov.cn", Gender = "Female", Position = "Data Analyst" },
                new Employee() { EmployeeName = "Kilian", EmployeeID = Guid.Parse("D3EA677A-0F5B-41EA-8FEF-EA2FC41900FD"), Email = "khaquard2@arstechnica.com", Gender = "Male", Position = "Developer" },
                new Employee() { EmployeeName = "Kendall", EmployeeID = Guid.Parse("89452EDB-BF8C-4283-9BA4-8259FD4A7A76"), Email = "kendall01@gmail.com", Gender = "Female", Position = "Junior Developer" },
                new Employee() { EmployeeName = "Corabelle", EmployeeID = Guid.Parse("F5BD5979-1DC1-432C-B1F1-DB5BCCB0E56D"), Email = "corabelle@outlook.com", Gender = "Female", Position = "Senior Developer" },
                new Employee() { EmployeeName = "Seumas", EmployeeID = Guid.Parse("A795E22D-FAED-42F0-B134-F3B89B8683E5"), Email = "seumus@gmail.com", Gender = "Male", Position = "Data Scientist" },
                new Employee() { EmployeeName = "Freemon", EmployeeID = Guid.Parse("7B75097B-BFF2-459F-8EA8-63742BBD7AFB"), Email = "freemon98@gmail.com", Gender = "Male", Position = "Product Manager" },
                new Employee() { EmployeeName = "Oby", EmployeeID = Guid.Parse("6717C42D-16EC-4F15-80D8-4C7413E250CB"), Email = "fbischof6@boston.com", Gender = "Male", Position = "QA Analyst" }
                };

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
            /*List<EmployeeResponse> lists = new List<EmployeeResponse>() { new EmployeeResponse() { Email="anuvindm02@gmail.com",EmployeeID=Guid.NewGuid(),
            EmployeeName="anuvind",Gender=GenderOptions.Male.ToString(), Position="developer"} };
            return lists;*/
            return _employees.Select(employee => employee.ToEmployeeResponse()).ToList();
        }

        public EmployeeResponse GetEmployeeById(Guid? EmployeeId)
        {
            if (EmployeeId == null)
                return null;

            Employee? employee = _employees.FirstOrDefault(emp => emp.EmployeeID == EmployeeId);

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
            Employee? matchingEmployee = _employees.FirstOrDefault(employee => employee.EmployeeID == employeeUpdateRequest.EmployeeID);

            if (matchingEmployee == null)
                throw new ArgumentException("No employee found");

            matchingEmployee.EmployeeName = employeeUpdateRequest?.EmployeeName;
            matchingEmployee.Position = employeeUpdateRequest?.Position;
            matchingEmployee.Email = employeeUpdateRequest?.Email;
            matchingEmployee.Gender = employeeUpdateRequest?.Gender.ToString();

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