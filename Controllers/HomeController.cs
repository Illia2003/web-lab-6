using LR4.DbConnection;
using LR4.Entities;
using LR4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LR4.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataContext _context;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]

    public async Task<IActionResult> Index()
    {
        var records = await _context.Products
            .AsNoTracking()
            .Include(t => t.Sizes)
            .ToListAsync();

        return View(records);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public IActionResult CreateProduct()
    {
        return View(new Product());
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        if (!ModelState.IsValid)
            return View(product);

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetModifyProduct(Guid id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(t => t.Sizes)
            .FirstOrDefaultAsync(t => t.Id == id);

        return View("ModifyProduct", product);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> ModifyProduct(Product modProduct)
    {
        var product = await _context.Products
            .Include(t => t.Sizes)
            .FirstOrDefaultAsync(t => t.Id == modProduct.Id);

        product.Sizes.Clear();

        foreach(var item in modProduct.Sizes) 
        { 
            item.ProductId = product.Id;
            await _context.AddAsync(item);
        }

        product.Title = modProduct.Title;
        product.Description = modProduct.Description;
        product.ProductType = modProduct.ProductType;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetProduct([FromRoute]Guid id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(t => t.Sizes)
            .FirstOrDefaultAsync(t => t.Id == id);

        return View(product);
    }

    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(t => t.Id == id);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteSize([FromRoute] Guid id, [FromQuery] Guid productid)
    {
        var size = await _context.Size
            .FirstOrDefaultAsync(t => t.Id == id);

        _context.Size.Remove(size);
        await _context.SaveChangesAsync();

        return RedirectToAction("GetModifyProduct", new {id = productid });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}