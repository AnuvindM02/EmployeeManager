using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    public interface IEmployeeServices
    {
        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employeeAddRequest"></param>
        /// <returns>EmployeeResponse Class</returns>
        EmployeeResponse AddEmployee(EmployeeAddRequest? employeeAddRequest);

        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns>List of EmployeeResponse Class</returns>
        List<EmployeeResponse> GetAllEmployees();

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employeeUpdateRequest"></param>
        /// <returns>EmployeeResponse Class</returns>
        EmployeeResponse UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest);

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns>Boolean</returns>
        bool DeleteEmployee(Guid? EmployeeId);

        /// <summary>
        /// Find employee by EmployeeID
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns>EmployeeResponse Class</returns>
        EmployeeResponse GetEmployeeById(Guid? EmployeeId);

        /// <summary>
        /// Get filtered employees list
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="searchstring"></param>
        /// <returns>List of EmployeeResponse</returns>
        List<EmployeeResponse> GetFilteredEmployees(string searchBy, string? searchstring);

        /// <summary>
        /// Get sorted employees list
        /// </summary>
        /// <param name="allEmployees"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        List<EmployeeResponse> GetSortedEmployees(List<EmployeeResponse> allEmployees, string sortBy, SortOrderOptions sortOrder);
    }
}