using BL.DataAccess.Repository.IRepository;
using BL.Models;
using BL.Models.ViewModel;
using BL.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BergerLandWeb.Pages.Admin.Order
{
    [Authorize(Roles =$"{SD.ManagerRole},{SD.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<OrderDetailVM> OrderDetailVM { get; set; }
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            OrderDetailVM = new();
            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(x => x.Status == SD.StatusSubmitted ||
            x.Status == SD.StatusInProcess).ToList();
            foreach (OrderHeader item in orderHeaders)
            {
                OrderDetailVM individual = new OrderDetailVM()
                {
                    OrderHeader=item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(x=> x.OrderId==item.Id).ToList()
                };
                OrderDetailVM.Add(individual);
            }
        }
        public IActionResult OnPostOrderInProcess(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusInProcess);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
        public IActionResult OnPostOrderReady(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusReady);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
        public IActionResult OnPostOrderCanceled(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
