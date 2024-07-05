namespace EmployeeManagementSystem1.Models.Responses
{
    public class GetEmployeesResponse: BaseResponse
    {
        public List<Employee>? Employees { get; set; }
    }
}
