using Dominiotic.Entities.Models;

namespace Dominiotic.Services
{
    public interface IUnitOfWork
    {
        IRepository<Clients> Clients { get; }
        IRepository<Establishment> Establishments { get; }
        IRepository<Ingredients> Ingredients { get; }
        IRepository<Orders> Orders { get; }
        IRepository<OrdersItems> OrdersItems{ get; }
        IRepository<Plates> Plates { get; }
        IRepository<PlatesItems> PlatesItems { get; }
        IRepository<Receivables> Receivables { get; }
        void Save();
    }
}
