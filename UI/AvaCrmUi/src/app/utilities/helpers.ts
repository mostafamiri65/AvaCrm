import {CustomerType} from '../dtos/CustomerManagment/customer.enum';

function convertToCustomerType(value: string): CustomerType {
  switch (value) {
    case '1':
      return CustomerType.Individual;
    case '2':
      return CustomerType.Organization;
    default:
      throw new Error('Invalid customer type value');
  }
}

