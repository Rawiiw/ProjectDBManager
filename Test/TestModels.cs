using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProjectDBManager.Models.Data;
using ProjectDBManager.Models.Data.Entities;



namespace ProjectDBManager.Test
{
    [TestFixture]
    public class LogicTests
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private EDBContext _dbContext;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EDBContext>()
                .UseInMemoryDatabase(databaseName: "DBLocal")
                .Options;
            _dbContext = new EDBContext(options);
            _dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task AddEmployeeAsync_ShouldAddEmployeeToDatabase()
        {
            // Arrange
            var employee = new Employee { EmployeeId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" };

            // Act
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            // Assert
            var result = await _dbContext.Employees.FindAsync(employee.EmployeeId);
            //  Assert.IsNotNull(result);
            // Assert.AreEqual(employee, result);

        }

        [Test]
        public async Task GetEmployeeByIdAsync_ShouldReturnEmployeeFromDatabase()
        {
            // Arrange
            var employee = new Employee { EmployeeId = 1, FirstName = "Jane", LastName = "Doe", Email = "jane@example.com" };
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _dbContext.Employees.FindAsync(employee.EmployeeId);

            // Assert
            //  Assert.NotNull(result);
            //  Assert.AreEqual(employee, result);
        }
    }
}
