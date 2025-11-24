using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitilaCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    ActivityLevel = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    Preferences = table.Column<int>(type: "int", nullable: false),
                    NumberOfMeals = table.Column<int>(type: "int", nullable: false),
                    Target_DailyKcal = table.Column<double>(type: "float", nullable: true),
                    Target_DailyProtein = table.Column<double>(type: "float", nullable: true),
                    Target_DailyCarbs = table.Column<double>(type: "float", nullable: true),
                    Target_DailyFat = table.Column<double>(type: "float", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardServingSizeInGrams = table.Column<double>(type: "float", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    Kcal = table.Column<double>(type: "float", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Carbs = table.Column<double>(type: "float", nullable: false),
                    Fat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    KcalPer100g = table.Column<double>(type: "float", nullable: false),
                    ProteinPer100g = table.Column<double>(type: "float", nullable: false),
                    CarbsPer100g = table.Column<double>(type: "float", nullable: false),
                    FatPer100g = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalKcal = table.Column<double>(type: "float", nullable: false),
                    TotalProtein = table.Column<double>(type: "float", nullable: false),
                    TotalCarbs = table.Column<double>(type: "float", nullable: false),
                    TotalFat = table.Column<double>(type: "float", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyLogs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantityInGrams = table.Column<double>(type: "float", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodIngredients_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyLogMeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalCalories = table.Column<int>(type: "int", nullable: false),
                    MealName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyLogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyLogMeals_DailyLogs_DailyLogId",
                        column: x => x.DailyLogId,
                        principalTable: "DailyLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyLogRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumptionMultiplier = table.Column<double>(type: "float", nullable: false),
                    QuantityConsumedInGrams = table.Column<double>(type: "float", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    DailyLogMealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyLogRecipes_DailyLogMeals_DailyLogMealId",
                        column: x => x.DailyLogMealId,
                        principalTable: "DailyLogMeals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyLogRecipes_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Carbs", "Description", "Fat", "ImageUrl", "IsVegan", "IsVegetarian", "Kcal", "Name", "Protein", "StandardServingSizeInGrams" },
                values: new object[,]
                {
                    { 101, 1.3999999999999999, "Mic dejun clasic cu două ouă bătute și prăjite în ulei de măsline. O opțiune rapidă și bogată în proteine pentru a începe ziua.", 19.0, "reteta1.jpg", false, true, 286.0, "Omletă simplă", 25.199999999999999, 130.0 },
                    { 102, 42.700000000000003, "O masă de bază pentru sportivi. Piept de pui fraged, preparat simplu la grătar sau fiert, servit alături de o porție generoasă de orez alb fiert.", 5.2000000000000002, "reteta2.jpg", false, false, 425.0, "Pui și Orez ", 38.700000000000003, 350.0 },
                    { 103, 7.7999999999999998, "O salată proaspătă și sățioasă, cu bucăți de ton în apă, roșii proaspete tăiate cubulețe și un strop de ulei de măsline extravirgin.", 12.0, "reteta3.jpg", false, false, 260.0, "Salată de Ton", 26.399999999999999, 205.0 },
                    { 104, 50.0, "Gustarea clasică, perfectă pentru un plus de energie. Două felii de pâine integrală unse cu un strat generos de unt de arahide cremos.", 15.0, "reteta4.jpg", true, true, 350.0, "Pâine cu Unt de Arahide", 16.0, 125.0 },
                    { 105, 27.0, "Un bol cremos de iaurt grecesc 2%, bogat în proteine, amestecat cu o banană proaspătă tăiată felii. Ideal pentru mic dejun sau o gustare rapidă.", 4.5, "reteta5.jpg", false, true, 200.0, "Iaurt Grecesc cu Banane", 14.5, 250.0 },
                    { 106, 45.0, "O tortilla integrală umplută cu o varietate de legume proaspete (salată, roșii, ardei) și o bază de hummus. Ideal pentru vegetarieni și vegani.", 10.0, "reteta6.jpg", true, true, 300.0, "Wrap Vegetarian", 8.0, 200.0 },
                    { 107, 7.0, "O masă curată, bogată în proteine și fibre. Piept de pui la grătar servit alături de buchețele de broccoli preparate la abur sau trase ușor la tigaie.", 15.0, "reteta7.jpg", false, false, 320.0, "Pui și Broccoli", 35.0, 270.0 },
                    { 108, 90.0, "O porție consistentă de chili vegan, plină de fasole neagră, porumb și condimente, servită peste un pat de orez fiert. O masă completă și sățioasă.", 12.0, "reteta8.jpg", true, true, 610.0, "Chili Vegetarian (mare)", 35.0, 450.0 },
                    { 109, 40.0, "Un wrap gigant pentru cei care au nevoie de proteine. O tortilla mare umplută cu 200g de piept de pui, mozzarella topită și felii de avocado.", 38.0, "reteta9.jpg", false, false, 750.0, "Wrap Proteic XXL", 65.0, 350.0 },
                    { 110, 40.0, "Un mic dejun sau prânz mediteranean sățios. Conține ouă fierte, roșii proaspete, avocado cremos și un strop de ulei de măsline.", 36.0, "reteta10.jpg", false, true, 660.0, "Bol Grecesc cu Ouă", 38.0, 400.0 },
                    { 111, 70.0, "O salată vegană plină de nutrienți. Bază de quinoa fiartă, năut, roșii, castraveți și o vinegretă simplă de lămâie și ulei de măsline.", 20.0, "reteta11.jpg", true, true, 560.0, "Salată Quinoa și Năut", 22.0, 410.0 },
                    { 112, 30.0, "Gustarea crocantă și dulce-sărată perfectă. Un măr proaspăt, tăiat felii, servit cu o porție de unt de arahide pentru dipp.", 16.0, "reteta12.jpg", true, true, 280.0, "Măr cu Unt de Arahide", 7.0, 175.0 },
                    { 113, 18.0, "O gustare vegană, preparată peste noapte. Semințe de chia înmuiate în lapte de migdale (sau alt lapte vegetal) până devin o budincă gelatinoasă.", 13.0, "reteta13.jpg", true, true, 210.0, "Budincă de Chia", 6.0, 150.0 },
                    { 114, 17.0, "Gustare vegană clasică, bogată în fibre și vitamine. Hummus cremos din năut, servit cu bastonașe de morcov proaspăt.", 8.0, "reteta14.jpg", true, true, 170.0, "Hummus cu Morcovi", 5.0, 150.0 },
                    { 115, 46.0, "Un mic dejun sau o gustare rapidă, emblematică. O felie de pâine integrală prăjită, acoperită cu avocado pasat și un praf de sare și piper.", 8.0, "reteta15.jpg", true, true, 295.0, "Avocado Toast", 14.0, 130.0 },
                    { 116, 6.0, "O salată italiană clasică, proaspătă și ușoară. Conține doar roșii coapte, mozzarella proaspătă (low-fat) și un strop de ulei de măsline.", 24.0, "reteta16.jpg", false, true, 356.0, "Salată Caprese Simplă", 29.0, 210.0 },
                    { 117, 75.0, "O porție decadentă de paste clasice, preparate cu ou, bacon (guanciale sau pancetta) și o cantitate generoasă de smântână pentru gătit.", 55.0, "reteta17.jpg", false, false, 950.0, "Paste Carbonara", 45.0, 350.0 },
                    { 118, 11.0, "O salată cu foarte puțini carbohidrați, dar extrem de sățioasă. Conține ouă fierte, avocado, bacon prăjit și bucăți de mozzarella, totul pe un pat de verdeață.", 52.0, "reteta18.jpg", false, false, 698.0, "Salată Keto", 47.0, 300.0 }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CarbsPer100g", "FatPer100g", "ImageUrl", "IsVegan", "IsVegetarian", "KcalPer100g", "Name", "ProteinPer100g" },
                values: new object[,]
                {
                    { 1, 0.69999999999999996, 9.5, "aliment1.jpg", false, true, 143.0, "Ou (buc)", 12.6 },
                    { 2, 0.0, 3.6000000000000001, "aliment2.jpg", false, false, 165.0, "Piept de pui (crud)", 31.0 },
                    { 3, 28.0, 0.29999999999999999, "aliment3.jpg", true, true, 130.0, "Orez (fiert)", 2.7000000000000002 },
                    { 4, 6.5999999999999996, 0.40000000000000002, "aliment4.jpg", true, true, 34.0, "Broccoli", 2.7999999999999998 },
                    { 5, 3.8999999999999999, 0.20000000000000001, "aliment5.jpg", true, true, 18.0, "Roșii", 0.90000000000000002 },
                    { 6, 0.0, 100.0, "aliment6.jpg", true, true, 884.0, "Ulei de măsline", 0.0 },
                    { 7, 43.5, 3.5, "aliment7.jpg", true, true, 247.0, "Pâine integrală", 13.0 },
                    { 8, 20.0, 50.0, "aliment8.jpg", true, true, 588.0, "Unt de arahide", 25.0 },
                    { 9, 23.0, 0.29999999999999999, "aliment9.jpg", true, true, 89.0, "Banane", 1.1000000000000001 },
                    { 10, 0.0, 0.80000000000000004, "aliment10.jpg", false, false, 116.0, "Ton în apă", 25.5 },
                    { 11, 4.0, 2.0, "aliment11.jpg", false, true, 73.0, "Iaurt grecesc (2%)", 10.0 },
                    { 12, 8.5, 14.0, "aliment12.jpg", true, true, 160.0, "Avocado", 2.0 },
                    { 13, 23.699999999999999, 0.5, "aliment13.jpg", true, true, 139.0, "Fasole neagră (conservă)", 8.9000000000000004 },
                    { 14, 2.2000000000000002, 14.0, "aliment14.jpg", false, true, 250.0, "Mozzarella (low-fat)", 28.0 },
                    { 15, 21.0, 1.8999999999999999, "aliment15.jpg", true, true, 120.0, "Quinoa (fiartă)", 4.4000000000000004 },
                    { 16, 27.0, 2.6000000000000001, "aliment16.jpg", true, true, 164.0, "Năut (conservă)", 8.9000000000000004 },
                    { 17, 42.0, 31.0, "aliment17.jpg", true, true, 486.0, "Semințe de Chia", 17.0 },
                    { 18, 14.0, 0.20000000000000001, "aliment18.jpg", true, true, 52.0, "Măr", 0.29999999999999999 },
                    { 19, 22.0, 50.0, "aliment19.jpg", true, true, 579.0, "Migdale", 21.0 },
                    { 20, 3.0, 1.0, "aliment20.jpg", false, true, 72.0, "Brânză Cottage (degresată)", 12.0 },
                    { 21, 9.5999999999999996, 0.20000000000000001, "aliment21.jpg", true, true, 41.0, "Morcov", 0.90000000000000002 },
                    { 22, 3.1000000000000001, 30.0, "aliment22.jpg", false, true, 292.0, "Smântână (30%)", 2.5 },
                    { 23, 75.0, 1.5, "aliment23.jpg", true, true, 371.0, "Paste (uscate)", 13.0 },
                    { 24, 1.5, 42.0, "aliment24.jpg", false, false, 541.0, "Bacon", 37.0 }
                });

            migrationBuilder.InsertData(
                table: "FoodIngredients",
                columns: new[] { "Id", "FoodId", "IngredientId", "QuantityInGrams" },
                values: new object[,]
                {
                    { 1, 101, 1, 120.0 },
                    { 2, 101, 6, 10.0 },
                    { 3, 102, 2, 150.0 },
                    { 4, 102, 3, 200.0 },
                    { 5, 103, 10, 100.0 },
                    { 6, 103, 5, 100.0 },
                    { 7, 103, 6, 5.0 },
                    { 8, 104, 7, 100.0 },
                    { 9, 104, 8, 25.0 },
                    { 10, 105, 11, 150.0 },
                    { 11, 105, 9, 100.0 },
                    { 12, 107, 2, 120.0 },
                    { 13, 107, 4, 150.0 },
                    { 14, 108, 13, 250.0 },
                    { 15, 108, 3, 200.0 },
                    { 16, 109, 2, 200.0 },
                    { 17, 109, 14, 100.0 },
                    { 18, 109, 12, 50.0 },
                    { 19, 110, 1, 150.0 },
                    { 20, 110, 5, 150.0 },
                    { 21, 110, 12, 100.0 },
                    { 22, 111, 15, 150.0 },
                    { 23, 111, 16, 100.0 },
                    { 24, 111, 5, 150.0 },
                    { 25, 111, 6, 10.0 },
                    { 26, 112, 18, 150.0 },
                    { 27, 112, 8, 25.0 },
                    { 28, 113, 17, 40.0 },
                    { 34, 114, 16, 50.0 },
                    { 35, 114, 21, 100.0 },
                    { 36, 115, 7, 100.0 },
                    { 37, 115, 12, 30.0 },
                    { 38, 116, 5, 100.0 },
                    { 39, 116, 14, 100.0 },
                    { 40, 116, 6, 10.0 },
                    { 41, 117, 23, 100.0 },
                    { 42, 117, 24, 50.0 },
                    { 43, 117, 1, 60.0 },
                    { 44, 117, 22, 100.0 },
                    { 45, 118, 1, 100.0 },
                    { 46, 118, 12, 100.0 },
                    { 47, 118, 24, 50.0 },
                    { 48, 118, 14, 50.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogMeals_DailyLogId",
                table: "DailyLogMeals",
                column: "DailyLogId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogRecipes_DailyLogMealId",
                table: "DailyLogRecipes",
                column: "DailyLogMealId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogRecipes_FoodId",
                table: "DailyLogRecipes",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogs_ApplicationUserId",
                table: "DailyLogs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredients_FoodId",
                table: "FoodIngredients",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredients_IngredientId",
                table: "FoodIngredients",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DailyLogRecipes");

            migrationBuilder.DropTable(
                name: "FoodIngredients");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DailyLogMeals");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "DailyLogs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
