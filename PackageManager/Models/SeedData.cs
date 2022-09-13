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

                // add packages
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

                //// add items
                //context.Item.AddRange(
                //    new Item
                //    {
                //        Name = "Cheese Sandwiches",
                //        Address = "Krakowska 93",
                //        CreationDate = DateTime.Parse("2022-08-01"),
                //        Mass = 2.5f,
                //        PackageID = 1
                //    },
                //    new Item
                //    {
                //        Name = "Bidding Boxes",
                //        Address = "Czarnowiejska 18a",
                //        CreationDate = DateTime.Parse("2022-05-22"),
                //        Mass = 3.4f,
                //        PackageID = 2

                //    },
                //    new Item
                //    {
                //        Name = "Card Travellers",
                //        Address = "Czarnowiejska 18a",
                //        CreationDate = DateTime.Parse("2022-05-23"),
                //        Mass = 1.4f,
                //        PackageID = 2
                //    },
                //    new Item
                //    {
                //        Name = "Chairs",
                //        Address = "Czarnowiejska 19",
                //        CreationDate = DateTime.Parse("2022-09-11"),
                //        Mass = 223.0f,
                //        PackageID = 2
                //    },
                //    new Item
                //    {
                //        Name = "Bricks",
                //        Address = "Słoneczna 122a",
                //        CreationDate = DateTime.Parse("2021-11-03"),
                //        Mass = 897.5f,
                //        PackageID = 3
                //    },
                //    new Item
                //    {
                //        Name = "Smoked Salmon",
                //        Address = "Stawowa 68",
                //        CreationDate = DateTime.Parse("2022-07-31"),
                //        Mass = 22.1f,
                //        PackageID = 3
                //    },
                //    new Item
                //    {
                //        Name = "Whiteboard Markers",
                //        Address = "Zielonki 126c",
                //        CreationDate = DateTime.Parse("2021-09-26"),
                //        Mass = 4.9f,
                //        PackageID = 4
                //    }
                //);

                context.SaveChanges();
            }
        }
    }
}
