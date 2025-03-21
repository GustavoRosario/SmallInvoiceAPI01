using SmallInvoice.Application.Dto;

namespace SmallInvoice.Domain.Ports.Repository
{
    public interface IProductTypeRepository
    {
        public Task<ProductTypeResponseDto> CreateProductType(string name, int processModeId);
        public Task<ProductTypeResponseDto> UpdateProductType(ProductTypeDto input);
        public Task<List<ProductTypeDto>> GetProductTypes();
        public Task<ProductTypeDto> GetProductTypeById(Guid id);
        public Task<bool> ProductTypeExits(string productTypeName);
        public Task<ProductTypeResponseDto> DeleteProductTypeById(Guid id);
    }
}
