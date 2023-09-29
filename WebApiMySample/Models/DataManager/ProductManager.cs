using System.Collections.Generic;
using System.Linq;
using WebApiMySample.Models;
using WebApiMySample.Models.Repository;

namespace EFCoreCodeFirstSample.Models.DataManager
{
    public class ProductManager : IDataRepository<Product>
    {
        readonly ProductContext _ProductContext;

        public ProductManager(ProductContext context)
        {
            _ProductContext = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _ProductContext.Products.ToList();
        }

        public Product Get(long id)
        {
            return _ProductContext.Products
                  .FirstOrDefault(e => e.ProductId == id);
        }

        public void Add(Product entity)
        {
            _ProductContext.Products.Add(entity);
            _ProductContext.SaveChanges();
        }

        public void Update(Product Product, Product entity)
        {
            Product.ProductName = entity.ProductName;
            Product.Description = entity.Description;
            Product.ListPrice = entity.ListPrice;
            Product.BrandId = entity.BrandId;
            Product.CategoryId = entity.CategoryId;
            Product.ModelYear = entity.ModelYear;

            _ProductContext.SaveChanges();
        }

        public void Delete(Product Product)
        {
            _ProductContext.Products.Remove(Product);
            _ProductContext.SaveChanges();
        }
    }
}