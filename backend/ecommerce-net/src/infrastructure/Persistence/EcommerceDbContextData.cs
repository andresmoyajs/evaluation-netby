using domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace infrastructure.Persistence
{
    public class EcommerceDbContextData
    {
        public static async Task LoadDataAsync(
            ProductDbContext context,
            ILoggerFactory loggerFactory)
        {
            try
            {

                if (!context.Categories!.Any())
                {
                    var categoryData = File.ReadAllText("../../infrastructure/Data/category.json");
                    var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                    await context.Categories!.AddRangeAsync(categories!);
                    await context.SaveChangesAsync();
                }

                if (!context.Products!.Any())
                {
                    var productsData = File.ReadAllText("../../infrastructure/Data/product.json");
                    var products = JsonConvert.DeserializeObject<List<Product>>(productsData);
                    await context.Products!.AddRangeAsync(products!);
                    await context.SaveChangesAsync();
                }

                if (!context.Images!.Any())
                {
                    var imagesData = File.ReadAllText("../../infrastructure/Data/image.json");
                    var images = JsonConvert.DeserializeObject<List<Image>>(imagesData);
                    await context.Images!.AddRangeAsync(images!);
                    await context.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
                logger.LogError(ex.Message);
            }
        }
    }
}
