using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Products;
using ECommerce.Application.Params;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default)
        {
            var Brands = await unitOfWork.GetRepository<ProductsBrand,int>().GetAllAsync(ct);
            var mappedBrands = mapper.Map<IReadOnlyList<ProductsBrand>, IReadOnlyList<BrandDto>>(Brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(mappedBrands);
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(queryParams);
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationsAsync(spec,ct);

            var mappedProducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(Products);

            var countSpec = new ProductCountSpecifications(queryParams);
            var totalCount = await unitOfWork.GetRepository<Product, int>().GetProductCountWithSpecificationsAsync(countSpec, ct);

            return Result<PaginatedResult<ProductDto>>.Ok(new PaginatedResult<ProductDto>(mappedProducts,
            queryParams.PageIndex, Products.Count, totalCount));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default)
        {
            var Types = await unitOfWork.GetRepository<ProductsType, int>().GetAllAsync(ct);
            var mappedTypes = mapper.Map<IReadOnlyList<ProductsType>, IReadOnlyList<TypeDto>>(Types);
            return Result<IReadOnlyList<TypeDto>>.Ok(mappedTypes);
        }

        public async Task<Result<ProductDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdWithSpecificationsAsync(spec, ct);

            if (product is null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound", $"Product With Id: {id} is not found"));

            var mappedProduct = mapper.Map<Product, ProductDto>(product);

            return mappedProduct;
        }
    }
}
