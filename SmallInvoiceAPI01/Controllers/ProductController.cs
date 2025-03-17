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
        public async Task<ProductResponseDto> CreateProduct(ProductDto input)
        {
            bool productExits = false;
            var responseDto = new ProductResponseDto() { IsSuccess = true };

            try
            {
                productExits = await _productRepository.ProductExits(input.ProductName);

                //if (input.ProductName.Trim().IsNullOrEmpty())
                if (string.IsNullOrEmpty(input.ProductName.Trim()))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Debe indicar el nombre del producto.";
                }

                if (productExits)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = $"Ya existe un producto con el nombre [{input.ProductName}]";
                }

                if (input.ProcessModeId == 0)
                {
                    responseDto.Message = $"El Id del modo de proceso no puede ser cero.";
                    responseDto.IsSuccess = false;
                }

                if (responseDto.IsSuccess)
                    responseDto = await _productRepository.CreateProduct(input);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in CreateProduct method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpPost("Update")]
        public async Task<ProductResponseDto> UpdateProduct(UpdateProductDto input)
        {
            bool productExits = false;
            var responseDto = new ProductResponseDto() { IsSuccess = true };

            try
            {
                productExits = await _productRepository.ProductExits(input.ProductName);

                if (string.IsNullOrEmpty(input.ProductName.Trim()))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Debe indicar el nombre del producto.";
                }

                if (productExits)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = $"Ya existe un producto con el nombre [{input.ProductName}]";
                }

                /*
                if (input.ProcessModeId == 0)
                {
                    responseDto.Message = $"El Id del modo de proceso no puede ser cero.";
                    responseDto.IsSuccess = false;
                }
                */

                if (responseDto.IsSuccess)
                    responseDto = await _productRepository.UpdateProduct(input);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in UpdateProduct method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpGet("GetAll")]
        public async Task<List<ProductDto>> GetProduct()
        {

            List<ProductDto>? productDto = new List<ProductDto>();

            try
            {
                productDto = await _productRepository.GetProduct();
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProduct method : " + ex.Message);
            }

            return await Task.Run(() => productDto);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ProductDto> GetProductById(Guid id)
        {
            ProductDto productDto = new ProductDto();

            try
            {
                productDto = await _productRepository.GetProductById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProductById method : " + ex.Message);
            }

            return await Task.Run(() => productDto);
        }

        [HttpPost("Delete/{id}")]
        public async Task<ProductResponseDto> DeleteProductById(Guid id)
        {
            var responseDto = new ProductResponseDto();

            try
            {
                responseDto = await _productRepository.DeleteProductById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in DeleteProductById method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }
    }
}
