using BL.DataAccess.Data;
using BL.DataAccess.Repository.IRepository;
using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }

        public void UpdateStatus(int id, string status)
        {
            var objFromDb = _db.OrderHeader.FirstOrDefault(x => x.Id == id);
            if (objFromDb!=null)
            {
                objFromDb.Status = status;
            }
        }
    }
}
