using Figure.DataAccess.Repositories.Interfaces;

namespace Figure.DataAccess.Repositories;
public class FiguresRepository : Repository<DataAccess.Entities.Figure>,IFiguresRepository {
    private readonly ApplicationDbContext _context;

    public FiguresRepository(ApplicationDbContext context) :base(context) {
        _context = context;
    }
}
