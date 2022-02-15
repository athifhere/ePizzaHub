using ePizzaHub.DAL;
using ePizzaHub.DAL.Implementations;
using ePizzaHub.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ePizzaHub.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        DatabaseContext dbContext
        {
            get
            {
                return _db as DatabaseContext;
            }
        }
        public CartRepository(DbContext db) : base(db)
        {

        }
        public int DeleteItem(Guid cartId, int itemId)
        {
            var item = dbContext.CartItems.Where(ci => ci.CartId == cartId && ci.Id == itemId).FirstOrDefault();
            if (item != null)
            {
                dbContext.CartItems.Remove(item);
                return dbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public Cart GetCart(Guid CartId)
        {
            return dbContext.Carts.Include("Items").Where(c=>c.Id ==CartId && c.IsActive).FirstOrDefault();
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from cart in dbContext.Carts
                         where cart.Id == CartId && cart.IsActive == true
                         select new CartModel
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreatedDate = cart.CreatedDate,
                             Items = (from cartItem in dbContext.CartItems
                                      join item in dbContext.Items
                                      on cartItem.ItemId equals item.Id
                                      where cartItem.CartId == CartId
                                      select new ItemModel
                                      {
                                          Id = cartItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = cartItem.Quantity,
                                          ItemId = item.Id,
                                          UnitPrice = cartItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            Cart cart = GetCart(cartId);
            cart.UserId = userId;
            return dbContext.SaveChanges();
        }

        public int UpdateQuantity(Guid cartId, int itemId, int Quantity)
        {
            bool flag = false;
            var cart = GetCart(cartId);
            if (cart != null)
            {
                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (cart.Items[i].Id == itemId)
                    {
                        flag = true;
                        cart.Items[i].Quantity += (Quantity);
                        break;
                    }
                }
                if (flag)
                    return dbContext.SaveChanges();
            }
            return 0;
        }
    }
}
