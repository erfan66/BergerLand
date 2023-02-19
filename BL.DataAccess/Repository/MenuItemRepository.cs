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
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(MenuItem obj)
        {
            var objFromDb = _db.MenuItem.FirstOrDefault(x => x.Id == obj.Id);
            objFromDb.Name = obj.Name;
            objFromDb.Discription = obj.Discription;
            objFromDb.Price = obj.Price;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.FoodTypeId = obj.FoodTypeId;
            if (obj.Image!=null)
            {
                objFromDb.Image = obj.Image;
            }
        }
    }
}
