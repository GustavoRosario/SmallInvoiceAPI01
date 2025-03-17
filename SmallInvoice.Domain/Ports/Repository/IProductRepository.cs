using SmallInvoice.Application.Dto;

namespace SmallInvoice.Domain.Ports.Repository
{
    public interface IProductRepository
    {
        public Task<ProductResponseDto> CreateProduct(ProductDto input);
        public Task<ProductResponseDto> UpdateProduct(UpdateProductDto input);

        /// <summary>
        /// Retrieve all data of products.
        /// </summary>
        /// <returns></returns>
        public Task<List<ProductDto>> GetProduct();

        /// <summary>
        /// Retrieve the data of product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ProductDto> GetProductById(Guid id);

        /// <summary>
        /// Checks if the product exists.
        /// </summary>
        /// <param name="productName">The name that describe product.</param>
        /// <returns>A <see cref="bool"/> value.</returns>
        public Task<bool> ProductExits(string productName);

        /// <summary>
        /// To disable the product witch correspond to the Id.
        /// </summary>
        /// <param name="Id">Id referer the product</param>
        /// <returns></returns>
        public Task<ProductResponseDto> DeleteProductById(Guid Id);
    }
}
