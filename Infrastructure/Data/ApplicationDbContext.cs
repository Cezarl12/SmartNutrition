using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<FoodIngredient> FoodIngredients { get; set; }
        public DbSet<DailyLog> DailyLogs { get; set; }
        public DbSet<DailyLogMeal> DailyLogMeals { get; set; }
        public DbSet<DailyLogRecipe> DailyLogRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().OwnsOne(u => u.Target);

            builder.Entity<FoodIngredient>()
              .HasOne(fi => fi.Food)
              .WithMany(f => f.Ingredients)
              .HasForeignKey(fi => fi.FoodId);

            builder.Entity<FoodIngredient>()
              .HasOne(fi => fi.Ingredient)
              .WithMany()
              .HasForeignKey(fi => fi.IngredientId);


            builder.Entity<Ingredient>().HasData(
              new Ingredient { Id = 1, Name = "Ou (buc)", ImageUrl = "aliment1.jpg", IsVegan = false, IsVegetarian = true, KcalPer100g = 143, ProteinPer100g = 12.6, CarbsPer100g = 0.7, FatPer100g = 9.5 },
              new Ingredient { Id = 2, Name = "Piept de pui (crud)", ImageUrl = "aliment2.jpg", IsVegan = false, IsVegetarian = false, KcalPer100g = 165, ProteinPer100g = 31, CarbsPer100g = 0, FatPer100g = 3.6 },
              new Ingredient { Id = 3, Name = "Orez (fiert)", ImageUrl = "aliment3.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 130, ProteinPer100g = 2.7, CarbsPer100g = 28, FatPer100g = 0.3 },
              new Ingredient { Id = 4, Name = "Broccoli", ImageUrl = "aliment4.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 34, ProteinPer100g = 2.8, CarbsPer100g = 6.6, FatPer100g = 0.4 },
              new Ingredient { Id = 5, Name = "Roșii", ImageUrl = "aliment5.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 18, ProteinPer100g = 0.9, CarbsPer100g = 3.9, FatPer100g = 0.2 },
              new Ingredient { Id = 6, Name = "Ulei de măsline", ImageUrl = "aliment6.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 884, ProteinPer100g = 0, CarbsPer100g = 0, FatPer100g = 100 },
              new Ingredient { Id = 7, Name = "Pâine integrală", ImageUrl = "aliment7.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 247, ProteinPer100g = 13, CarbsPer100g = 43.5, FatPer100g = 3.5 },
              new Ingredient { Id = 8, Name = "Unt de arahide", ImageUrl = "aliment8.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 588, ProteinPer100g = 25, CarbsPer100g = 20, FatPer100g = 50 },
              new Ingredient { Id = 9, Name = "Banane", ImageUrl = "aliment9.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 89, ProteinPer100g = 1.1, CarbsPer100g = 23, FatPer100g = 0.3 },
              new Ingredient { Id = 10, Name = "Ton în apă", ImageUrl = "aliment10.jpg", IsVegan = false, IsVegetarian = false, KcalPer100g = 116, ProteinPer100g = 25.5, CarbsPer100g = 0, FatPer100g = 0.8 },
              new Ingredient { Id = 11, Name = "Iaurt grecesc (2%)", ImageUrl = "aliment11.jpg", IsVegan = false, IsVegetarian = true, KcalPer100g = 73, ProteinPer100g = 10, CarbsPer100g = 4, FatPer100g = 2 },
              new Ingredient { Id = 12, Name = "Avocado", ImageUrl = "aliment12.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 160, ProteinPer100g = 2, CarbsPer100g = 8.5, FatPer100g = 14 },
              new Ingredient { Id = 13, Name = "Fasole neagră (conservă)", ImageUrl = "aliment13.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 139, ProteinPer100g = 8.9, CarbsPer100g = 23.7, FatPer100g = 0.5 },
              new Ingredient { Id = 14, Name = "Mozzarella (low-fat)", ImageUrl = "aliment14.jpg", IsVegan = false, IsVegetarian = true, KcalPer100g = 250, ProteinPer100g = 28, CarbsPer100g = 2.2, FatPer100g = 14 },
              new Ingredient { Id = 15, Name = "Quinoa (fiartă)", ImageUrl = "aliment15.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 120, ProteinPer100g = 4.4, CarbsPer100g = 21, FatPer100g = 1.9 },
              new Ingredient { Id = 16, Name = "Năut (conservă)", ImageUrl = "aliment16.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 164, ProteinPer100g = 8.9, CarbsPer100g = 27, FatPer100g = 2.6 },
              new Ingredient { Id = 17, Name = "Semințe de Chia", ImageUrl = "aliment17.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 486, ProteinPer100g = 17, CarbsPer100g = 42, FatPer100g = 31 },
              new Ingredient { Id = 18, Name = "Măr", ImageUrl = "aliment18.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 52, ProteinPer100g = 0.3, CarbsPer100g = 14, FatPer100g = 0.2 },
              new Ingredient { Id = 19, Name = "Migdale", ImageUrl = "aliment19.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 579, ProteinPer100g = 21, CarbsPer100g = 22, FatPer100g = 50 },
              new Ingredient { Id = 20, Name = "Brânză Cottage (degresată)", ImageUrl = "aliment20.jpg", IsVegan = false, IsVegetarian = true, KcalPer100g = 72, ProteinPer100g = 12, CarbsPer100g = 3, FatPer100g = 1 },
              new Ingredient { Id = 21, Name = "Morcov", ImageUrl = "aliment21.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 41, ProteinPer100g = 0.9, CarbsPer100g = 9.6, FatPer100g = 0.2 },
              new Ingredient { Id = 22, Name = "Smântână (30%)", ImageUrl = "aliment22.jpg", IsVegan = false, IsVegetarian = true, KcalPer100g = 292, ProteinPer100g = 2.5, CarbsPer100g = 3.1, FatPer100g = 30 },
              new Ingredient { Id = 23, Name = "Paste (uscate)", ImageUrl = "aliment23.jpg", IsVegan = true, IsVegetarian = true, KcalPer100g = 371, ProteinPer100g = 13, CarbsPer100g = 75, FatPer100g = 1.5 },
              new Ingredient { Id = 24, Name = "Bacon", ImageUrl = "aliment24.jpg", IsVegan = false, IsVegetarian = false, KcalPer100g = 541, ProteinPer100g = 37, CarbsPer100g = 1.5, FatPer100g = 42 }
            );

            builder.Entity<Food>().HasData(
            new Food { Id = 101, Name = "Omletă simplă", Description = "Mic dejun clasic cu două ouă bătute și prăjite în ulei de măsline. O opțiune rapidă și bogată în proteine pentru a începe ziua.", ImageUrl = "reteta1.jpg", IsVegan = false, IsVegetarian = true, Kcal = 286, Protein = 25.2, Carbs = 1.4, Fat = 19.0, StandardServingSizeInGrams = 130 },
            new Food { Id = 102, Name = "Pui și Orez ", Description = "O masă de bază pentru sportivi. Piept de pui fraged, preparat simplu la grătar sau fiert, servit alături de o porție generoasă de orez alb fiert.", ImageUrl = "reteta2.jpg", IsVegan = false, IsVegetarian = false, Kcal = 425, Protein = 38.7, Carbs = 42.7, Fat = 5.2, StandardServingSizeInGrams = 350 },
            new Food { Id = 103, Name = "Salată de Ton", Description = "O salată proaspătă și sățioasă, cu bucăți de ton în apă, roșii proaspete tăiate cubulețe și un strop de ulei de măsline extravirgin.", ImageUrl = "reteta3.jpg", IsVegan = false, IsVegetarian = false, Kcal = 260, Protein = 26.4, Carbs = 7.8, Fat = 12.0, StandardServingSizeInGrams = 205 },
            new Food { Id = 104, Name = "Pâine cu Unt de Arahide", Description = "Gustarea clasică, perfectă pentru un plus de energie. Două felii de pâine integrală unse cu un strat generos de unt de arahide cremos.", ImageUrl = "reteta4.jpg", IsVegan = true, IsVegetarian = true, Kcal = 350, Protein = 16, Carbs = 50, Fat = 15, StandardServingSizeInGrams = 125 },
            new Food { Id = 105, Name = "Iaurt Grecesc cu Banane", Description = "Un bol cremos de iaurt grecesc 2%, bogat în proteine, amestecat cu o banană proaspătă tăiată felii. Ideal pentru mic dejun sau o gustare rapidă.", ImageUrl = "reteta5.jpg", IsVegan = false, IsVegetarian = true, Kcal = 200, Protein = 14.5, Carbs = 27, Fat = 4.5, StandardServingSizeInGrams = 250 },
            new Food { Id = 106, Name = "Wrap Vegetarian", Description = "O tortilla integrală umplută cu o varietate de legume proaspete (salată, roșii, ardei) și o bază de hummus. Ideal pentru vegetarieni și vegani.", ImageUrl = "reteta6.jpg", IsVegan = true, IsVegetarian = true, Kcal = 300, Protein = 8, Carbs = 45, Fat = 10, StandardServingSizeInGrams = 200 },
            new Food { Id = 107, Name = "Pui și Broccoli", Description = "O masă curată, bogată în proteine și fibre. Piept de pui la grătar servit alături de buchețele de broccoli preparate la abur sau trase ușor la tigaie.", ImageUrl = "reteta7.jpg", IsVegan = false, IsVegetarian = false, Kcal = 320, Protein = 35, Carbs = 7, Fat = 15, StandardServingSizeInGrams = 270 },
            new Food { Id = 108, Name = "Chili Vegetarian (mare)", Description = "O porție consistentă de chili vegan, plină de fasole neagră, porumb și condimente, servită peste un pat de orez fiert. O masă completă și sățioasă.", ImageUrl = "reteta8.jpg", IsVegan = true, IsVegetarian = true, Kcal = 610, Protein = 35, Carbs = 90, Fat = 12, StandardServingSizeInGrams = 450 },
            new Food { Id = 109, Name = "Wrap Proteic XXL", Description = "Un wrap gigant pentru cei care au nevoie de proteine. O tortilla mare umplută cu 200g de piept de pui, mozzarella topită și felii de avocado.", ImageUrl = "reteta9.jpg", IsVegan = false, IsVegetarian = false, Kcal = 750, Protein = 65, Carbs = 40, Fat = 38, StandardServingSizeInGrams = 350 },
            new Food { Id = 110, Name = "Bol Grecesc cu Ouă", Description = "Un mic dejun sau prânz mediteranean sățios. Conține ouă fierte, roșii proaspete, avocado cremos și un strop de ulei de măsline.", ImageUrl = "reteta10.jpg", IsVegan = false, IsVegetarian = true, Kcal = 660, Protein = 38, Carbs = 40, Fat = 36, StandardServingSizeInGrams = 400 },
            new Food { Id = 111, Name = "Salată Quinoa și Năut", Description = "O salată vegană plină de nutrienți. Bază de quinoa fiartă, năut, roșii, castraveți și o vinegretă simplă de lămâie și ulei de măsline.", ImageUrl = "reteta11.jpg", IsVegan = true, IsVegetarian = true, Kcal = 560, Protein = 22, Carbs = 70, Fat = 20, StandardServingSizeInGrams = 410 },
            new Food { Id = 112, Name = "Măr cu Unt de Arahide", Description = "Gustarea crocantă și dulce-sărată perfectă. Un măr proaspăt, tăiat felii, servit cu o porție de unt de arahide pentru dipp.", ImageUrl = "reteta12.jpg", IsVegan = true, IsVegetarian = true, Kcal = 280, Protein = 7, Carbs = 30, Fat = 16, StandardServingSizeInGrams = 175 },
            new Food { Id = 113, Name = "Budincă de Chia", Description = "O gustare vegană, preparată peste noapte. Semințe de chia înmuiate în lapte de migdale (sau alt lapte vegetal) până devin o budincă gelatinoasă.", ImageUrl = "reteta13.jpg", IsVegan = true, IsVegetarian = true, Kcal = 210, Protein = 6, Carbs = 18, Fat = 13, StandardServingSizeInGrams = 150 },
            new Food { Id = 114, Name = "Hummus cu Morcovi", Description = "Gustare vegană clasică, bogată în fibre și vitamine. Hummus cremos din năut, servit cu bastonașe de morcov proaspăt.", ImageUrl = "reteta14.jpg", IsVegan = true, IsVegetarian = true, Kcal = 170, Protein = 5, Carbs = 17, Fat = 8, StandardServingSizeInGrams = 150 },
            new Food { Id = 115, Name = "Avocado Toast", Description = "Un mic dejun sau o gustare rapidă, emblematică. O felie de pâine integrală prăjită, acoperită cu avocado pasat și un praf de sare și piper.", ImageUrl = "reteta15.jpg", IsVegan = true, IsVegetarian = true, Kcal = 295, Protein = 14, Carbs = 46, Fat = 8, StandardServingSizeInGrams = 130 },
            new Food { Id = 116, Name = "Salată Caprese Simplă", Description = "O salată italiană clasică, proaspătă și ușoară. Conține doar roșii coapte, mozzarella proaspătă (low-fat) și un strop de ulei de măsline.", ImageUrl = "reteta16.jpg", IsVegan = false, IsVegetarian = true, Kcal = 356, Protein = 29, Carbs = 6, Fat = 24, StandardServingSizeInGrams = 210 },
            new Food { Id = 117, Name = "Paste Carbonara", Description = "O porție decadentă de paste clasice, preparate cu ou, bacon (guanciale sau pancetta) și o cantitate generoasă de smântână pentru gătit.", ImageUrl = "reteta17.jpg", IsVegan = false, IsVegetarian = false, Kcal = 950, Protein = 45, Carbs = 75, Fat = 55, StandardServingSizeInGrams = 350 },
            new Food { Id = 118, Name = "Salată Keto", Description = "O salată cu foarte puțini carbohidrați, dar extrem de sățioasă. Conține ouă fierte, avocado, bacon prăjit și bucăți de mozzarella, totul pe un pat de verdeață.", ImageUrl = "reteta18.jpg", IsVegan = false, IsVegetarian = false, Kcal = 698, Protein = 47, Carbs = 11, Fat = 52, StandardServingSizeInGrams = 300 }
          );

            builder.Entity<FoodIngredient>().HasData(
              new FoodIngredient { Id = 1, FoodId = 101, IngredientId = 1, QuantityInGrams = 120 },
              new FoodIngredient { Id = 2, FoodId = 101, IngredientId = 6, QuantityInGrams = 10 },
              new FoodIngredient { Id = 3, FoodId = 102, IngredientId = 2, QuantityInGrams = 150 },
              new FoodIngredient { Id = 4, FoodId = 102, IngredientId = 3, QuantityInGrams = 200 },
              new FoodIngredient { Id = 5, FoodId = 103, IngredientId = 10, QuantityInGrams = 100 },
              new FoodIngredient { Id = 6, FoodId = 103, IngredientId = 5, QuantityInGrams = 100 },
              new FoodIngredient { Id = 7, FoodId = 103, IngredientId = 6, QuantityInGrams = 5 },
              new FoodIngredient { Id = 8, FoodId = 104, IngredientId = 7, QuantityInGrams = 100 },
              new FoodIngredient { Id = 9, FoodId = 104, IngredientId = 8, QuantityInGrams = 25 },
              new FoodIngredient { Id = 10, FoodId = 105, IngredientId = 11, QuantityInGrams = 150 },
              new FoodIngredient { Id = 11, FoodId = 105, IngredientId = 9, QuantityInGrams = 100 },
              new FoodIngredient { Id = 12, FoodId = 107, IngredientId = 2, QuantityInGrams = 120 },
              new FoodIngredient { Id = 13, FoodId = 107, IngredientId = 4, QuantityInGrams = 150 },
              new FoodIngredient { Id = 14, FoodId = 108, IngredientId = 13, QuantityInGrams = 250 },
              new FoodIngredient { Id = 15, FoodId = 108, IngredientId = 3, QuantityInGrams = 200 },
              new FoodIngredient { Id = 16, FoodId = 109, IngredientId = 2, QuantityInGrams = 200 },
              new FoodIngredient { Id = 17, FoodId = 109, IngredientId = 14, QuantityInGrams = 100 },
              new FoodIngredient { Id = 18, FoodId = 109, IngredientId = 12, QuantityInGrams = 50 },
              new FoodIngredient { Id = 19, FoodId = 110, IngredientId = 1, QuantityInGrams = 150 },
              new FoodIngredient { Id = 20, FoodId = 110, IngredientId = 5, QuantityInGrams = 150 },
              new FoodIngredient { Id = 21, FoodId = 110, IngredientId = 12, QuantityInGrams = 100 },
              new FoodIngredient { Id = 22, FoodId = 111, IngredientId = 15, QuantityInGrams = 150 },
              new FoodIngredient { Id = 23, FoodId = 111, IngredientId = 16, QuantityInGrams = 100 },
              new FoodIngredient { Id = 24, FoodId = 111, IngredientId = 5, QuantityInGrams = 150 },
              new FoodIngredient { Id = 25, FoodId = 111, IngredientId = 6, QuantityInGrams = 10 },
              new FoodIngredient { Id = 26, FoodId = 112, IngredientId = 18, QuantityInGrams = 150 },
              new FoodIngredient { Id = 27, FoodId = 112, IngredientId = 8, QuantityInGrams = 25 },
              new FoodIngredient { Id = 28, FoodId = 113, IngredientId = 17, QuantityInGrams = 40 },
              new FoodIngredient { Id = 34, FoodId = 114, IngredientId = 16, QuantityInGrams = 50 },
              new FoodIngredient { Id = 35, FoodId = 114, IngredientId = 21, QuantityInGrams = 100 },
              new FoodIngredient { Id = 36, FoodId = 115, IngredientId = 7, QuantityInGrams = 100 },
              new FoodIngredient { Id = 37, FoodId = 115, IngredientId = 12, QuantityInGrams = 30 },
              new FoodIngredient { Id = 38, FoodId = 116, IngredientId = 5, QuantityInGrams = 100 },
              new FoodIngredient { Id = 39, FoodId = 116, IngredientId = 14, QuantityInGrams = 100 },
              new FoodIngredient { Id = 40, FoodId = 116, IngredientId = 6, QuantityInGrams = 10 },
              new FoodIngredient { Id = 41, FoodId = 117, IngredientId = 23, QuantityInGrams = 100 },
              new FoodIngredient { Id = 42, FoodId = 117, IngredientId = 24, QuantityInGrams = 50 },
              new FoodIngredient { Id = 43, FoodId = 117, IngredientId = 1, QuantityInGrams = 60 },
              new FoodIngredient { Id = 44, FoodId = 117, IngredientId = 22, QuantityInGrams = 100 },
              new FoodIngredient { Id = 45, FoodId = 118, IngredientId = 1, QuantityInGrams = 100 },
              new FoodIngredient { Id = 46, FoodId = 118, IngredientId = 12, QuantityInGrams = 100 },
              new FoodIngredient { Id = 47, FoodId = 118, IngredientId = 24, QuantityInGrams = 50 },
              new FoodIngredient { Id = 48, FoodId = 118, IngredientId = 14, QuantityInGrams = 50 }
                  );
        }
    }
}