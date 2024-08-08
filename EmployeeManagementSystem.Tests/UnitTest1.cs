using Moq;
using EmployeeManagementSystem1.Services; 
using EmployeeManagementSystem1.Data; 
using Xunit;
using Microsoft.EntityFrameworkCore; // Using xUnit for testing

namespace EmployeeManagementSystemTests
{
    public class UnitTest1
    {
        private readonly Mock<IDbContextFactory<DataContext>> _mockContextFactory;
        private readonly DataContext _mockContext;
        private readonly EmployeeService _employeeService;

        public UnitTest1()
        {
            
        }

        [Fact]
        public void TestAddEmployee()
        {
          
        }
    }
}
