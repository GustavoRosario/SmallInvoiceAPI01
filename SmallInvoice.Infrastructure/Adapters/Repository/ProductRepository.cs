using SmallInvoice.Application.Dto;
using SmallInvoice.Domain.Entities;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Infrastructure.Entity.Data;

namespace SmallInvoice.Infrastructure.Adapters.Repository
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(SmallInvoice025DbContext context)
        {
            this.Context = context;
        }

        public async Task<ProductResponseDto> CreateProduct(ProductDto input)
        {
            var responseDto = new ProductResponseDto();
            var svproduct = Context.Set<SvProduct>();
            var refId = Guid.NewGuid();
            var ProductDto = new ProductDto();

            svproduct.Add(new SvProduct
            {
                RefId = refId,
                ProductTypeId = input.ProductTypeId,
                ProductName = input.ProductName.ToUpper(),
                //Price = input.Price,
                ProcessModeId = input.ProcessModeId,
                Active = true
            }
            );

            AffectedRecords = await Context.SaveChangesAsync();

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                /*responseDto.Date = DateTime.Now.ToShortDateString();
                responseDto.Time = DateTime.Now.ToShortDateString();*/
                ProductDto.RefId = refId;
                ProductDto.ProductTypeId = input.ProductTypeId;
                ProductDto.ProductName = input.ProductName.ToUpper();
                //ProductDto.Price = input.Price;
                ProductDto.ProcessModeId = input.ProcessModeId;
                ProductDto.Active = true;
                responseDto.Product = ProductDto;
                responseDto.Message = $"El producto {input.ProductName} ha sido registrado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }

        public async Task<ProductResponseDto> UpdateProduct(ProductDto input)
        {
            var responseDto = new ProductResponseDto();
            var productDto = new ProductDto();

            var producType = Context.SvProductType.Where(i => i.RefId == input.RefId && i.Active == true).FirstOrDefault();

            if (producType != null)
            {
                producType.ProductTypeName = input.ProductName.ToUpper();

                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                /*responseDto.Date = DateTime.Now.ToShortDateString();
                responseDto.Time = DateTime.Now.ToShortDateString();*/
                productDto.RefId = producType.RefId;
                productDto.ProductName = input.ProductName.ToUpper();
                productDto.Active = true;
                responseDto.Product = productDto;
                responseDto.Message = $"El producto {input.ProductName} ha sido actualizado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }

        public async Task<List<ProductDto>> GetProduct()
        {
            var lstproduct = Context.SvProduct.Where(x => x.Active == true).ToList();

            List<ProductDto>? lst = lstproduct.ConvertAll(x =>
            {
                return new ProductDto()
                {
                    ProductId = x.ProductId,
                    RefId = x.RefId,
                    ProductTypeId = x.ProductTypeId,
                    ProductName = x.ProductName,
                    ProcessModeId = x.ProcessModeId,
                    Active = x.Active
                };
            });

            return await Task.Run(() => lst);
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            ProductDto productDto = null;
            var product = Context.SvProduct.Where(x => x.RefId == id && x.Active == true).FirstOrDefault();

            if (product == null)
            {
                productDto = new ProductDto();
                return await Task.Run(() => productDto);
            }

            productDto = new ProductDto()
            {
                ProductId = product.ProductId,
                RefId = product.RefId,
                ProductTypeId = product.ProductTypeId,
                ProductName = product.ProductName,
                ProcessModeId = product.ProcessModeId,
                Active = product.Active
            };

            return await Task.Run(() => productDto);
        }

        public async Task<bool> ProductExits(string productName)
        {
            bool response = false;
            var product = Context.SvProduct.Where(x => x.ProductName == productName && x.Active == true).FirstOrDefault();

            if (product != null)
                response = true;

            return await Task.Run(() => response);
        }

        public async Task<ProductResponseDto> DeleteProductById(Guid Id)
        {
            var responseDto = new ProductResponseDto();
            var productDto = new ProductDto();

            var product = Context.SvProduct.Where(i => i.RefId == Id && i.Active == true).FirstOrDefault();

            if (product != null)
            {
                product.Active = false;
                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {

                productDto.RefId = Id;
                productDto.ProductId = product.ProductId;
                productDto.ProductTypeId = product.ProductTypeId;
                productDto.ProductName = product.ProductName.ToUpper();
                productDto.ProcessModeId = product.ProcessModeId;
                productDto.Active = false;
                responseDto.IsSuccess = true;
                responseDto.Product = productDto;
                responseDto.Message = $"El tipo de producto {product.ProductName} ha sido eliminado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }
    }
}
