namespace Selection.ProductService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selection.ProductService.Data;
using Selection.ProductService.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductContext context;

    public ProductsController(ProductContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await this.context.Products.ToListAsync();
    }

    // responde con HTTP 201 y los datos del producto creado.
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product) 
    {
        await this.context.SaveChangesAsync();
        return this.CreatedAtAction(nameof(this.GetProducts), new { id = product.Id }, product);
    }
}
