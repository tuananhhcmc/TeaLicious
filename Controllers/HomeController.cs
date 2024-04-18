using bai3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using bai3.ViewModels;
namespace bai3.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebsiteBanHangContext _context;
        public HomeController(WebsiteBanHangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m => m.Order).Take(5).ToListAsync();
            var slides = await _context.Sliders.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var tea_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 4).OrderBy(m => m.Order).Take(3).ToListAsync();
            var tea_cate_prods = await _context.Catologies.Where(m => m.IdCat == 4).FirstOrDefaultAsync();
            var coffee_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 5).OrderBy(m => m.Order).Take(3).ToListAsync();
            var coffee_cate_prods = await _context.Catologies.Where(m => m.IdCat == 5).FirstOrDefaultAsync();
            var viewModel = new HomeViewModel
            {
                Menus = menus,
                Blogs = blogs,
                Sliders = slides,
                TeaProds = tea_prods,
                CoffeeProds = coffee_prods,
                TeaCateProds = tea_cate_prods,
                CoffeeCateProds = coffee_cate_prods,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _SlidePartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _ProductPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _BlogPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }
    }
}