using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using SmallInvoice.Infrastructure.Entity.Data;
using SmallInvoice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmallInvoice.Infrastructure.Adapters.Repository
{
    public class ProductTypeRepository : RepositoryBase, IProductTypeRepository
    {
        public ProductTypeRepository(SmallInvoice025DbContext context)
        {
            this.Context = context;
        }

        public async Task<ProductTypeResponseDto> CreateProductType(string name, int processModeId)
        {
            var svproductType = Context.SvProductType;
            var refId = Guid.NewGuid();

            bool productTypeExits = await Context.SvProductType.AnyAsync(x => x.ProductTypeName == name && x.Active == true);

            if (productTypeExits)
                return new ProductTypeResponseDto() { IsSuccess = false, Message = $"Ya existe un tipo de producto con el nombre [{name}]" };

            svproductType.Add(new SvProductType
            {
                RefId = refId,
                ProductTypeName = name.ToUpper(),
                ProcessModeId = processModeId,
                Active = true
            }
            );

            if (await Context.SaveChangesAsync() > 0)
            {
                return new ProductTypeResponseDto()
                {
                    ProductType = new ProductTypeDto()
                    {
                        RefId = refId,
                        ProductTypeName = name,
                        Active = true
                    },
                    IsSuccess = true,
                    Message = $"El tipo de producto {name} ha sido registrado."
                };
            }

            return new ProductTypeResponseDto() { IsSuccess = false, Message = "No se ha podido registrar el tipo de producto." };
        }

        public async Task<ProductTypeResponseDto> UpdateProductType(ProductTypeDto input)
        {
            string productName = input.ProductTypeName.ToUpper();
            var product = Context.SvProductType.Where(i => i.RefId == input.RefId && i.Active == true).FirstOrDefault();

            if (product == null)
                return new ProductTypeResponseDto() { IsSuccess = false, Message = $"No existe el tipo de producto con ID {input.RefId}" };

            bool productExits = await Context.SvProductType.AnyAsync(x => x.ProductTypeName == productName && x.Active == true);

            if (productExits)
                return new ProductTypeResponseDto() { IsSuccess = false, Message = $"Ya existe un tipo de producto con el nombre [{productName}]" };

            product.ProductTypeName = productName;
            product.ProductTypeId = input.ProductTypeId;

            if (await Context.SaveChangesAsync() > 0)
            {
                return new ProductTypeResponseDto()
                {
                    ProductType = new ProductTypeDto()
                    {
                        RefId = product.RefId,
                        ProductTypeId = product.ProductTypeId,
                        ProductTypeName = productName,
                        /*ProductTypeId = input.ProductTypeId,
                        ProcessModeId = product.ProcessModeId,*/
                        Active = true
                    },
                    IsSuccess = true,
                    Message = $"El tipo de producto {productName} ha sido actualizado."
                };
            }

            return new ProductTypeResponseDto() { IsSuccess = false, Message = "No se ha podido actualizar el tipo de producto." };
        }

        public async Task<List<ProductTypeDto>> GetProductTypes()
        {
            var lstproductType = Context.SvProductType.Where(x => x.Active == true).ToList();

            List<ProductTypeDto>? lst = lstproductType.ConvertAll(x =>
            {
                return new ProductTypeDto()
                {
                    ProductTypeId = x.ProductTypeId,
                    RefId = x.RefId,
                    ProductTypeName = x.ProductTypeName,
                    Active = x.Active
                };
            });

            return await Task.Run(() => lst);
        }

        public async Task<ProductTypeDto> GetProductTypeById(Guid id)
        {
            ProductTypeDto productTypeDto = null;
            var productType = Context.SvProductType.Where(x => x.RefId == id && x.Active == true).FirstOrDefault();

            if (productType == null)
                return productTypeDto;

            productTypeDto = new ProductTypeDto()
            {
                ProductTypeId = productType.ProductTypeId,
                RefId = productType.RefId,
                ProductTypeName = productType.ProductTypeName,
                Active = productType.Active
            };

            return productTypeDto;
        }

        public async Task<bool> ProductTypeExits(string productTypeName)
        {
            bool response = false;
            var productType = Context.SvProductType.Where(x => x.ProductTypeName == productTypeName && x.Active == true).FirstOrDefault();

            if (productType != null)
                response = true;

            return await Task.Run(() => response);
        }

        public async Task<ProductTypeResponseDto> DeleteProductTypeById(Guid Id)
        {
            var product = Context.SvProductType.Where(i => i.RefId == Id && i.Active == true).FirstOrDefault();

            if (product == null)
                return new ProductTypeResponseDto() { IsSuccess = false, Message = $"El tipo de producto ID {Id} que ha indicado no existe." };
            else
                product.Active = false;

            if (await Context.SaveChangesAsync() > 0)
            {
                return new ProductTypeResponseDto()
                {
                    ProductType = new ProductTypeDto()
                    {
                        RefId = product.RefId,
                        ProductTypeId = product.ProductTypeId,
                        ProductTypeName = product.ProductTypeName,
                        //ProductTypeId = product.ProductTypeId,
                        //ProcessModeId = product.ProcessModeId,
                        Active = true
                    },
                    IsSuccess = true,
                    Message = $"El tipo de producto {product.ProductTypeName} ha sido eliminado."
                };
            }

            return new ProductTypeResponseDto() { IsSuccess = false, Message = "No se ha podido eliminar el tipo de producto." };
        }
    }
}
