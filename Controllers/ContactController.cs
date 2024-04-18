using bai3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bai3.ViewModels;
using Newtonsoft.Json;

public class ContactController : Controller
{
    private readonly WebsiteBanHangContext _context;
	private const string CartSession = "CartSession";
	public ContactController(WebsiteBanHangContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
        var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m => m.Order).Take(5).ToListAsync();
        var viewModel = new ContactViewModel
        {
            Menus = menus,
            Blogs = blogs,
        };
        return View(viewModel);
    }
    public IActionResult AddItem(int ProductId, int Quantity)
    {
        var product = _context.Products.Find(ProductId);
        var cart = HttpContext.Session.GetString(CartSession);
        if (!string.IsNullOrEmpty(cart))
        {
            var list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            if (list.Exists(x => x.Product.IdPro == ProductId))
            {
                foreach (var item in list)
                {
                    if (item.Product.IdPro == ProductId)
                    {
                        item.Quantity += Quantity;
                    }
                }
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = Quantity;
                list.Add(item);
            }
            HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(list));
        }
        else
        {
            var item = new CartItem();
            item.Product = product;
            item.Quantity = Quantity;
            var list = new List<CartItem>();
            list.Add(item);
            HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(list));
        }
        return RedirectToAction("Index");
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