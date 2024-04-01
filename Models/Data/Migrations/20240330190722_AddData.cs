using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectDBManager.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Email", "FirstName", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1, "sk464656@gmail.com", "Sasha", "Kosinova", "Igorevna" },
                    { 2, "vanya@example.com", "Ivan", "Popov", "Sergeevich" },
                    { 3, "borya@example.com", "Boris", "Cherniy", "Petrovich" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "CustomerCompanyName", "DirectorId", "EmployeeId", "EndDate", "ExecutorCompanyName", "Name", "Priority", "StartDate" },
                values: new object[,]
                {
                    { 1, "Sber", 1, null, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Galera", "  Sybers", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Sber", 2, null, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "IDK", "Uralski Pelmeni", 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Ozon", 3, null, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wildberries", "Amazon", 3, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ProjectEmployee",
                columns: new[] { "EmployeeId", "ProjectId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 2, 0 },
                    { 3, 3, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectEmployee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProjectEmployee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProjectEmployee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 3);
        }
    }
}
