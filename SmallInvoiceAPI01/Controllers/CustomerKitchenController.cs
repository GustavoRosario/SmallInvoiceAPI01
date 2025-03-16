using Microsoft.AspNetCore.Mvc;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using SmallInvoiceAPI01.Constats;
using log4net;

namespace SmallInvoiceAPI01.Controllers
{
    [ApiController]
    public class CustomerKitchenController : Controller
    {
        private ICustomerKitchenRepository _customerKitchenRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CustomerKitchenController));

        public CustomerKitchenController(ICustomerKitchenRepository customerKitchenRepository)
        {
            _customerKitchenRepository = customerKitchenRepository;
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "CreateCustomerKitchen")]
        public async Task<CustomerKitchenResponseDto> CreateCustomerKitchen(CreateCustomerKitchenDto input)
        {
            bool customerExits = false;
            var responseDto = new CustomerKitchenResponseDto() { IsSuccess = true };

            try
            {
                //customerExits = this.customerKitchenRepository.CustomerKitchenExits(input.CustomerKitchenName).Result;

                if (string.IsNullOrEmpty(input.CustomerKitchenName.Trim()))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Debe indicar el nombre de la cocina.";
                }
                /*
                if (customerExits)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = $"Ya existe un cliente con el nombre [{input.CustomerKitchenName}]";
                }
                */
                if (input.ProcessModeId == 0)
                {
                    responseDto.Message = $"El Id del modo de proceso no puede ser cero.";
                    responseDto.IsSuccess = false;
                }

                if (responseDto.IsSuccess)
                    responseDto = await _customerKitchenRepository.CreateCustomerKitchen(input);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in CreateCustomerKitchen method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "UpdateCustomerKitchen")]
        public async Task<CustomerKitchenResponseDto> UpdateCustomerKitchen(UpdateCustomerKitchenDto input)
        {
            var responseDto = new CustomerKitchenResponseDto() { IsSuccess = true };

            try
            {
                if (string.IsNullOrEmpty(input.CustomerKitchenName.Trim()))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Debe indicar el nombre de la cocina.";
                }

                if (responseDto.IsSuccess)
                    responseDto = await _customerKitchenRepository.UpdateCustomerKitchen(input);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in UpdateCustomerKitchen method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetCustomerKitchen")]
        public async Task<List<CustomerKitchenDto>> GetCustomerKitchen()
        {
            List<CustomerKitchenDto>? customerDto = new List<CustomerKitchenDto>();

            try
            {
                customerDto = await _customerKitchenRepository.GetCustomerKitchen();
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetCustomerKitchen method : " + ex.Message);
            }

            return await Task.Run(() => customerDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetCustomerKitchenByCustomerId")]
        public async Task<List<CustomerKitchenDto>> GetCustomerKitchenByCustomerId(int customerId)
        {
            List<CustomerKitchenDto>? customerDto = new List<CustomerKitchenDto>();

            try
            {
                customerDto = await _customerKitchenRepository.GetCustomerKitchenByCustomerId(customerId);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetCustomerKitchenByCustomerId method : " + ex.Message);
            }

            return await Task.Run(() => customerDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetCustomerKitchenById")]
        public async Task<CustomerKitchenDto> GetCustomerKitchenById(Guid id)
        {
            CustomerKitchenDto customerDto = new CustomerKitchenDto();

            try
            {
                customerDto = await _customerKitchenRepository.GetCustomerKitchenById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetCustomerKitchenById method : " + ex.Message);
            }

            return await Task.Run(() => customerDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "DeleteCustomerKitchenById")]
        public async Task<CustomerKitchenResponseDto> DeleteCustomerKitchenById(Guid id)
        {
            var responseDto = new CustomerKitchenResponseDto();

            try
            {
                responseDto = await _customerKitchenRepository.DeleteCustomerKitchenById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in DeleteCustomerKitchenById method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

    }
}
