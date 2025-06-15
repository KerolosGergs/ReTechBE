using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ReTechApi.Models;
using ReTechApi.Data;
using ReTechApi.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using ReTechBE.Models;

namespace ReTechApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //private readonly List<Product> _products;
        //GenericRepository<Product> context = new GenericRepository()
        private readonly ApplicationDbContext _context;

        // Constructor injection (DbContext is auto-provided by DI)
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        [HttpGet("GetCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetCategory()
        {
          
            
            return Ok(await _context.Products.Select(s => s.Category.GetDisplayName()).Distinct().ToListAsync());
        }
        [HttpGet("getProductByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategories(string Category)
        {
            // Try to parse the string to the ProductCategory enum
            if (!Enum.TryParse<ProductCategory>(Category, ignoreCase: true, out var productCategory))
            {
                return BadRequest("Invalid category specified");
            }

            var products = await _context.Products
                .Where(p => p.Category == productCategory)
                .ToListAsync();
            return products;
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(string id)
        {
            var product =_context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductCreateDto model)
        {
          

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                Category = model.Category,
                IsNew = model.IsNew,
                IsAvailable = true,
                Image = $"Images/{fileName}",
                ImageUrl = $"Images/{fileName}",
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
} 