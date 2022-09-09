using Microsoft.EntityFrameworkCore;
using PackageManager.Data;

namespace PackageManager.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PackageManagerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PackageManagerContext>>()))
            {
                if (context.Package.Any()) return;

                context.Package.AddRange(
                    new Package
                    {
                        City = "Krakow",
                        CreationDate = DateTime.Parse("2022-07-08"),
                        Name = "Webcon",
                        IsSealed = true,
                        SealDate = DateTime.Parse("2022-08-12")
                    },
                    new Package
                    {
                        City = "Warszawa",
                        CreationDate = DateTime.Parse("2022-04-18"),
                        Name = "Google",
                        IsSealed = true,
                        SealDate = DateTime.Parse("2022-05-19")
                    },
                    new Package
                    {
                        City = "Krakow",
                        CreationDate = DateTime.Parse("2022-07-11"),
                        Name = "SoftwareMansion",
                        IsSealed = true,
                        SealDate = DateTime.Parse("2022-07-12")
                    },
                    new Package
                    {
                        City = "Krakow",
                        CreationDate = DateTime.Parse("2022-07-08"),
                        Name = "Webcon",
                        IsSealed = false,
                        SealDate = null
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
