using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Contract.ProductAgg.Repository;
using App.Domain.Core.Contract.ProductAgg.Service;
using App.Domain.Core.Dtos.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.ProductAgg
{
    public class ProductService(IProductRepository productRepository , ICategoryRepository categoryRepository) : IProductService
    {
        public async Task<Result<int>> Edit(ProductDto productDto, CancellationToken cancellationToken)
        {
            ProductDto? productDtoDb= await productRepository.GetById(productDto.Id , cancellationToken);
            if (productDtoDb == null)
            {
                Result<int>.Failure("همچین محصولی موجود نیست.");
            }
            var categoryDto =await categoryRepository.GetById(productDto.CategoryId , cancellationToken);
            if (categoryDto.Data == null) 
            {
                Result<int>.Failure("همچین دسته بندی ای موجود نیست.");
            }

           return Result<int>.Success(await productRepository.Edit(productDto, cancellationToken));

        }

        public async Task<ProductListDto> GetAll(int pageNumber, int pageSize, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            return await productRepository.GetAll(pageNumber, pageSize, categoryId, cancellationToken);
        }

        public async Task<ProductDto?> GetById(int productId , CancellationToken cancellationToken)
        {
            ProductDto? productDto =await productRepository.GetById(productId , cancellationToken);
            if (productDto==null)
            {
                throw new Exception("همچین محصولی موجود نیست.");
            }
            return productDto;
        }

        public async Task<int> GetCountProduct(CancellationToken cancellationToken)
        {
            return await productRepository.GetCount(cancellationToken);
        }
    }
}
