using Microsoft.AspNetCore.Mvc;

namespace bai3.Controllers
{
	public class HomeAdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
