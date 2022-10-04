using Figure.Core.Entities;

namespace Figure.DataAccess.Interfaces;
internal interface IOrdersRepository<T> where T : IEntity {
    Task Archive(Task entity);
    Task UnArchive(Task entity);
}
