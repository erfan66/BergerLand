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
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public FoodTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(FoodType foodType)
        {
            var objFromDb = _db.FoodType.FirstOrDefault(x => x.Id == foodType.Id);
            objFromDb.Name = foodType.Name;
        }
    }
}
