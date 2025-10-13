// models/customer-address.models.ts
export interface CustomerAddressListDto {
  id: number;
  customerId: number;
  countryId: number;
  provinceId: number;
  city : string;
  cityId?: number;
  street: string;
  additionalInfo: string;
  countryName: string;
  provinceName: string;
  cityName?: string;
  customerCode: string;
}

export interface CustomerAddressCreateDto {
  customerId: number;
  countryId: number;
  provinceId: number;
  city?: string
  cityId?: number;
  street: string;
  additionalInfo: string;
}

export interface CustomerAddressUpdateDto {
  id: number;
  customerId : number;
  countryId: number;
  provinceId: number;
  cityId?: number;
  city?: string
  street: string;
  additionalInfo: string;
}


export interface CityDto {
  id: number;
  customerId: number;
  name: string;
  provinceId: number;
}
