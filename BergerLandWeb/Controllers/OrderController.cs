using BL.DataAccess.Repository.IRepository;
using BL.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BergerLandWeb.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Get(string? status = null)
        {

            var OrderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            if (status== "cancelled")
            {
                OrderHeaderList = OrderHeaderList.Where(x => x.Status == SD.StatusCancelled || x.Status == SD.StatusRejected);
            }
            else
            {
                if (status == "completed")
                {
                    OrderHeaderList = OrderHeaderList.Where(x => x.Status == SD.StatusCompleted);
                }
                else
                {
                    if (status == "ready")
                    {
                        OrderHeaderList = OrderHeaderList.Where(x => x.Status == SD.StatusReady);
                    }
                    else
                    {
                        OrderHeaderList = OrderHeaderList.Where(x => x.Status == SD.StatusInProcess || x.Status == SD.StatusSubmitted);
                    }
                }
            }
            return Json(new { data = OrderHeaderList });
        }
    }
}
