using Microsoft.AspNetCore.Mvc;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using log4net;
using SmallInvoiceAPI01.Constats;

namespace SmallInvoiceAPI01.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ProductController));

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "CreateProduct")]
        public async Task<ProductResponseDto> CreateProduct(ProductDto input)
        {
            bool productExits = false;
            var responseDto = new ProductResponseDto() { IsSuccess = true };

            try
            {
                productExits = await this.productRepository.ProductExits(input.ProductName);

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
                    responseDto = this.productRepository.CreateProduct(input).Result;
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in CreateProduct method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "UpdateProduct")]
        public async Task<ProductResponseDto> UpdateProduct(ProductDto input)
        {
            bool productExits = false;
            var responseDto = new ProductResponseDto();

            try
            {
                productExits = await this.productRepository.ProductExits(input.ProductName);

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
                    responseDto = await this.productRepository.UpdateProduct(input);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in UpdateProduct method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetProduct")]
        public async Task<List<ProductDto>> GetProduct()
        {

            List<ProductDto>? productDto = new List<ProductDto>();

            try
            {
                productDto = await this.productRepository.GetProduct();
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProduct method : " + ex.Message);
            }

            return await Task.Run(() => productDto);
        }

        [HttpGet]
        [Route(Prefix.API_ROUT + "GetProductById")]
        public async Task<ProductDto> GetProductById(Guid id)
        {
            ProductDto productDto = new ProductDto();

            try
            {
                productDto = await this.productRepository.GetProductById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in GetProductById method : " + ex.Message);
            }

            return await Task.Run(() => productDto);
        }

        [HttpPost]
        [Route(Prefix.API_ROUT + "DeleteProductById")]
        public async Task<ProductResponseDto> DeleteProductById(Guid id)
        {
            var responseDto = new ProductResponseDto();

            try
            {
                responseDto = await this.productRepository.DeleteProductById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error has been in DeleteProductById method : " + ex.Message);
            }

            return await Task.Run(() => responseDto);
        }
    }
}
