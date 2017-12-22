using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;
using Task_4.Models;

namespace Task_4.DAL.Repositories
{
    public class ProductRepository : Repository<ProductDAL>
    {
        private UsersContext context;
        private DbSet<ProductDAL> dbSet;

        public ProductRepository(UsersContext context) /*: base(context)*/
        {
            this.context = context;
            this.dbSet = context.Set<ProductDAL>();
        }

        public int? GetId(ProductDAL item)
        {
            var tmp = context.Products.FirstOrDefault(x => (x.ProductName == item.ProductName));
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp.ProductId;
            }
        }

        public Product ToEntity(ProductDAL source)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDAL, Product>()).CreateMapper();
            //return mapper.Map<ProductDAL, Product>(source);
            return new Product()
            {
                ProductName = source.ProductName,
                ProductPrice = source.ProductPrice
            };
        }

        public ProductDAL ToObject(Product source)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDAL>()).CreateMapper();
            //return mapper.Map<Product, ProductDAL>(source);
            return new ProductDAL()
            {
                ProductName = source.ProductName,
                //ProductPrice = source.ProductPrice
            };
        }

        public void Insert(ProductDAL item)
        {
            context.Products.Add(ToEntity(item));
        }

        public IEnumerable<ProductDAL> GetAll()
        {
            return context.Products
                .Select(x => new ProductDAL() { Id = x.ProductId, ProductName = x.ProductName }).ToArray();
        }

        public ProductDAL GetById(int Id)
        {
            return ToObject(context.Products
                .FirstOrDefault(x => (x.ProductId == Id)));
        }

        public void Update(ProductDAL item)
        {
            context.Products.AddOrUpdate(ToEntity(item));
            //context.Entry(ToEntity(item)).State = EntityState.Modified;
        }
    }
}
