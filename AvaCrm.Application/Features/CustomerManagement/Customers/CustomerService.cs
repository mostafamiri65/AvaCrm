using AutoMapper;
using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;
using AvaCrm.Domain.Enums.CustomerManagement;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Application.Features.CustomerManagement.Customers;

public class CustomerService : BaseService, ICustomerService
{
	private readonly ICustomerRepository _customerRepository;
	private readonly IIndividualCustomerRepository _individualCustomerRepository;
	private readonly IOrganizationCustomerRepository _organizationCustomerRepository;
	private readonly ICustomerTagRepository _customerTagRepository;

	public CustomerService(
		IMapper mapper,
		ICustomerRepository customerRepository,
		IIndividualCustomerRepository individualCustomerRepository,
		IOrganizationCustomerRepository organizationCustomerRepository,
		ICustomerTagRepository customerTagRepository) : base(mapper)
	{
		_customerRepository = customerRepository;
		_individualCustomerRepository = individualCustomerRepository;
		_organizationCustomerRepository = organizationCustomerRepository;
		_customerTagRepository = customerTagRepository;
	}

	public async Task<GlobalResponse<CustomerDetailDto>> GetCustomerById(long id, CancellationToken cancellationToken = default)
	{
		try
		{
			var customer = await _customerRepository.GetCustomerWithDetails(id, cancellationToken);
			if (customer == null)
				return new GlobalResponse<CustomerDetailDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

			var result = _mapper.Map<CustomerDetailDto>(customer);
			return new GlobalResponse<CustomerDetailDto> { StatusCode = 200, Data = result, Message = "عملیات موفق" };
		}
		catch (Exception ex)
		{
			return new GlobalResponse<CustomerDetailDto> { StatusCode = 500, Message = $"خطا در دریافت اطلاعات: {ex.Message}" };
		}
	}

	public async Task<GlobalResponse<PaginatedResult<CustomerListDto>>> GetAllCustomers(PaginationRequest request,long userId, CancellationToken cancellationToken = default)
	{
		try
		{
			var query =await _customerRepository.GetAllCustomers(userId);

			// Apply search filter
			if (!string.IsNullOrWhiteSpace(request.SearchTerm))
			{
				query = query.Where(c => c.Code.Contains(request.SearchTerm) ||
										c.Email.Contains(request.SearchTerm) ||
										c.PhoneNumber.Contains(request.SearchTerm));
			}

			// Apply sorting
			if (!string.IsNullOrWhiteSpace(request.SortColumn))
			{
				query = request.SortColumn.ToLower() switch
				{
					"code" => request.SortDirection == "desc" ? query.OrderByDescending(c => c.Code) : query.OrderBy(c => c.Code),
					"email" => request.SortDirection == "desc" ? query.OrderByDescending(c => c.Email) : query.OrderBy(c => c.Email),
					_ => query.OrderByDescending(c => c.CreationDate)
				};
			}
			else
			{
				query = query.OrderByDescending(c => c.CreationDate);
			}

			var totalCount = await query.CountAsync(cancellationToken);

			// Apply pagination
			var customers = await query
				.Skip((request.PageNumber - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToListAsync(cancellationToken);

			var result = new PaginatedResult<CustomerListDto>
			{
				Items = _mapper.Map<List<CustomerListDto>>(customers),
				TotalCount = totalCount,
				PageNumber = request.PageNumber,
				PageSize = request.PageSize
			};

			return new GlobalResponse<PaginatedResult<CustomerListDto>>
			{
				StatusCode = 200,
				Data = result,
				Message = "عملیات موفق"
			};
		}
		catch (Exception ex)
		{
			return new GlobalResponse<PaginatedResult<CustomerListDto>>
			{
				StatusCode = 500,
				Message = $"خطا در دریافت لیست مشتریان: {ex.Message}"
			};
		}
	}

	public async Task<GlobalResponse<CustomerListDto>> CreateCustomer(CustomerCreateDto createDto, long userId, CancellationToken cancellationToken = default)
	{
		try
		{
			// Check if code is unique
			if (!await _customerRepository.IsCodeUnique(createDto.Code, null, cancellationToken))
				return new GlobalResponse<CustomerListDto> { StatusCode = 400, Message = "کد مشتری باید یکتا باشد" };

			var customer = _mapper.Map<Customer>(createDto);

			var createdCustomer = await _customerRepository.Create(customer, userId);

			// Create specific customer type
			if (createDto.CustomerType == CustomerType.Individual && createDto.IndividualCustomer != null)
			{
				var individualCustomer = _mapper.Map<IndividualCustomer>(createDto.IndividualCustomer);
				individualCustomer.CustomerId = createdCustomer.Id;
				await _individualCustomerRepository.Create(individualCustomer, userId);
			}
			else if (createDto.CustomerType == CustomerType.Organization && createDto.OrganizationCustomer != null)
			{
				var organizationCustomer = _mapper.Map<OrganizationCustomer>(createDto.OrganizationCustomer);
				organizationCustomer.CustomerId = createdCustomer.Id;
				await _organizationCustomerRepository.Create(organizationCustomer, userId);
			}

			// Add tags if provided
			if (createDto.TagIds != null && createDto.TagIds.Any())
			{
				foreach (var tagId in createDto.TagIds)
				{
					var customerTag = new CustomerTag
					{
						CustomerId = createdCustomer.Id,
						TagId = tagId
					};
					await _customerTagRepository.CreateCustomerTag(customerTag);
				}
			}

			var result = _mapper.Map<CustomerListDto>(createdCustomer);
			return new GlobalResponse<CustomerListDto> { StatusCode = 201, Data = result, Message = "مشتری با موفقیت ایجاد شد" };
		}
		catch (Exception ex)
		{
			return new GlobalResponse<CustomerListDto> { StatusCode = 500, Message = $"خطا در ایجاد مشتری: {ex.Message}" };
		}
	}

	public async Task<GlobalResponse<CustomerListDto>> UpdateCustomer(CustomerUpdateDto updateDto, long userId, CancellationToken cancellationToken = default)
	{
		try
		{
			var existingCustomer = await _customerRepository.GetById(updateDto.Id, cancellationToken);
			if (existingCustomer == null)
				return new GlobalResponse<CustomerListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

			// Check if code is unique (excluding current customer)
			if (!await _customerRepository.IsCodeUnique(updateDto.Code, updateDto.Id, cancellationToken))
				return new GlobalResponse<CustomerListDto> { StatusCode = 400, Message = "کد مشتری باید یکتا باشد" };

			_mapper.Map(updateDto, existingCustomer);
			await _customerRepository.Update(existingCustomer, userId, cancellationToken);

			var result = _mapper.Map<CustomerListDto>(existingCustomer);
			return new GlobalResponse<CustomerListDto> { StatusCode = 200, Data = result, Message = "مشتری با موفقیت بروزرسانی شد" };
		}
		catch (Exception ex)
		{
			return new GlobalResponse<CustomerListDto> { StatusCode = 500, Message = $"خطا در بروزرسانی مشتری: {ex.Message}" };
		}
	}

	public async Task<GlobalResponse<ResponseResultGlobally>> DeleteCustomer(long id, long userId, CancellationToken cancellationToken = default)
	{
		try
		{
			var customer = await _customerRepository.GetById(id, cancellationToken);
			if (customer == null)
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 404,
					Message = "مشتری یافت نشد",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};

			await _customerRepository.Delete(id, userId);

			return new GlobalResponse<ResponseResultGlobally>
			{
				StatusCode = 200,
				Message = "مشتری با موفقیت حذف شد",
				Data = new ResponseResultGlobally { DoneSuccessfully = true }
			};
		}
		catch (Exception ex)
		{
			return new GlobalResponse<ResponseResultGlobally>
			{
				StatusCode = 500,
				Message = $"خطا در حذف مشتری: {ex.Message}",
				Data = new ResponseResultGlobally { DoneSuccessfully = false }
			};
		}
	}

	public async Task<GlobalResponse<ResponseResultGlobally>> IsCodeUnique(string code, long? excludeCustomerId = null, CancellationToken cancellationToken = default)
	{
		try
		{
			var isUnique = await _customerRepository.IsCodeUnique(code, excludeCustomerId, cancellationToken);
			return new GlobalResponse<ResponseResultGlobally> { StatusCode = 200, Data = new ResponseResultGlobally()
                { DoneSuccessfully = isUnique }, Message = "عملیات موفق" };
		}
		catch (Exception ex)
		{
			return new GlobalResponse<ResponseResultGlobally> { StatusCode = 500, Message = $"خطا در بررسی کد: {ex.Message}", Data = new ResponseResultGlobally(){DoneSuccessfully = false} };
		}
	}
}