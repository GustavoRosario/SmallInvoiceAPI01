using SmallInvoice.Application.Dto;
using SmallInvoice.Domain.Entities;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Infrastructure.Entity.Data;

namespace SmallInvoice.Infrastructure.Adapters.Repository
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public CustomerRepository(SmallInvoice025DbContext context)
        {
            this.Context = context;
        }

        public async Task<CustomerResponseDto> CreateCustomer(CreateCustomerDto input)
        {
            var responseDto = new CustomerResponseDto();
            var svcustomer = Context.Set<SvCustomer>();
            var refId = Guid.NewGuid();
            var customerDto = new CustomerDto();

            svcustomer.Add(new SvCustomer
            {
                RefId = refId,
                //CustomerId = input.CustomerId,
                CustomerName = input.CustomerName.ToUpper(),
                Rnc = input.Rnc,
                AddressCustomer = input.AddressCustomer,
                PhoneNumber = input.PhoneNumber,
                Email = input.Email,
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
                customerDto.CustomerName = input.CustomerName.ToUpper();
                customerDto.ProcessModeId = input.ProcessModeId;
                customerDto.Active = true;
                responseDto.Customer = customerDto;
                responseDto.Message = $"El cliente {input.CustomerName} ha sido registrado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }

        public async Task<CustomerResponseDto> DeleteCustomerById(Guid Id)
        {
            var responseDto = new CustomerResponseDto();
            var customerDto = new CustomerDto();

            var customer = Context.SvCustomer.Where(i => i.RefId == Id && i.Active == true).FirstOrDefault();

            if (customer != null)
            {
                customer.Active = false;
                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                customerDto.CustomerId = customer.CustomerId;
                customerDto.RefId = Id;
                customerDto.CustomerName = customer.CustomerName.ToUpper();
                customerDto.Rnc = customer.Rnc;
                customerDto.AddressCustomer = customer.AddressCustomer;
                customerDto.PhoneNumber = customer.PhoneNumber;
                customerDto.ProcessModeId = customer.ProcessModeId;
                customerDto.Active = false;
                responseDto.Customer = customerDto;
                responseDto.Message = $"El cliente {customer.CustomerName} ha sido eliminado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }

        public async Task<List<CustomerDto>> GetCustomer()
        {
            var lstCustomer = Context.SvCustomer.Where(x => x.Active == true).ToList();

            List<CustomerDto>? lst = lstCustomer.ConvertAll(x =>
            {
                return new CustomerDto()
                {
                    CustomerId = x.CustomerId,
                    RefId = x.RefId,
                    CustomerName = x.CustomerName,
                    Rnc = x.Rnc,
                    AddressCustomer = x.AddressCustomer,
                    PhoneNumber = x.PhoneNumber,
                    ProcessModeId = x.ProcessModeId,
                    Active = x.Active
                };
            });

            return await Task.Run(() => lst);
        }
        
        public async Task<CustomerDto> GetCustomerById(Guid id)
        {
            CustomerDto customerDto = null;
            var customer = Context.SvCustomer.Where(x => x.RefId == id && x.Active == true).FirstOrDefault();

            if (customer == null)
            {
                customerDto = new CustomerDto();
                return await Task.Run(() => customerDto);
            }

            customerDto = new CustomerDto()
            {
                CustomerId = customer.CustomerId,
                RefId = customer.RefId,
                CustomerName = customer.CustomerName,
                Rnc = customer.Rnc,
                AddressCustomer = customer.AddressCustomer,
                PhoneNumber = customer.PhoneNumber,
                ProcessModeId = customer.ProcessModeId,
                Active = customer.Active
            };

            return await Task.Run(() => customerDto);
        }

        public Task<string> GetCustomerNameById(int id)
        {
            var customer = Context.SvCustomer.Where(i => i.CustomerId == id && i.Active == true).FirstOrDefault();
            string customerName = string.Empty;

            if (customer != null)
                customerName = customer.CustomerName;

            return Task.Run(() => customerName);
        }

        public async Task<bool> CustomerExits(string customerName)
        {
            bool response = false;
            var customer = Context.SvCustomer.Where(x => x.CustomerName == customerName && x.Active == true).FirstOrDefault();

            if (customer != null)
                response = true;

            return await Task.Run(() => response);
        }

        public async Task<CustomerResponseDto> UpdateCustomer(UpdateCustomerDto input)
        {
            var responseDto = new CustomerResponseDto();
            var customerDto = new CustomerDto();

            var customer = Context.SvCustomer.Where(i => i.RefId == input.RefId && i.Active == true).FirstOrDefault();

            if (customer != null)
            {
                customer.CustomerName = input.CustomerName.ToUpper();
                customer.Rnc = input.Rnc;
                customer.AddressCustomer = input.AddressCustomer;
                customer.PhoneNumber = input.PhoneNumber;

                if (input.Email != null)
                {
                    customer.Email = input.Email;
                }

                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {
                responseDto.IsSuccess = true;
                customerDto.CustomerId = customer.CustomerId;
                customerDto.RefId = customer.RefId;
                customerDto.CustomerName = customer.CustomerName.ToUpper();
                customerDto.Rnc = customer.Rnc;
                customerDto.AddressCustomer = customer.AddressCustomer;
                customerDto.PhoneNumber = customer.PhoneNumber;
                customerDto.ProcessModeId = customer.ProcessModeId;
                customerDto.Active = true;
                responseDto.Customer = customerDto;
                responseDto.Message = $"El cliente {input.CustomerName} ha sido actualizado.";
                AffectedRecords = 0;
            }

            return await Task.Run(() => responseDto);
        }
    }
}
