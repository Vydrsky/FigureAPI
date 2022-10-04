using Figure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Figure.DataAccess;

public class DbSeeder : IDbSeeder{
    public void Seed(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Order>().HasData(
            new Order {
                Id = Guid.NewGuid(),
                Name = "test1",
                Email = "adsfdfg",
                PhoneNumber = "9345435",
                Description="description1",
                CreatedAt = DateTime.Now,
                IsArchived = false,
                ArchivedAt = null
            },
            new Order {
                Id = Guid.NewGuid(),
                Name = "test1",
                Email = "adsfdfg",
                PhoneNumber = "9345435",
                Description = "description1",
                CreatedAt = DateTime.Now,
                IsArchived = false,
                ArchivedAt = null
            },
            new Order {
                Id = Guid.NewGuid(),
                Name = "test1",
                Email = "adsfdfg",
                PhoneNumber = "9345435",
                Description = "description1",
                CreatedAt = DateTime.Now,
                IsArchived = false,
                ArchivedAt = null
            },
            new Order {
                Id = Guid.NewGuid(),
                Name = "test1",
                Email = "adsfdfg",
                PhoneNumber = "9345435",
                Description = "description1",
                CreatedAt = DateTime.Now,
                IsArchived = false,
                ArchivedAt = null
            }
            );
    }
}

