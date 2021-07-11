using PracticeUnitTest.Models;
using System;

namespace PracticeUnitTest.Tests
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(BlogDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Category.AddRange(
                new Category() { Name = "Category 1", Slug = "category-1" },
                new Category() { Name = "Category 2", Slug = "category-2" },
                new Category() { Name = "Category 3", Slug = "category-3" },
                new Category() { Name = "Category 4", Slug = "category-4" }
            );

            context.Post.AddRange(
                new Post() { Title = "Test Title 1", Description = "Test Description 1", CategoryId = 2, CreatedDate = DateTime.Now },
                new Post() { Title = "Test Title 2", Description = "Test Description 2", CategoryId = 3, CreatedDate = DateTime.Now }
            );
            context.SaveChanges();
        }
    }
}