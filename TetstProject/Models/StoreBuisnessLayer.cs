using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using TetstProject.DataAccessLayer;
using TetstProject.ViewModel;

namespace TetstProject.Models
{
    public class StoreBuisnessLayer
    {
        public List<Store> GetStore()
        {
            MyDbContext dbContext = new MyDbContext();
            return dbContext.Stores.ToList();
        }

        public List<Product> GetProduct()
        {
            MyDbContext dbContext = new MyDbContext();
            return dbContext.Products.ToList();
        }

        public void CreateProduct(ProductViewModel productVM)
        {

            MyDbContext dbContext = new MyDbContext();
            Product product = new Product();
            product.Code = Convert.ToInt32(productVM.Code);
            product.Name = productVM.Name;
            product.Price = Convert.ToDouble(productVM.Price);
            product.StoreId = dbContext.Stores.Where(s => s.Name == productVM.StoreName).Select(s => s.StoreId).First();

            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public void CreateStore(StoreViewModel storeVM)
        {
            MyDbContext dbContext = new MyDbContext();
            Store store = new Store();
            store.Name = storeVM.Name;
            dbContext.Stores.Add(store);
            dbContext.SaveChanges();
        }

        public ProductViewModel FindProductById(string id)
        {
            MyDbContext dbContext = new MyDbContext();
            Product p = dbContext.Products.Find(Convert.ToInt32(id));
            if (p != null)
            {
                return new ProductViewModel { Id = id.ToString(), Code = p.Code.ToString(), Name = p.Name, Price = p.Price.ToString(), StoreName = p.Store.Name };
            }
            else return null;
        }

        public StoreViewModel FindStoreById(string id)
        {
            MyDbContext dbContext = new MyDbContext();
            Store s = dbContext.Stores.Find(Convert.ToInt32(id));
            if (s != null)
            {
                return new StoreViewModel { Id = id.ToString(), Name = s.Name };
            }
            else return null;
        }

        public void EditStore(StoreViewModel storeVM)
        {
            MyDbContext dbContext = new MyDbContext();
            Store store = new Store()
            {
                StoreId = Convert.ToInt32(storeVM.Id),
                Name = storeVM.Name,
            };
            dbContext.Entry(store).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void EditProduct(ProductViewModel productVM)
        {
            MyDbContext dbContext = new MyDbContext();
            Product product = new Product()
            {
                ProductId = Convert.ToInt32(productVM.Id),
                Code = Convert.ToInt32(productVM.Code),
                Price = Convert.ToDouble(productVM.Price),
                Name = productVM.Name,
                StoreId = dbContext.Stores.Where(s => s.Name == productVM.StoreName).Select(s => s.StoreId).First()
            };

            dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }
        public string DeleteProduct(string id)
        {
            MyDbContext dbContext = new MyDbContext();
            ProductViewModel productVM = FindProductById(id);
            if (productVM != null)
            {
                Product product = new Product()
                {
                    ProductId = Convert.ToInt32(id)
                };
                try
                {
                    dbContext.Entry(product).State = EntityState.Deleted;
                    dbContext.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Не найден объект!";
        }

        

        public string DeleteStore(string id)
        {
            MyDbContext dbContext = new MyDbContext();
            StoreViewModel storeVM = FindStoreById(id);
          
            if (storeVM != null)
            {
                Store store = new Store() { Name = storeVM.Name, StoreId = Convert.ToInt32(storeVM.Id) };
                try
                {
                    dbContext.Entry(store).State = EntityState.Deleted; 
                dbContext.SaveChanges();
                return null;
                }
                catch (Exception)
                {
                    return "Для удаления склад должен быть пустым! Удалите продукты, относящиеся к складу: " + store.Name;
                }
            }
            return "Не найден объект!";
        }
    }
}
