using Microsoft.EntityFrameworkCore;

namespace Figure.DataAccess;

public interface IDbSeeder {
    void Seed(ModelBuilder modelBuilder);
}
