using SmallInvoice.Application.Dto;

namespace SmallInvoice.Domain.Ports.Repository
{
    public interface IPriceListRepository
    {
        public Task<PriceListResponseDto> CreatePriceList(List<CreatePriceListDto> input);
        public Task<PriceListResponseDto> CreatePriceList(CreatePriceListDto input);
        //public Task<PriceListResponseDto> UpdatePriceList(UpdatePriceListDto input);
        public Task<List<PriceListDto>> GetPriceList();
        public Task<List<PriceListDto>> GetPriceListByCustomerId(int customerId);
        public Task<PriceListResponseDto> DeleteProductOfPriceListById(int productId, int customerId);
    }
}
