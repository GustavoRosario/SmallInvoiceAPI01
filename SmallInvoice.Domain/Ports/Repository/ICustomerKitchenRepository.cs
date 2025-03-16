using SmallInvoice.Application.Dto;

namespace SmallInvoice.Domain.Ports.Repository
{
    public interface ICustomerKitchenRepository
    {
        public Task<CustomerKitchenResponseDto> CreateCustomerKitchen(CreateCustomerKitchenDto input);
        public Task<CustomerKitchenResponseDto> UpdateCustomerKitchen(UpdateCustomerKitchenDto input);
        public Task<List<CustomerKitchenDto>> GetCustomerKitchen();
        public Task<CustomerKitchenDto> GetCustomerKitchenById(Guid refId);
        public Task<List<CustomerKitchenDto>> GetCustomerKitchenByCustomerId(int customerId);
        public Task<bool> CustomerKitchenExits(string customerKitchenName);
        public Task<CustomerKitchenResponseDto> DeleteCustomerKitchenById(Guid Id);
    }
}
