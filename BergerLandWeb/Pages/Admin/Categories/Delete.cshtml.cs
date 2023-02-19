using BL.DataAccess.Data;
using BL.DataAccess.Repository.IRepository;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BergerLandWeb.Pages.Admin.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            //Category = _db.Category.FirstOrDefault(x => x.Id == id);
            //Category = _db.Category.SingleOrDefault(x => x.Id == id);
            //Category = _db.Category.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task<IActionResult> OnPost()
        {
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == Category.Id);
            if (categoryFromDb!=null)
            {
                _unitOfWork.Category.Remove(categoryFromDb);
                _unitOfWork.Save();
                TempData[("Success")] = "Category deleted successfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
