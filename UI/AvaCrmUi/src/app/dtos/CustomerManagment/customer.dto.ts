import {CustomerType} from './customer.enum';
import {CustomerAddressCreateDto, CustomerAddressListDto} from './customer-address.models';
import {CustomerTagListDto} from './customer-tag.dto';
import {NoteListDto} from './note.models';
import {InteractionListDto} from './interaction.models';
import {ContactPersonListDto} from './contact-person.models';

export interface CustomerListDto {
  id: number;
  code: string;
  customerType: CustomerType;
  email: string;
  phoneNumber: string;
  customerName?: string;
  companyName?: string;
  tags?: string[];
  typeOfCustomer: number;
}

export interface CustomerDetailDto {
  id: number;
  code: string;
  customerType: CustomerType;
  email: string;
  phoneNumber: string;
  individualCustomer?: IndividualCustomerListDto;
  organizationCustomer?: OrganizationCustomerListDto;
  customerAddresses?: CustomerAddressListDto[];
  customerTags?: CustomerTagListDto[];
  followUps?: FollowUpListDto[];
  notes?: NoteListDto[];
  interactions?: InteractionListDto[];
  contactPersons?: ContactPersonListDto[];
}

export interface CustomerCreateDto {
  code: string;
  email: string;
  phoneNumber: string;
  individualCustomer?: IndividualCustomerCreateDto;
  organizationCustomer?: OrganizationCustomerCreateDto;
  customerAddresses?: CustomerAddressCreateDto[];
  tagIds?: number[];
  typeOfCustomer: number;
}

export interface CustomerUpdateDto {
  id: number;
  code: string;
  customerType: CustomerType;
  email: string;
  phoneNumber: string;
}

// Individual Customer Models
export interface IndividualCustomerListDto {
  id: number;
  customerId: number;
  firstName: string;
  lastName: string;
  birthDate?: string;
  customerCode: string;
  email: string;
  phoneNumber: string;
}

export interface IndividualCustomerCreateDto {
  customerId: number;
  firstName: string;
  lastName: string;
  birthDate?: string;
}

export interface IndividualCustomerUpdateDto {
  id: number;
  firstName: string;
  lastName: string;
  birthDate?: string;
}

// Organization Customer Models
export interface OrganizationCustomerListDto {
  id: number;
  customerId: number;
  companyName: string;
  registrationNumber: string;
  industry: string;
  customerCode: string;
  email: string;
  phoneNumber: string;
}

export interface OrganizationCustomerCreateDto {
  customerId: number;
  companyName: string;
  registrationNumber: string;
  industry: string;
}

export interface OrganizationCustomerUpdateDto {
  id: number;
  companyName: string;
  registrationNumber: string;
  industry: string;
}



export interface FollowUpListDto { /* بعداً تکمیل می‌شود */
}

