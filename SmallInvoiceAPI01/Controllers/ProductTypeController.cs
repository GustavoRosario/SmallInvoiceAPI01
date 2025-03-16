using Microsoft.AspNetCore.Mvc;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using log4net;
using Microsoft.IdentityModel.Tokens;
using SmallInvoiceAPI01.Constats;

namespace SmallInvoiceAPI01.Controllers
{
    [ApiController]
    public class ProductTypeController : Controller
    {
        private IProductTypeRepository _productTypeRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ProductController));

        public ProductTypeController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "CreateProductType")]
        public async Task<ProductTypeResponseDto> CreateProductType(string productTypeName, int processModeId)
        {
            bool productTypeExits = false;
            var responseDto = new ProductTypeResponseDto() { IsSuccess = true };

            try
            {
                productTypeExits = await _productTypeRepository.ProductTypeExits(productTypeName);

                if (productTypeName.Trim().IsNullOrEmpty())
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = $"Debe indicar el nombre del tipo de producto.";
                }

                if (processModeId == 0)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = $"El processModeId no puede ser cero.";
                }

                if (productTypeExits)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = $"Ya existe un tipo de producto con el nombre [{productTypeName}]";
                }

                if (responseDto.IsSuccess)
                    responseDto = await _productTypeRepository.CreateProductType(productTypeName, processModeId);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in CreateProductType method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "UpdateProductType")]
        public async Task<ProductTypeResponseDto> UpdateProductType(ProductTypeDto input)
        {
            var responseDto = new ProductTypeResponseDto();

            try
            {
                responseDto = await _productTypeRepository.UpdateProductType(input);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in UpdateProductType method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetProductType")]
        public async Task<List<ProductTypeDto>> GetProductType()
        {

            List<ProductTypeDto>? productTypeDto = new List<ProductTypeDto>();

            try
            {
                productTypeDto = await _productTypeRepository.GetProductTypes();
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProductType method : " + ex.Message);
            }

            return await Task.Run(() => productTypeDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetProductTypeById")]
        public async Task<ProductTypeDto> GetProductTypeById(Guid id)
        {

            ProductTypeDto? productTypeDto = new ProductTypeDto();

            try
            {
                productTypeDto = await _productTypeRepository.GetProductTypeById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProductTypeById method : " + ex.Message);
            }

            return await Task.Run(() => productTypeDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "DeleteProductTypeById")]
        public async Task<ProductTypeResponseDto> DeleteProductTypeById(Guid Id)
        {
            var responseDto = new ProductTypeResponseDto();

            try
            {
                responseDto = await _productTypeRepository.DeleteProductTypeById(Id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in DeleteProductTypeById method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }
    }
}
