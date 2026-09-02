using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Products;
using ECommerce.Application.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class ProductController(IProductServices productServices) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams, CancellationToken ct)
        {
            var Products = await productServices.GetAllProductsAsync(queryParams, ct);
            var Result = ToActionResult(Products);
            return Result;
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var Brands = await productServices.GetAllProductBrandsAsync(ct);
            var Result = ToActionResult(Brands);
            return Result;
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var Types = await productServices.GetAllProductTypesAsync(ct);
            var Result = ToActionResult(Types);
            return Result;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id,CancellationToken ct)
        {
            var Product = await productServices.GetByIdAsync(id,ct);
            var Result = ToActionResult(Product);
            return Result;
        }
    }
}
