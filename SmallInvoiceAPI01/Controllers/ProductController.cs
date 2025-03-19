using Microsoft.AspNetCore.Mvc;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using log4net;
using SmallInvoiceAPI01.Constats;

namespace SmallInvoiceAPI01.Controllers
{
    [ApiController]
    [Route(Prefix.API_ROUT + "Product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ProductController));

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] ProductDto input)
        {
            try
            {
                if (input == null)
                    return BadRequest(new ProductResponseDto { IsSuccess = false, Message = "Los datos de entrada no deben ser nulos." });

                if (string.IsNullOrEmpty(input.ProductName.Trim()))
                    return BadRequest(new ProductResponseDto { IsSuccess = false, Message = "Debe indicar el nombre del producto." });

                if (input.ProductTypeId == 0)
                    return BadRequest(new ProductResponseDto { IsSuccess = false, Message = "Debe indicar el Id del tipo de producto." });

                if (input.ProcessModeId == 0)
                    return BadRequest(new ProductResponseDto { IsSuccess = false, Message = $"El Id del modo de proceso no puede ser cero, [10,20]." });

                var response = await _productRepository.CreateProduct(input);

                if (response.IsSuccess)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error has been in CreateProduct method : {ex}");

                return StatusCode(500, new ProductResponseDto
                {
                    IsSuccess = false,
                    Message = "Error : " + ex.Message
                });
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<ProductResponseDto>> UpdateProduct([FromBody] UpdateProductDto input)
        {
            try
            {
                if (input == null)
                    return BadRequest(new ProductResponseDto { IsSuccess = false, Message = "Los datos de entrada no deben ser nulos." });

                if (string.IsNullOrEmpty(input.ProductName.Trim()))
                    return BadRequest(new ProductResponseDto { IsSuccess = false, Message = "Debe indicar el nombre del producto." });

                if (input.ProductTypeId == 0)
                    return BadRequest(new ProductResponseDto { IsSuccess = false, Message = "Debe indicar el Id del tipo de producto." });


                var responseDto = await _productRepository.UpdateProduct(input);

                if (responseDto.IsSuccess)
                    return Ok(responseDto);
                else
                    return BadRequest(responseDto);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error has been in UpdateProduct method : {ex}");

                return StatusCode(500, new ProductResponseDto
                {
                    IsSuccess = false,
                    Message = "Error : " + ex.Message
                });
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductDto>>> GetProduct()
        {
            try
            {
                var productDto = await _productRepository.GetProduct();

                return Ok(productDto);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProduct method : " + ex.Message);

                return StatusCode(500, "Error : " + ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            try
            {
                var productDto = await _productRepository.GetProductById(id);

                if (productDto == null)
                    return NotFound($"El produto con el ID {id} no puede ser encontrado.");
                else
                    return Ok(productDto);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProductById method : " + ex.Message);

                return StatusCode(500, "Error : " + ex.Message);
            }
        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResult<ProductResponseDto>> DeleteProductById(Guid id)
        {
            try
            {
                var responseDto = await _productRepository.DeleteProductById(id);

                if (responseDto == null)
                    return NotFound($"No se ha podido eliminar el producto ID {id}");
                else
                    return Ok(responseDto);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in DeleteProductById method : " + ex.Message);

                return StatusCode(500, "Error : " + ex.Message);
            }
        }
    }
}

