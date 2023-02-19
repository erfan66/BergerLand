using BL.DataAccess.Data;
using BL.DataAccess.Repository.IRepository;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BergerLandWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public FoodType FoodType { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        
        public void OnGet(int id)
        {
            FoodType= _unitOfWork.FoodType.GetFirstOrDefault(x=> x.Id==id);
            //Category = _db.Category.FirstOrDefault(x => x.Id == id);
            //Category = _db.Category.SingleOrDefault(x => x.Id == id);
            //Category = _db.Category.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task<IActionResult> OnPost()
        {
            var categoryFromDb = _unitOfWork.FoodType.GetFirstOrDefault(x=> x.Id==FoodType.Id);
            if (categoryFromDb!=null)
            {
                _unitOfWork.FoodType.Remove(categoryFromDb);
                _unitOfWork.Save();
                TempData[("Success")] = "Category deleted successfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
