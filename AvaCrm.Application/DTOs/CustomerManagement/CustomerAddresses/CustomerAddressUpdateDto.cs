namespace AvaCrm.Application.DTOs.CustomerManagement.CustomerAddresses
{
    public class CustomerAddressUpdateDto
    {
		public long Id { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int? CityId { get; set; }
        public string? City { get; set; }
        public string Street { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
	}
}