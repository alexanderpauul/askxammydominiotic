using Dominiotic.Entities.Models;

namespace Dominiotic.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private DominioticContext _dbContext;
        private BaseRepository<Clients> _client;
        private BaseRepository<Establishment> _establishment;
        private BaseRepository<Ingredients> _ingredients;
        private BaseRepository<Orders> _orders;
        private BaseRepository<OrdersItems> _ordersItems;
        private BaseRepository<Plates> _plates;
        private BaseRepository<PlatesItems> _platesItems;
        private BaseRepository<Receivables> _receivables;

        public UnitOfWork(DominioticContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Clients> Clients
        {
            get
            {
                return _client ?? (_client = new BaseRepository<Clients>(_dbContext));
            }
        }

        public IRepository<Establishment> Establishments
        {
            get
            {
                return _establishment ?? (_establishment = new BaseRepository<Establishment>(_dbContext));
            }
        }

        public IRepository<Ingredients> Ingredients
        {
            get
            {
                return _ingredients ?? (_ingredients = new BaseRepository<Ingredients>(_dbContext));
            }
        }

        public IRepository<Orders> Orders
        {
            get
            {
                return _orders ?? (_orders = new BaseRepository<Orders>(_dbContext));
            }
        }

        public IRepository<OrdersItems> OrdersItems
        {
            get
            {
                return _ordersItems ?? (_ordersItems = new BaseRepository<OrdersItems>(_dbContext));
            }
        }

        public IRepository<Plates> Plates
        {
            get
            {
                return _plates ?? (_plates = new BaseRepository<Plates>(_dbContext));
            }
        }

        public IRepository<PlatesItems> PlatesItems
        {
            get
            {
                return _platesItems ?? (_platesItems = new BaseRepository<PlatesItems>(_dbContext));
            }
        }

        public IRepository<Receivables> Receivables
        {
            get
            {
                return _receivables ?? (_receivables = new BaseRepository<Receivables>(_dbContext));
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
