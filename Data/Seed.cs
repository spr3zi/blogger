using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;


namespace Models
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Posts.Any()) return;

            var posts = new List<Post>{
                new Post{
                    Id = 1,
                    Title = "Test 1",
                    Text = "Lorem ipsum Lorem ipsum",
                    Author = "Johnny Cash"
                },
                new Post{
                    Id = 2,
                    Title = "Test 2",
                    Text = "asdfkjlha;slkdasdk",
                    Author = "Johnny Cash"
                }
            };

            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync();

        }
    }
}