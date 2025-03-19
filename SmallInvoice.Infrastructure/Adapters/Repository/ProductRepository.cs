using Microsoft.EntityFrameworkCore;
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
            var refId = Guid.NewGuid();
            var svproduct = Context.SvProduct;
            var productName = input.ProductName.ToUpper();

            bool productExits = await Context.SvProduct.AnyAsync(x => x.ProductName == productName && x.Active == true);

            if (productExits)
                return new ProductResponseDto() { IsSuccess = false, Message = $"Ya existe un producto con el nombre [{productName}]" };

            svproduct.Add(new SvProduct
            {
                RefId = refId,
                ProductTypeId = input.ProductTypeId,
                ProductName = productName,
                ProcessModeId = input.ProcessModeId,
                Active = true
            }
            );

            if (await Context.SaveChangesAsync() > 0)
            {
                return new ProductResponseDto()
                {
                    Product = new ProductDto()
                    {
                        RefId = refId,
                        ProductTypeId = input.ProductTypeId,
                        ProductName = productName,
                        ProcessModeId = input.ProcessModeId,
                        Active = true
                    },
                    IsSuccess = true,
                    Message = $"El producto {input.ProductName} ha sido registrado."
                };
            }

            return new ProductResponseDto() { IsSuccess = false, Message = "No se ha podido registrar el producto." };
        }

        public async Task<ProductResponseDto> UpdateProduct(UpdateProductDto input)
        {
            string productName = input.ProductName.ToUpper();
            var product = Context.SvProduct.Where(i => i.RefId == input.RefId && i.Active == true).FirstOrDefault();

            if (product == null)
                return new ProductResponseDto() { IsSuccess = false, Message = $"No existe el producto con ID {input.RefId}" };

            bool productExits = await Context.SvProduct.AnyAsync(x => x.ProductName == productName && x.Active == true);

            if (productExits)
                return new ProductResponseDto() { IsSuccess = false, Message = $"Ya existe un producto con el nombre [{productName}]" };

            product.ProductName = productName;
            product.ProductTypeId = input.ProductTypeId;

            if (await Context.SaveChangesAsync() > 0)
            {
                return new ProductResponseDto()
                {
                    Product = new ProductDto()
                    {
                        RefId = product.RefId,
                        ProductId = product.ProductId,
                        ProductName = productName,
                        ProductTypeId = input.ProductTypeId,
                        ProcessModeId = product.ProcessModeId,
                        Active = true
                    },
                    IsSuccess = true,
                    Message = $"El producto {productName} ha sido actualizado."
                };
            }

            return new ProductResponseDto() { IsSuccess = false, Message = "No se ha podido actualizar el producto." };
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
                return productDto;
            else
            {
                productDto = new ProductDto()
                {
                    ProductId = product.ProductId,
                    RefId = product.RefId,
                    ProductTypeId = product.ProductTypeId,
                    ProductName = product.ProductName,
                    ProcessModeId = product.ProcessModeId,
                    Active = product.Active
                };

                return productDto;
            }
        }

        public async Task<bool> ProductExits(string productName)
        {
            return await Context.SvProduct.AnyAsync(x => x.ProductName == productName && x.Active == true);
        }

        public async Task<ProductResponseDto> DeleteProductById(Guid Id)
        {
            var product = Context.SvProduct.Where(i => i.RefId == Id && i.Active == true).FirstOrDefault();

            if (product == null)
                return new ProductResponseDto() { IsSuccess = false, Message = $"El producto ID {Id} que ha indicado no existe." };
            else
                product.Active = false;

            if (await Context.SaveChangesAsync() > 0)
            {
                return new ProductResponseDto()
                {
                    Product = new ProductDto()
                    {
                        RefId = product.RefId,
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductTypeId = product.ProductTypeId,
                        ProcessModeId = product.ProcessModeId,
                        Active = true
                    },
                    IsSuccess = true,
                    Message = $"El producto {product.ProductName} ha sido eliminado."
                };
            }

            return new ProductResponseDto() { IsSuccess = false, Message = "No se ha podido eliminar el producto." }; ;
        }
    }
}
