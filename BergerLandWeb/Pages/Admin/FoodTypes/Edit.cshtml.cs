using BL.DataAccess.Data;
using BL.DataAccess.Repository.IRepository;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BergerLandWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public FoodType FoodType { get; set; }
        public void OnGet(int id)
        {
            FoodType= _unitOfWork.FoodType.GetFirstOrDefault(x=> x.Id==id);
            //Category = _db.Category.FirstOrDefault(x => x.Id == id);
            //Category = _db.Category.SingleOrDefault(x => x.Id == id);
            //Category = _db.Category.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task<IActionResult> OnPost()
        {
            if (FoodType.Name== FoodType.Id.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the Name!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.FoodType.Update(FoodType);
                _unitOfWork.Save();
                TempData[("Success")] = "Category updated successfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
