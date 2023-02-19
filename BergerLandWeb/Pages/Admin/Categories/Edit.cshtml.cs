using BL.DataAccess.Data;
using BL.DataAccess.Repository.IRepository;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BergerLandWeb.Pages.Admin.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category=_unitOfWork.Category.GetFirstOrDefault(x=> x.Id==id);
            //Category = _db.Category.FirstOrDefault(x => x.Id == id);
            //Category = _db.Category.SingleOrDefault(x => x.Id == id);
            //Category = _db.Category.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name==Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the Name!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(Category);
                _unitOfWork.Save();
                TempData[("Success")] = "Category updated successfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
