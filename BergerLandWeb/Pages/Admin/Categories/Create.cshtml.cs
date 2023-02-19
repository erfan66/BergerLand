using BL.DataAccess.Data;
using BL.DataAccess.Repository.IRepository;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BergerLandWeb.Pages.Admin.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public Category Category { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name==Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the Name!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(Category);
                _unitOfWork.Save();
                TempData[("Success")]= "Category created successfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
