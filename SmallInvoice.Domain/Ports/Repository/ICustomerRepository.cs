using SmallInvoice.Application.Dto;

namespace SmallInvoice.Domain.Ports.Repository
{
    public interface ICustomerRepository
    {
        public Task<CustomerResponseDto> CreateCustomer(CreateCustomerDto input);
        public Task<CustomerResponseDto> UpdateCustomer(UpdateCustomerDto input);
        public Task<List<CustomerDto>> GetCustomer();
        public Task<CustomerDto> GetCustomerById(Guid id);
        public Task<bool> CustomerExits(string CustomerName);
        public Task<CustomerResponseDto> DeleteCustomerById(Guid Id);
        /// <summary>
        /// Get Customer Name by id like string.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>A string value that represent the Customer Name.</returns>
        public Task<string> GetCustomerNameById(int id);
    }
}
