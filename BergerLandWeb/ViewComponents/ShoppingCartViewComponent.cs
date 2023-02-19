using BL.DataAccess.Repository.IRepository;
using BL.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BergerLandWeb.ViewComponents
{
    public class ShoppingCartViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;
            if (claim!=null)
            {
                //log in
                if (HttpContext.Session.GetInt32(SD.SessionCart)!=null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                else
                {
                    count = _unitOfWork.ShoppingCart.GetAll(x =>
                    x.ApplicationUserId == claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(SD.SessionCart, count);
                    return View(count);
                }
               

            }
            else
            {
                //not login
                HttpContext.Session.Clear();
                return View(count);
            }
        }
    }
}
