using bai3.Models;
using bai3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList;
using X.PagedList;
namespace bai3.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebsiteBanHangContext _context;
        public ProductController(WebsiteBanHangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? page)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
               m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>
               m.Order).Take(5).ToListAsync();
            var prods = await _context.Products.Where(m => m.Hide == 0).OrderBy(m =>
               m.Order).ToListAsync();

            int pageSize = 3;
            int pageNumber = page ?? 1; // Nếu page null thì mặc định là trang 1

            var pagedProds = await prods.ToPagedListAsync(pageNumber, pageSize);

            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Blogs = blogs,
                Prods = pagedProds.ToList(), // Gán danh sách sản phẩm đã phân trang vào viewModel
            };

            return View(viewModel);
        }


        public async Task<IActionResult> CateProd(string slug, long id)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
           m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>
           m.Order).Take(2).ToListAsync();
            var cateProds = await _context.Catologies
            .Where(cp => cp.IdCat == id && cp.Link == slug).FirstOrDefaultAsync();
            if (cateProds == null)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = "CateProd Error",
                };
                return View("Error", errorViewModel);
            }
            var prods = await _context.Products
           .Where(m => m.Hide == 0 && m.IdCat == cateProds.IdCat)
            .OrderBy(m => m.Order).ToListAsync();
            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Blogs = blogs,
                Prods = prods,
                cateName = cateProds.NameCat,
            };
            return View(viewModel);
        }
		public async Task<IActionResult> ProdDetail(string slug, long id)
		{
			var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
		   m.Order).ToListAsync();
			var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>
		   m.Order).Take(5).ToListAsync();
			var prods = await _context.Products.Where(m => m.Link == slug && m.IdPro ==
		   id).ToListAsync();
			if (prods == null)
			{
				var errorViewModel = new ErrorViewModel
				{
					RequestId = "Product Error",
				};
				return View("Error", errorViewModel);
			}
			var viewModel = new ProductViewModel
			{
				Menus = menus,
				Blogs = blogs,
				Prods = prods,
			};
			return View(viewModel);
		}
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                // Tìm sản phẩm cần xóa trong danh sách
                var productToDelete = _context.Products.Find(id);

                // Kiểm tra xem sản phẩm có tồn tại không
                if (productToDelete == null)
                {
                    return NotFound();
                }

                // Xóa sản phẩm khỏi danh sách
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        public IActionResult AddProduct()
        {
            var model = new ProductViewModel();
            model.Menus = _context.Menus.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var product in model.Prods)
                {
                    // Thêm từng sản phẩm từ danh sách vào cơ sở dữ liệu
                    _context.Products.Add(product);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Nếu dữ liệu không hợp lệ, hiển thị lại form với thông báo lỗi
            return View(model);
        }

        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _BlogPartial()
        {
            return PartialView();
        }
    }
}