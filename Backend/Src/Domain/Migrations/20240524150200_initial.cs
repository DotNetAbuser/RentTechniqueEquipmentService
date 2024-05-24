using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PicturePath = table.Column<string>(type: "text", nullable: false),
                    CostOneHour = table.Column<decimal>(type: "numeric", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CountRentHours = table.Column<int>(type: "integer", nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPayed = table.Column<bool>(type: "boolean", nullable: false),
                    Arrived = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "CostOneHour", "Created", "Description", "Name", "PicturePath", "Updated" },
                values: new object[,]
                {
                    { 1, 1500m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3006), "Мини-погрузчик представляет собой компактное и удобное специализированное строительное оборудование, которое используется для погрузки, разгрузки и перемещения различных грузов на строительных площадках, складах и прочих объектах. Эти машины отличаются маневренностью, удобством управления и возможностью работать в условиях ограниченного пространства.", "Мини-погрузчики", "Files//Images//EquipmentPictures//1.jpg", null },
                    { 2, 3300m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3022), "Экскаваторы-погрузчики - это универсальные машины, которые сочетают в себе функции экскаватора и погрузчика. Они используются для выполнения различных работ на строительных площадках, дорожных работах, при уборке территории и других задачах.", "Экскаваторы-погрузчики", "Files//Images//EquipmentPictures//2.jpg", null },
                    { 3, 3500m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3024), "Бульдозер - это тяжелая техника, предназначенная для перемещения грунта, выравнивания поверхности земли, разрушения сооружений и других работ на строительных площадках.", "Бульдозеры", "Files//Images//EquipmentPictures//3.jpg", null },
                    { 4, 2000m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3026), "Фронтальные погрузчики – это специализированная техника, предназначенная для погрузки и перемещения грузов на строительных площадках, складах, заводах и других объектах. Они отличаются высокой маневренностью, удобством управления и большой грузоподъемностью.", "Фронтальные погрузчики", "Files//Images//EquipmentPictures//4.jpg", null },
                    { 5, 1500m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3028), "Мини-экскаваторы - это компактные и удобные спецтехника, которая применяется для проведения мелких земляных работ, строительства фундаментов, канализации, прокладки трубопроводов и т.д. В Казани можно арендовать мини-экскаваторы различных марок и моделей.", "Мини-экскаваторы", "Files//Images//EquipmentPictures//5.jpg", null },
                    { 6, 4000m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3031), "Телескопические погрузчики – это многофункциональные машины, которые используются для подъема и перемещения грузов на строительных площадках, в складских помещениях и других объектах. Они обладают высокой грузоподъемностью и могут поднимать грузы на значительную высоту.", "Телескопические погрузчики", "Files//Images//EquipmentPictures//6.jpg", null },
                    { 7, 1500m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3033), "Скреперы - это специальная техника для земельных работ, которая предназначена для перемещения грунта, снега, щебня и других материалов. Они широко используются в строительстве, сельском хозяйстве и дорожном строительстве.", "Скреперы", "Files//Images//EquipmentPictures//7.jpg", null },
                    { 8, 2000m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3034), "Автогрейдеры – это специализированная строительная техника, предназначенная для выравнивания поверхности грунта на строительных объектах, дорогах, аэродромах и других объектах инфраструктуры. Они обеспечивают высокую точность и качество работы благодаря специальному оборудованию – рабочему столу с грейдерным отвалом.", "Автогрейдеры", "Files//Images//EquipmentPictures//8.jpg", null },
                    { 9, 3000m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3036), "Гусеничные тракторы - это мощные и надежные машины, которые используются для выполнения различных земельных работ, таких как строительство дорог, планировка местности, рыхление почвы и другие.", "Гусеничные тракторы", "Files//Images//EquipmentPictures//9.jpg", null },
                    { 10, 2500m, new DateTime(2024, 5, 24, 15, 2, 0, 29, DateTimeKind.Utc).AddTicks(3090), "Колесные тракторы - это сельскохозяйственная техника, предназначенная для выполнения различных работ на поле, таких как пахота, посев, вспашка и др. Они оснащены мощным двигателем и колесным приводом, что позволяет им эффективно передвигаться по неровной местности.", "Колесные тракторы", "Files//Images//EquipmentPictures//10.jpg", null }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Created", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 24, 15, 2, 0, 30, DateTimeKind.Utc).AddTicks(2262), "В ожидание", null },
                    { 2, new DateTime(2024, 5, 24, 15, 2, 0, 30, DateTimeKind.Utc).AddTicks(2269), "В работе", null },
                    { 3, new DateTime(2024, 5, 24, 15, 2, 0, 30, DateTimeKind.Utc).AddTicks(2274), "Закончена", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Created", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 24, 15, 2, 0, 30, DateTimeKind.Utc).AddTicks(6322), "Guest", null },
                    { 2, new DateTime(2024, 5, 24, 15, 2, 0, 30, DateTimeKind.Utc).AddTicks(6358), "Operator", null },
                    { 3, new DateTime(2024, 5, 24, 15, 2, 0, 30, DateTimeKind.Utc).AddTicks(6360), "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "RoleId", "Updated" },
                values: new object[,]
                {
                    { new Guid("343acdc7-f2fb-47c5-9731-5ea520108a22"), new DateTime(2024, 5, 24, 15, 2, 0, 31, DateTimeKind.Utc).AddTicks(4701), "bulat1@example.com", "Булат", "Салахиев", "Гость", "$2a$11$yLjJ4G/hwDSPYUD3OgbHOet2.VTeDIRTL8AwUifAHxNYBRBM5FtEW", "+79177793601", 1, null },
                    { new Guid("8f8fbacd-ea00-4dd3-a2f8-3080ad8842d1"), new DateTime(2024, 5, 24, 15, 2, 0, 179, DateTimeKind.Utc).AddTicks(6086), "bulat2@example.com", "Булат", "Салахиев", "Оператор", "$2a$11$MxW78ZHoT5JfLtbgO0ujiuGxG87kuPVCfb9EA9uIoZcjSfOoZ7Y.i", "+79177793602", 2, null },
                    { new Guid("ba73d3bb-bd2d-4e85-8e96-e1384b268878"), new DateTime(2024, 5, 24, 15, 2, 0, 328, DateTimeKind.Utc).AddTicks(5886), "bulat3@example.com", "Булат", "Салахиев", "Админ", "$2a$11$Mf94eyDiGFQtNv7/FDq47umDPAwBgRVXl3aSk7WyRSYxFJtAiU/N2", "+79177793603", 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Description",
                table: "Equipments",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Name",
                table: "Equipments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EquipmentId",
                table: "Orders",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_Name",
                table: "OrderStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
