using Microsoft.AspNetCore.Mvc;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using log4net;
using SmallInvoiceAPI01.Constats;

namespace SmallInvoiceAPI01.Controllers
{
    [ApiController]
    public class PriceListRepositoryController : Controller
    {
        private IPriceListRepository _priceListRepository;
        private ICustomerRepository customerRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CustomerController));

        public PriceListRepositoryController(IPriceListRepository priceListRepository, ICustomerRepository customerRepository)
        {
            _priceListRepository = priceListRepository;
            this.customerRepository = customerRepository;
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "CreatePriceList")]
        public async Task<PriceListResponseDto> CreatePriceList(List<CreatePriceListDto> input)
        {
            var responseDto = new PriceListResponseDto();
            var customerName = customerRepository.GetCustomerNameById(input[0].CustomerId);

            try
            {
                responseDto = await _priceListRepository.CreatePriceList(input);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in CreatePriceList method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetPriceList")]
        public async Task<List<PriceListDto>> GetPriceList()
        {
            List<PriceListDto> responseDto = new List<PriceListDto>();

            try
            {
                responseDto = await _priceListRepository.GetPriceList();
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in CreatePriceList method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetPriceListByCustomerId")]
        public async Task<List<PriceListDto>> GetPriceListByCustomerId(int customerId)
        {
            List<PriceListDto> responseDto = new List<PriceListDto>();

            try
            {
                responseDto = await _priceListRepository.GetPriceListByCustomerId(customerId);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetPriceListByCustomerId method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "DeleteProductOfPriceListById")]
        public async Task<PriceListResponseDto> DeleteProductOfPriceListById(int productId, int customerId)
        {
            var responseDto = new PriceListResponseDto();

            try
            {
                responseDto = await _priceListRepository.DeleteProductOfPriceListById(productId, customerId);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in DeleteProductOfPriceListById method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }
    }
}
