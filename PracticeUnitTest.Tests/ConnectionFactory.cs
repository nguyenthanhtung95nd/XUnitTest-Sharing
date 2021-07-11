using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PracticeUnitTest.Models;
using System;

namespace PracticeUnitTest.Tests
{
    public class ConnectionFactory : IDisposable
    {
        private bool _disposedValue = false;

        public BlogDBContext CreateContextForSqLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<BlogDBContext>().UseSqlite(connection).Options;

            var context = new BlogDBContext(option);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}