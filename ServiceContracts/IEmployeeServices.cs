using ServiceContracts.DTO;

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
    }
}