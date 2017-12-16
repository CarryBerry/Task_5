using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ShopAssistantRepository(UsersContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<ShopAssistantDAL>();
        }

        public ShopAssistant ToEntity(ShopAssistantDAL source)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShopAssistantDAL, ShopAssistant>()).CreateMapper();
            return mapper.Map<ShopAssistantDAL, ShopAssistant>(source);
        }

        public ShopAssistantDAL ToObject(ShopAssistant source)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShopAssistant, ShopAssistantDAL>()).CreateMapper();
            return mapper.Map<ShopAssistant, ShopAssistantDAL>(source);
        }

        public override int? GetId(ShopAssistantDAL item)
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

        public new void Insert(ShopAssistantDAL item)
        {
            context.ShopAssistants.Add(ToEntity(item));
        }

        public new IEnumerable<ShopAssistantDAL> GetAll()
        {
            return context.ShopAssistants.Select(c => new ShopAssistantDAL() { Id = c.ShopAssistantId, ShopAssistantName = c.ShopAssistantName }).ToArray();
        }

        public new ShopAssistantDAL GetById(int Id)
        {
            return ToObject(context.ShopAssistants.FirstOrDefault(c => (c.ShopAssistantId == Id)));
        }

        public void Update(ShopAssistant item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
