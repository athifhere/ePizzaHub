using ePizzaHub.Entities;

namespace ePizzaHub.Services.Interfaces
{
    public interface ICatalogService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Item> GetItems();
        IEnumerable<ItemType> GetItemTypes();
        Item GetItem(int id);
        void AddItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int id);
    }
}
