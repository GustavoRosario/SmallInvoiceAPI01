using SmallInvoice.Application.Dto;
using SmallInvoice.Domain.Entities;
using SmallInvoice.Domain.Ports.Repository;
using SmallInvoice.Infrastructure.Entity.Data;

namespace SmallInvoice.Infrastructure.Adapters.Repository
{
    public class PriceListRepository : RepositoryBase, IPriceListRepository
    {
        private ICustomerRepository _customerRepository;
        public PriceListRepository(SmallInvoice025DbContext context, ICustomerRepository customerRepository)
        {
            this.Context = context;
            _customerRepository = customerRepository;
        }

        public async Task<PriceListResponseDto> CreatePriceList(List<CreatePriceListDto> input)
        {
            var responseDto = new PriceListResponseDto();
            var priceListDto = new PriceListDto();
            var svPriceList = Context.Set<SvPriceList>();
            string customerName = string.Empty;
            int elementCount = input.Count - 1;
            Guid refId = Guid.NewGuid();

            #region ORIGINAL_CODE
            /*
            if (input != null)
            {
                if (input.Count > 0)
                {
                    for (int x = 0; x < input.Count; x++)
                    {
                        svPriceList.Add(new SvPriceList
                        {
                            RefId = refId,
                            CustomerId = input[x].CustomerId,
                            ProductId = input[x].ProductId,
                            ProductCode = input[x].ProductCode,
                            Price = input[x].Price,
                            ProcessModeId = input[x].ProcessModeId,
                            Active = true
                        }
                            );
                        AffectedRecords = await Context.SaveChangesAsync();

                        if (AffectedRecords > 0 && elementCount == x)
                        {
                            customerName = GetCustomerNameById(input[0].CustomerId);
                            priceListDto.RefId = refId;
                            priceListDto.CustomerId = input[0].CustomerId;
                            priceListDto.ProductId = input[0].ProductId;
                            priceListDto.Price = 0.00M;
                            responseDto.IsSuccess = true;
                            responseDto.PriceList = priceListDto;
                            responseDto.Message = $"Se ha registrado la lista de precios para el cliente {customerName}.";
                            AffectedRecords = 0;
                        }
                    }
                }
            }
            */
            #endregion

            if (input == null)
                return await Task.Run(() => responseDto);

            if (input.Count > 0)
            {
                for (int x = 0; x < input.Count; x++)
                {
                    svPriceList.Add(new SvPriceList
                    {
                        RefId = refId,
                        CustomerId = input[x].CustomerId,
                        ProductId = input[x].ProductId,
                        ProductCode = input[x].ProductCode,
                        Price = input[x].Price,
                        ProcessModeId = input[x].ProcessModeId,
                        Active = true
                    }
                        );
                    AffectedRecords = await Context.SaveChangesAsync();

                    if (AffectedRecords > 0 && elementCount == x)
                    {
                        customerName = await _customerRepository.GetCustomerNameById(input[0].CustomerId);
                        priceListDto.RefId = refId;
                        priceListDto.CustomerId = input[0].CustomerId;
                        priceListDto.ProductId = input[0].ProductId;
                        priceListDto.Price = 0.00M;
                        responseDto.IsSuccess = true;
                        responseDto.PriceList = priceListDto;
                        responseDto.Message = $"Se ha registrado la lista de precios para el cliente {customerName}.";
                        AffectedRecords = 0;
                    }
                }
            }

            return await Task.Run(() => responseDto);
        }

        public Task<PriceListResponseDto> CreatePriceList(CreatePriceListDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PriceListDto>> GetPriceList()
        {
            var lstPriceList = Context.SvPriceList.Where(x => x.Active == true).ToList();

            List<PriceListDto>? lst = lstPriceList.ConvertAll(x =>
            {
                return new PriceListDto()
                {
                    PriceListId = x.PriceListId,
                    RefId = x.RefId,
                    CustomerId = x.CustomerId,
                    ProductId = x.ProductId,
                    //ProductName = x.Product.,//lstPriceList[0].Product.ProductName,
                    Price = x.Price,
                    Active = x.Active
                };
            });

            return await Task.Run(() => lst);
        }

        public async Task<List<PriceListDto>> GetPriceListByCustomerId(int customerId)
        {
            var lstPriceList = Context.SvPriceList.Where(x => x.CustomerId == customerId && x.Active == true).ToList();

            List<PriceListDto>? lst = lstPriceList.ConvertAll(x =>
            {
                return new PriceListDto()
                {
                    PriceListId = x.PriceListId,
                    RefId = x.RefId,
                    CustomerId = x.CustomerId,
                    ProductId = x.ProductId,
                    Price = x.Price,
                    Active = x.Active
                };
            });

            return await Task.Run(() => lst);
        }

        public async Task<PriceListResponseDto> DeleteProductOfPriceListById(int productId, int customerId)
        {
            var responseDto = new PriceListResponseDto();
            var priceListDto = new PriceListDto();

            var priceList = Context.SvPriceList.Where(x => x.ProductId == productId && x.CustomerId == customerId && x.Active == true).FirstOrDefault();

            if (priceList != null)
            {
                priceList.Active = false;
                AffectedRecords = await Context.SaveChangesAsync();
            }

            if (AffectedRecords > 0)
            {
                priceListDto.RefId = priceList.RefId;
                priceList.CustomerId = priceList.CustomerId;
                priceListDto.ProductId = priceList.ProductId;
                //priceListDto.ProductName = priceList.Product.ProductName;
                priceListDto.Price = priceList.Price;
                priceListDto.Active = priceList.Active;
                responseDto.PriceList = priceListDto;
                responseDto.IsSuccess = true;
                responseDto.Message = $"Se ha eliminado el producto {""} de la lista.";
            }

            return await Task.Run(() => responseDto);
        }

        public async Task<PriceListResponseDto> UpdateProductOfPriceListById(int productId)
        {
            var responseDto = new PriceListResponseDto();
            var priceListDto = new PriceListDto();

            var priceList = Context.SvPriceList.Where(x => x.ProductId == productId && x.Active == true).FirstOrDefault();

            if (priceList != null)
            {
                //priceList.Active = false;

                AffectedRecords = Context.SaveChanges();
            }

            if (AffectedRecords > 0)
            {
                priceListDto.RefId = priceList.RefId;

                responseDto.IsSuccess = true;
            }

            return await Task.Run(() => responseDto);
        }

    }

}
