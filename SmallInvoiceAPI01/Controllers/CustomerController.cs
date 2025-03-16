using Microsoft.AspNetCore.Mvc;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using log4net;
using SmallInvoiceAPI01.Constats;

namespace SmallInvoiceAPI01.Controllers
{
    [ApiController]
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CustomerController));

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "CreateCustomer")]
        public async Task<CustomerResponseDto> CreateCustomer(CreateCustomerDto input)
        {
            bool customerExits = false;
            var responseDto = new CustomerResponseDto() { IsSuccess = true };

            try
            {
                customerExits = await _customerRepository.CustomerExits(input.CustomerName);

                if (string.IsNullOrEmpty(input.CustomerName.Trim()))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Debe indicar el nombre del cliente.";
                }

                if (customerExits)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = $"Ya existe un cliente con el nombre [{input.CustomerName}]";
                }

                if (input.ProcessModeId == 0)
                {
                    responseDto.Message = $"El Id del modo de proceso no puede ser cero.";
                    responseDto.IsSuccess = false;
                }

                if (responseDto.IsSuccess)
                    responseDto = _customerRepository.CreateCustomer(input).Result;
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in CreateCustomer method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "UpdateCustomer")]
        public async Task<CustomerResponseDto> UpdateCustomer(UpdateCustomerDto input)
        {
            var responseDto = new CustomerResponseDto() { IsSuccess = true };

            try
            {
                if (string.IsNullOrEmpty(input.CustomerName.Trim()))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Debe indicar el nombre del cliente.";
                }

                if (responseDto.IsSuccess)
                    responseDto = await _customerRepository.UpdateCustomer(input);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in UpdateCustomer method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetCustomer")]
        public async Task<List<CustomerDto>> GetCustomer()
        {
            List<CustomerDto>? customerDto = new List<CustomerDto>();

            try
            {
                customerDto = await _customerRepository.GetCustomer();
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetCustomer method : " + ex.Message);
            }

            return await Task.Run(() => customerDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetCustomerById")]
        public async Task<CustomerDto> GetCustomerById(Guid id)
        {
            CustomerDto customerDto = new CustomerDto();

            try
            {
                customerDto = await _customerRepository.GetCustomerById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetCustomerById method : " + ex.Message);
            }

            return await Task.Run(() => customerDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "DeleteCustomerById")]
        public async Task<CustomerResponseDto> DeleteCustomerById(Guid Id)
        {
            var responseDto = new CustomerResponseDto();

            try
            {
                responseDto = await _customerRepository.DeleteCustomerById(Id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in DeleteCustomerById method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }
    }
}
