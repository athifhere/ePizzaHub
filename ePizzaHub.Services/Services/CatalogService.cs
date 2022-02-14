using ePizzaHub.DAL.Interfaces;
using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;

namespace ePizzaHub.Services.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IRepository<Item> _itemRepo;
        private readonly IRepository<ItemType> _itemTypeRepo;
        private readonly IRepository<Category> _categoryRepo;

        public CatalogService(IRepository<Item> itemRepo, IRepository<ItemType> itemTypeRepo, 
            IRepository<Category> categoryRepo)
        {
            _itemRepo = itemRepo;
            _itemTypeRepo = itemTypeRepo;
            _categoryRepo = categoryRepo;
        }

        public void AddItem(Item item)
        {
            _itemRepo.Add(item);
            _itemRepo.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            _itemRepo.Delete(id);
            _itemRepo.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepo.GetAll();
        }

        public Item GetItem(int id)
        {
            return _itemRepo.Find(id);
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemRepo.GetAll();
        }

        public IEnumerable<ItemType> GetItemTypes()
        {
            return _itemTypeRepo.GetAll();
        }

        public void UpdateItem(Item item)
        {
            _itemRepo.Update(item);
            _itemRepo.SaveChanges();
        }
    }
}
