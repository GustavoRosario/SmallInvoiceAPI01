using Microsoft.AspNetCore.Mvc;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using log4net;
using Microsoft.IdentityModel.Tokens;
using SmallInvoiceAPI01.Constats;

namespace SmallInvoiceAPI01.Controllers
{
    [ApiController]
    [Route(Prefix.API_ROUT + "ProductType")]
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ProductController));

        public ProductTypeController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ProductTypeResponseDto>> CreateProductType(string productTypeName, int processModeId)
        {
            try
            {
                if (productTypeName.Trim().IsNullOrEmpty())
                    return BadRequest(new ProductTypeResponseDto { IsSuccess = false, Message = "Debe indicar el nombre del tipo de producto." });

                if (processModeId == 0)
                    return BadRequest(new ProductTypeResponseDto { IsSuccess = false, Message = "El processModeId no puede ser cero." });

                var responseDto = await _productTypeRepository.CreateProductType(productTypeName, processModeId);

                if (responseDto.IsSuccess)
                    return Ok(responseDto);
                else
                    return BadRequest(responseDto);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error has been in CreateProductType method : {ex}");

                return StatusCode(500, new ProductTypeResponseDto
                {
                    IsSuccess = false,
                    Message = $"Error : {ex}"
                });
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<ProductTypeResponseDto>> UpdateProductType(ProductTypeDto input)
        {
            try
            {
                if (input == null)
                    return BadRequest(new ProductTypeResponseDto { IsSuccess = false, Message = "Los datos de entrada no deben ser nulos." });

                if (input.RefId == Guid.Empty)
                    return BadRequest(new ProductTypeResponseDto { IsSuccess = false, Message = "El ID del tipo de producto no es valido." });

                var responseDto = await _productTypeRepository.UpdateProductType(input);

                if (responseDto.IsSuccess)
                    return Ok(responseDto);
                else
                    return BadRequest(responseDto);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in UpdateProductType method : " + ex.Message);

                return StatusCode(500, new ProductTypeResponseDto
                {
                    IsSuccess = false,
                    Message = $"Error : {ex}"
                });
            }
        }

        [HttpGet("GetAll")]
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

        [HttpGet("GetById/{id}")]
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

        [HttpPost("DeleteById")]
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
