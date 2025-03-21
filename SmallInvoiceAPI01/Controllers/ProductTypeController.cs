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
        public async Task<ActionResult<List<ProductTypeDto>>> GetProductType()
        {
            try
            {
                var productTypeDto = await _productTypeRepository.GetProductTypes();

                return Ok(productTypeDto);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProductType method : " + ex.Message);

                return StatusCode(500, $"Error : {ex}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductTypeDto>> GetProductTypeById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("El ID del tipo de producto no es valido.");

                var productTypeDto = await _productTypeRepository.GetProductTypeById(id);

                if (productTypeDto == null)
                    return NotFound($"No se encontro el tipo de producto con ID {id}.");

                return Ok(productTypeDto);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProductTypeById method : " + ex.Message);

                return StatusCode(500, $"Error : {ex}");
            }
        }

        [HttpPost("DeleteById")]
        public async Task<ActionResult<ProductTypeResponseDto>> DeleteProductTypeById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("El ID del tipo de producto no es valido.");

                var responseDto = new ProductTypeResponseDto();

                responseDto = await _productTypeRepository.DeleteProductTypeById(id);

                if (responseDto.IsSuccess)
                    return Ok(responseDto);
                else
                    return NotFound(responseDto);

            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in DeleteProductTypeById method : " + ex.Message);

                return StatusCode(500, $"Error : {ex}");
            }
        }
    }
}
