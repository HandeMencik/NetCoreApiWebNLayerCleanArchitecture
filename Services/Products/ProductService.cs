using Repositories;
using Repositories.Products;

namespace Services.Products;
public class ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsnyc(int count)
    {
        var products = await productRepository.GetTopPriceProductAsnyc(count);

        var productsAsDto = products.Select(p => new ProductDto(p.ProductId,p.Name,p.Price,p.Stock)).ToList();

        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return ServiceResult<ProductDto>.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
        }
        var productAsDto = product is not null ? new ProductDto(product.ProductId, product.Name, product.Price, product.Stock) : null;

        return ServiceResult<ProductDto>.Success(productAsDto!);
    }
    public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product()
        {
            Name =request.Name,
            Price =request.Price,
            Stock = request.Stock
        };
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.ProductId));
    }
    public async Task<ServiceResult> UpdateProductAsync(UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
        }
        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }
    public async Task<ServiceResult> DeleteProductAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
        }
        productRepository.Delete(product.ProductId);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }



}
