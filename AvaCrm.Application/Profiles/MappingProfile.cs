using AvaCrm.Application.DTOs.Commons.Cities;
using AvaCrm.Application.DTOs.Commons.Currencies;
using AvaCrm.Application.DTOs.Commons.Provinces;
using AvaCrm.Application.DTOs.Commons.Units;
using AvaCrm.Application.DTOs.CustomerManagement.ContactPersons;
using AvaCrm.Application.DTOs.CustomerManagement.CustomerAddresses;
using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.DTOs.CustomerManagement.FollowUps;
using AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers;
using AvaCrm.Application.DTOs.CustomerManagement.Interactions;
using AvaCrm.Application.DTOs.CustomerManagement.Notes;
using AvaCrm.Application.DTOs.CustomerManagement.OrganizationCustomers;
using AvaCrm.Application.DTOs.CustomerManagement.Tags;
using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Domain.Entities.CustomerManagement;
using AvaCrm.Domain.Entities.ProjectManagement;
using AvaCrm.Domain.Enums.CustomerManagement;
using Unit = AvaCrm.Domain.Entities.Commons.Unit;

namespace AvaCrm.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Commons

        #region Country
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Country, CreateCountryDto>().ReverseMap();
        #endregion

        #region Province
        CreateMap<Province, ProvinceDto>().ReverseMap();
        CreateMap<Province, CreateProvinceDto>().ReverseMap();
        #endregion

        #region City
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<City, CreateCityDto>().ReverseMap();
        #endregion

        #region Unit
        CreateMap<Unit, UnitDto>().ReverseMap();
        CreateMap<Unit,CreateUnitDto>().ReverseMap();
        CreateMap<Unit,UpdateUnitDto>().ReverseMap();
        #endregion

        #region Currencies
        CreateMap<Currency, CurrencyDto>().ReverseMap();
        CreateMap<Currency, CreateCurrencyDto>().ReverseMap();
        CreateMap<Currency, UpdateCurrencyDto>().ReverseMap();
        #endregion
        #endregion


        #region Customer Management

        #region Customer
        CreateMap<Customer, CustomerListDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
                src.CustomerType == CustomerType.Individual && src.IndividualCustomer != null
                    ? $"{src.IndividualCustomer.FirstName} {src.IndividualCustomer.LastName}"
                    : src.OrganizationCustomer != null
                        ? src.OrganizationCustomer.CompanyName
                        : string.Empty))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src =>
                src.CustomerType == CustomerType.Organization && src.OrganizationCustomer != null
                    ? src.OrganizationCustomer.CompanyName
                    : string.Empty))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                src.CustomerTags != null
                    ? src.CustomerTags.Select(ct => ct.Tag.Title!).ToList()
                    : new List<string>()))
            .ForMember(dest => dest.TypeOfCustomer, opt => opt.MapFrom(src => (int)src.CustomerType));

        CreateMap<Customer, CustomerDetailDto>();
        CreateMap<CustomerCreateDto, Customer>();
        CreateMap<CustomerUpdateDto, Customer>();
        #endregion

        #region IndividualCustomer
        CreateMap<IndividualCustomer, IndividualCustomerListDto>()
            .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer.Code))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber));

        CreateMap<IndividualCustomerCreateDto, IndividualCustomer>();
        CreateMap<IndividualCustomerUpdateDto, IndividualCustomer>();
        #endregion

        #region OrganizationCustomer
        CreateMap<OrganizationCustomer, OrganizationCustomerListDto>()
            .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer.Code))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber));

        CreateMap<OrganizationCustomerCreateDto, OrganizationCustomer>();
        CreateMap<OrganizationCustomerUpdateDto, OrganizationCustomer>();
        #endregion

        #region ContactPerson
        CreateMap<ContactPerson, ContactPersonListDto>()
            .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer.Code));

        CreateMap<ContactPersonCreateDto, ContactPerson>();
        CreateMap<ContactPersonUpdateDto, ContactPerson>();
        #endregion

        #region CustomerAddress
        CreateMap<CustomerAddress, CustomerAddressListDto>()
            .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer.Code))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province.Name))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City != null ? src.City.Name : string.Empty));

        CreateMap<CustomerAddressCreateDto, CustomerAddress>().ReverseMap();
        CreateMap<CustomerAddressUpdateDto, CustomerAddress>().ReverseMap();
        #endregion

        #region FollowUp
        CreateMap<FollowUp, FollowUpListDto>()
            .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer.Code))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreationDate));

        CreateMap<FollowUpCreateDto, FollowUp>();
        CreateMap<FollowUpUpdateDto, FollowUp>();
        #endregion

        #region Interaction
        CreateMap<Interaction, InteractionListDto>()
            .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer.Code))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.InteractionTypeName, opt => opt.MapFrom(src => src.InteractionType.ToString()));

        CreateMap<InteractionCreateDto, Interaction>();
        CreateMap<InteractionUpdateDto, Interaction>();
        #endregion

        #region Note
        CreateMap<Note, NoteListDto>()
            .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer.Code))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreationDate));

        CreateMap<NoteCreateDto, Note>();
        CreateMap<NoteUpdateDto, Note>();
        #endregion

        #region Tag
        CreateMap<Tag, TagDto>()
            .ForMember(dest => dest.CustomerCount, opt => opt.MapFrom(src => src.CustomerTags != null ?
            src.CustomerTags.Count : 0));

        CreateMap<TagDto, Tag>();
        #endregion

        #region CustomerTag
        CreateMap<CustomerTag, CustomerTagListDto>()
            .ForMember(dest => dest.TagTitle, opt => opt.MapFrom(src => src.Tag.Title))
            .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer.Code));

        CreateMap<CustomerTagCreateDto, CustomerTag>();
        #endregion



        #endregion

        #region Project Management
        #region Projects

        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Project, CreateProjectDto>().ReverseMap();

        #endregion
        #region TaskItems
        CreateMap<TaskItem, TaskItemDto>().ReverseMap();
        CreateMap<TaskItem, CreateTaskItemDto>().ReverseMap();
        #endregion
        #region Attachment
        CreateMap<Attachment, AttachmentDto>().ReverseMap();
        #endregion
        #region Comments
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CreateCommentDto>().ReverseMap();
        CreateMap<UpdateCommentDto, Comment>().ReverseMap();
        #endregion
        #endregion
    }
}
