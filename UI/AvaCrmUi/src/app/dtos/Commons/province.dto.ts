export interface ProvinceDto {
  id: number;
  name: string;
  countryId : number;
}

export interface CreateProvinceDto {
  name: string;
  countryId : number;
}

export interface UpdateProvinceDto {
  id: number;
  name: string;
  countryId : number;
}
