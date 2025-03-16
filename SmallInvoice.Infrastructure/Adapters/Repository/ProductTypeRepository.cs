using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Application.Dto;
using SmallInvoice.Infrastructure.Entity.Data;
//using SmallInvoice.Infrastructure.Adapters.Repository;
using SmallInvoice.Domain.Entities;
using System.Xml.Linq;

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
            var responseDto = new ProductTypeResponseDto();
            var svproductType = Context.Set<SvProductType>();
            var refId = Guid.NewGuid();
            var ProductTypeDto = new ProductTypeDto();

            svproductType.Add(new SvProductType
            {
                ProductTypeName = name.ToUpper(),
                RefId = refId,
                ProcessModeId = processModeId,
                Active = true
            }
            );

            AffectedRecords = await Context.SaveChangesAsync();

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                ProductTypeDto.RefId = refId;
                ProductTypeDto.ProductTypeName = name.ToUpper();
                ProductTypeDto.Active = true;
                responseDto.ProductType = ProductTypeDto;
                responseDto.Message = $"El tipo de producto {name} ha sido registrado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }

        public async Task<ProductTypeResponseDto> UpdateProductType(ProductTypeDto input)
        {
            var responseDto = new ProductTypeResponseDto();
            var productTypeDto = new ProductTypeDto();
            //Guid refId = Guid.NewGuid()

            var producType = Context.SvProductType.Where(i => i.RefId == input.RefId && i.Active == true).FirstOrDefault();

            if (producType != null)
            {
                producType.ProductTypeName = input.ProductTypeName.ToUpper();
                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                productTypeDto.RefId = producType.RefId;
                productTypeDto.ProductTypeName = input.ProductTypeName.ToUpper();
                productTypeDto.Active = true;
                responseDto.ProductType = productTypeDto;
                responseDto.Message = $"El tipo de producto {input.ProductTypeName} ha sido actualizado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
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
            {
                productTypeDto = new ProductTypeDto();
                return await Task.Run(() => productTypeDto);
            }

            productTypeDto = new ProductTypeDto()
            {
                ProductTypeId = productType.ProductTypeId,
                RefId = productType.RefId,
                ProductTypeName = productType.ProductTypeName,
                Active = productType.Active
            };

            return await Task.Run(() => productTypeDto);
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
            var responseDto = new ProductTypeResponseDto();
            var productTypeDto = new ProductTypeDto();

            var producType = Context.SvProductType.Where(i => i.RefId == Id && i.Active == true).FirstOrDefault();

            if (producType != null)
            {
                producType.Active = false;
                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                productTypeDto.RefId = Id;
                productTypeDto.ProductTypeName = producType.ProductTypeName.ToUpper();
                productTypeDto.Active = false;
                responseDto.ProductType = productTypeDto;
                responseDto.Message = $"El tipo de producto {producType.ProductTypeName} ha sido eliminado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }
    }
}
