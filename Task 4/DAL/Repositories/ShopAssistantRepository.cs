using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL;
using Task_4.DAL.Models;
using Task_4.Models;

namespace Task_4.DAL.Repositories
{
    public class ShopAssistantRepository : Repository<ShopAssistantDAL>
    {
        private UsersContext context;
        private DbSet<ShopAssistantDAL> dbSet;

        public ShopAssistantRepository(UsersContext context) /*: base(context)*/
        {
            this.context = context;
            this.dbSet = context.Set<ShopAssistantDAL>();
        }

        public ShopAssistant ToEntity(ShopAssistantDAL source)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShopAssistantDAL, ShopAssistant>()).CreateMapper();
            //return mapper.Map<ShopAssistantDAL, ShopAssistant>(source);
            return new ShopAssistant()
            {
                ShopAssistantId = source.Id,
                ShopAssistantName = source.ShopAssistantName
            };
        }

        public ShopAssistantDAL ToObject(ShopAssistant source)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShopAssistant, ShopAssistantDAL>()).CreateMapper();
            //return mapper.Map<ShopAssistant, ShopAssistantDAL>(source);
            return new ShopAssistantDAL()
            {
                Id = source.ShopAssistantId,
                ShopAssistantName = source.ShopAssistantName
            };
        }

        public int? GetId(ShopAssistantDAL item)
        {
            var tmp = context.ShopAssistants.FirstOrDefault(x => (x.ShopAssistantName == item.ShopAssistantName));
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp.ShopAssistantId;
            }
        }
        public int? GetIdByName(string name)
        {
            var tmp = context.ShopAssistants.FirstOrDefault(x => (x.ShopAssistantName == name));
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp.ShopAssistantId;
            }
        }

        public IEnumerable<ShopAssistantDAL> GetAll()
        {
            return context.ShopAssistants.Select(x => new ShopAssistantDAL() { Id = x.ShopAssistantId, ShopAssistantName = x.ShopAssistantName }).ToArray();
        }

        public ShopAssistantDAL GetById(int Id)
        {
            return ToObject(context.ShopAssistants.FirstOrDefault(x => (x.ShopAssistantId == Id)));
        }

        public void Insert(ShopAssistantDAL item)
        {
            context.ShopAssistants.Add(ToEntity(item));
        }
        public void Delete(int id)
        {
            ShopAssistant item = context.ShopAssistants.Find(id);
            if (item != null)
            {
                context.ShopAssistants.Remove(item);
            }
        }

        public void Update(ShopAssistantDAL item) 
        {
            //context.ShopAssistants.AddOrUpdate(ToEntity(item));
            context.Entry(ToEntity(item)).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
