using AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public class IndividualCustomerService : BaseService, IIndividualCustomerService
	{
		private readonly IIndividualCustomerRepository _individualCustomerRepository;
		private readonly ICustomerRepository _customerRepository;

		public IndividualCustomerService(
			IMapper mapper,
			IIndividualCustomerRepository individualCustomerRepository,
			ICustomerRepository customerRepository) : base(mapper)
		{
			_individualCustomerRepository = individualCustomerRepository;
			_customerRepository = customerRepository;
		}

		public async Task<GlobalResponse<IndividualCustomerListDto>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
		{
			try
			{
				var individualCustomer = await _individualCustomerRepository.GetByCustomerId(customerId, cancellationToken);
				if (individualCustomer == null)
					return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 404, Message = "مشتری حقیقی یافت نشد" };

				var result = _mapper.Map<IndividualCustomerListDto>(individualCustomer);
				return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 200, Data = result, Message = "عملیات موفق" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 500, Message = $"خطا در دریافت اطلاعات: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<IndividualCustomerListDto>> Create(IndividualCustomerCreateDto createDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				// Verify customer exists
				var customer = await _customerRepository.GetById(createDto.CustomerId, cancellationToken);
				if (customer == null)
					return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

				var individualCustomer = _mapper.Map<IndividualCustomer>(createDto);
				var created = await _individualCustomerRepository.Create(individualCustomer, userId);
				var result = _mapper.Map<IndividualCustomerListDto>(created);

				return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 201, Data = result, Message = "مشتری حقیقی با موفقیت ایجاد شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 500, Message = $"خطا در ایجاد مشتری حقیقی: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<IndividualCustomerListDto>> Update(IndividualCustomerUpdateDto updateDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var existing = await _individualCustomerRepository.GetById(updateDto.Id, cancellationToken);
				if (existing == null)
					return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 404, Message = "مشتری حقیقی یافت نشد" };

				_mapper.Map(updateDto, existing);
				await _individualCustomerRepository.Update(existing, userId, cancellationToken);
				var result = _mapper.Map<IndividualCustomerListDto>(existing);

				return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 200, Data = result, Message = "مشتری حقیقی با موفقیت بروزرسانی شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<IndividualCustomerListDto> { StatusCode = 500, Message = $"خطا در بروزرسانی مشتری حقیقی: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var individualCustomer = await _individualCustomerRepository.GetById(id, cancellationToken);
				if (individualCustomer == null)
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "مشتری حقیقی یافت نشد",
						Data = new ResponseResultGlobally { DoneSuccessfully = false }
					};

				await _individualCustomerRepository.Delete(id, userId);

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "مشتری حقیقی با موفقیت حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف مشتری حقیقی: {ex.Message}",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};
			}
		}
	}
}