using bai3.Models;
using bai3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bai3.Controllers
{
    public class BlogController : Controller
    {
        private readonly WebsiteBanHangContext _context;
        public BlogController(WebsiteBanHangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
           m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>
           m.Order).Take(5).ToListAsync();
           
            var viewModel = new BlogViewModel
            {
                Menus = menus,
                Blogs = blogs
              
            };
            return View(viewModel);
        }
        public async Task<IActionResult> BlogDetail(string slug, long id)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
           m.Order).ToListAsync();

            var blogs = await _context.Blogs.Where(m => m.Hide == 0 && m.IdBlog == id).OrderBy(m =>
           m.Order).Take(5).ToListAsync();

            var viewModel = new BlogViewModel
            {
                Menus = menus,
                Blogs = blogs,

            };
            return View(viewModel);
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
