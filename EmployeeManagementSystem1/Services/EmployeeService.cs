using EmployeeManagementSystem1.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem1.Models.Responses;
using EmployeeManagementSystem1.Models.DTOs;
using EmployeeManagementSystem1.Models;
using EmployeeManagementSystem1.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem1.Services;


public interface IEmployeeService
{
    Task<GetEmployeesResponse> GetEmployees();
    Task<BaseResponse> AddEmployee(AddEmployeeForm form, string userID);
    Task<GetEmployeeResponse> GetEmployee(int id);
    Task<BaseResponse> DeleteEmployee(Employee employee);
    Task<BaseResponse> EditEmployee(Employee employee);
    Task<List<AuditLog>> GetAuditLogsAsync();
}

public class EmployeeService : IEmployeeService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IDbContextFactory<DataContext> _factory;

    public EmployeeService(UserManager<IdentityUser> userManager, IDbContextFactory<DataContext> factory)
    {
        _userManager = userManager;
        _factory = factory;
    }

    public async Task<BaseResponse> AddEmployee(AddEmployeeForm form, string userId)
    {
        var response = new BaseResponse();
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (!await _userManager.IsInRoleAsync(user, "Manager"))
            {
                response.StatusCode = 403; // Forbidden
                response.Message = "Unauthorized to add employee";
                return response;
            }


            using (var context = _factory.CreateDbContext())
            {
                context.Add(new Employee
                {
                    Name = form.Name,
                    Position = form.Position,
                    Salary = form.Salary,
                    Type = form.Type,
                    ImgUrl = form.ImgUrl,
                });


                var auditLog = new AuditLog
                {
                    Action = "Add Employee",
                    UserName = "Manager",
                    Timestamp = DateTime.UtcNow,
                    Details = $"Added a new employee: {form.Name}"
                };

                context.AuditLogs.Add(auditLog);

                var result = await context.SaveChangesAsync();

                if (result == 2)
                {
                    response.StatusCode = 200;
                    response.Message = "Employee added successfully";
                }
                else
                {
                    response.StatusCode = 400;
                    response.Message = "Error occurred while adding the employee";
                }
            }


        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error adding employee:" + ex.Message;
        }

        return response;
    }

    public async Task<GetEmployeesResponse> GetEmployees()
    {
        var response = new GetEmployeesResponse();
        try
        {
            using (var context = _factory.CreateDbContext())
            {
                var employees = context.Employees.ToList();
                response.StatusCode = 200;
                response.Message = "Success";
                response.Employees = employees;
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error retrieving employees: " + ex.Message;
            response.Employees = null;
        }

        return response;
    }
    public async Task<BaseResponse> EditEmployee(Employee employee)
    {
        var response = new BaseResponse();
        try
        {
            using (var context = _factory.CreateDbContext())
            {
                context.Update(employee);

                var auditLog = new AuditLog
                {
                    //for now user
                    Action = "Edit Employee",
                    UserName = "Manager",
                    Timestamp = DateTime.UtcNow,
                    Details = $"Edited employee: {employee.Name}"
                };
                context.AuditLogs.Add(auditLog);

                var result = await context.SaveChangesAsync();

                if (result == 2)
                {
                    response.StatusCode = 200;
                    response.Message = "Employee updated successfully";
                }
                else
                {
                    response.StatusCode = 400;
                    response.Message = "Error occurred while updating the employee.";
                }
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error updating employee: " + ex.Message;
        }

        return response;
    }

    public async Task<GetEmployeeResponse> GetEmployee(int id)
    {
        var response = new GetEmployeeResponse();
        try
        {
            using (var context = _factory.CreateDbContext())
            {
                var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                response.StatusCode = 200;
                response.Message = "Success";
                response.Employee = employee;
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error retrieving employee: " + ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse> DeleteEmployee(Employee employee)
    {
        var response = new BaseResponse();
        try
        {
            using (var context = _factory.CreateDbContext())
            {
               
                context.Remove(employee);

                var auditLog = new AuditLog
                {
                    //for now user
                    Action = "Delete Employee",
                    UserName = "Manager",
                    Timestamp = DateTime.UtcNow,
                    Details = $"Deleted employee: {employee.Name}"
                };
                context.AuditLogs.Add(auditLog);

                var result = await context.SaveChangesAsync();

                if (result == 2)
                {
                    response.StatusCode = 204;
                    response.Message = "Employee removed successfully";
                }
                else
                {
                    response.StatusCode = 400;
                    response.Message = "Error occurred while removing the employee.";
                }
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error removing employee: " + ex.Message;
        }

        return response;
    }

    public async Task<List<AuditLog>> GetAuditLogsAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            return await context.AuditLogs.OrderByDescending(log => log.Timestamp).ToListAsync();
        }
    }

}
