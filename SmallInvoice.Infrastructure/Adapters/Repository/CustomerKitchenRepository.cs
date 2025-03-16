using SmallInvoice.Application.Dto;
using SmallInvoice.Domain.Entities;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Infrastructure.Entity.Data;

namespace SmallInvoice.Infrastructure.Adapters.Repository
{
    public class CustomerKitchenRepository : RepositoryBase, ICustomerKitchenRepository
    {
        public CustomerKitchenRepository(SmallInvoice025DbContext context)
        {
            this.Context = context;
        }

        public async Task<CustomerKitchenResponseDto> CreateCustomerKitchen(CreateCustomerKitchenDto input)
        {
            var responseDto = new CustomerKitchenResponseDto();
            var svcustomerKitchen = Context.Set<SvCustomerKitchen>();
            var customerDto = new CustomerKitchenDto();
            var refId = Guid.NewGuid();

            svcustomerKitchen.Add(new SvCustomerKitchen
            {
                RefId = refId,
                CustomerKitchenName = input.CustomerKitchenName.ToUpper(),
                CustomerId = input.CustomerId,
                ProcessModeId = input.ProcessModeId,
                Active = true
            }
            );

            AffectedRecords = await Context.SaveChangesAsync();

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                customerDto.RefId = refId;
                //customerDto.CustomerId = input.CustomerId;
                customerDto.CustomerKitchenName = input.CustomerKitchenName.ToUpper();
                //customerDto.ProcessModeId = input.ProcessModeId;
                customerDto.Active = true;
                responseDto.CustomerKitchen = customerDto;
                responseDto.Message = $"La cocina con la descripción {input.CustomerKitchenName} ha sido registrado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }

        public async Task<bool> CustomerKitchenExits(string customerKitchenName)
        {
            bool response = false;
            var customerKitchen = Context.SvCustomerKitchen.Where(x => x.CustomerKitchenName == customerKitchenName && x.Active == true).FirstOrDefault();

            if (customerKitchen != null)
                response = true;

            return await Task.Run(() => response);
        }

        public async Task<CustomerKitchenResponseDto> DeleteCustomerKitchenById(Guid Id)
        {
            var responseDto = new CustomerKitchenResponseDto();
            var customerDto = new CustomerKitchenDto();

            var product = Context.SvCustomerKitchen.Where(i => i.RefId == Id && i.Active == true).FirstOrDefault();

            if (product != null)
            {
                product.Active = false;
                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                customerDto.RefId = Id;
                customerDto.CustomerKitchenName = product.CustomerKitchenName.ToUpper();
                customerDto.Active = false;
                responseDto.CustomerKitchen = customerDto;
                responseDto.Message = $"El cliente {product.CustomerKitchenName} ha sido eliminado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }

        public async Task<List<CustomerKitchenDto>> GetCustomerKitchen()
        {
            var lstCustomer = Context.SvCustomerKitchen.Where(x => x.Active == true).ToList();

            List<CustomerKitchenDto>? lst = lstCustomer.ConvertAll(x =>
            {
                return new CustomerKitchenDto()
                {
                    CustomerKitchenId = x.CustomerKitchenId,
                    RefId = x.RefId,
                    CustomerKitchenName = x.CustomerKitchenName,
                    Active = x.Active
                };
            });

            return await Task.Run(() => lst);
        }

        public async Task<CustomerKitchenDto> GetCustomerKitchenById(Guid refId)
        {
            CustomerKitchenDto kitchenDto = null;
            var customerKitchen = Context.SvCustomerKitchen.Where(x => x.RefId == refId && x.Active == true).FirstOrDefault();

            if (customerKitchen == null)
            {
                kitchenDto = new CustomerKitchenDto();
                return await Task.Run(() => kitchenDto);
            }

            kitchenDto = new CustomerKitchenDto()
            {
                CustomerKitchenId = customerKitchen.CustomerKitchenId,
                RefId = customerKitchen.RefId,
                CustomerKitchenName = customerKitchen.CustomerKitchenName,
                Active = customerKitchen.Active
            };

            return await Task.Run(() => kitchenDto);
        }

        public async Task<List<CustomerKitchenDto>> GetCustomerKitchenByCustomerId(int customerId)
        {
            var lstCustomer = Context.SvCustomerKitchen.Where(x => x.CustomerId == customerId && x.Active == true).ToList();

            List<CustomerKitchenDto>? lst = lstCustomer.ConvertAll(x =>
            {
                return new CustomerKitchenDto()
                {
                    CustomerKitchenId = x.CustomerKitchenId,
                    RefId = x.RefId,
                    CustomerKitchenName = x.CustomerKitchenName,
                    Active = x.Active
                };
            });

            return await Task.Run(() => lst);
        }

        public async Task<CustomerKitchenResponseDto> UpdateCustomerKitchen(UpdateCustomerKitchenDto input)
        {
            var responseDto = new CustomerKitchenResponseDto();
            var customerDto = new CustomerKitchenDto();

            var customer = Context.SvCustomerKitchen.Where(i => i.RefId == input.RefId && i.Active == true).FirstOrDefault();

            if (customer != null)
            {
                customer.CustomerKitchenName = input.CustomerKitchenName.ToUpper();
                customer.CustomerId = input.CustomerId;

                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                customerDto.RefId = customer.RefId;
                customerDto.CustomerKitchenName = input.CustomerKitchenName.ToUpper();
                customerDto.Active = true;
                responseDto.CustomerKitchen = customerDto;
                responseDto.Message = $"El producto {input.CustomerKitchenName} ha sido actualizado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }
    }
}
