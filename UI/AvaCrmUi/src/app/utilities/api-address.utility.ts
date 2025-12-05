export class ApiAddressUtility {
  static BaseAddress: string = "https://localhost:7283/api";

  static login: string = "/Account/login";
  static register: string = "/Account/register";
  static allCountries: string = "/Countries/GetAllCountries";
  static countryById: string = "/Countries/CountryById";
  static createCountry: string = "/Countries/Create";
  static updateCountry: string = "/Countries/Update";
  static deleteCountry: string = "/Countries/Delete";
  static allProvinces: string = "/Provinces/GetAllProvinces";
  static ProvinceById: string = "/Provinces/ProvinceById";
  static createProvince: string = "/Provinces/Create";
  static updateProvince: string = "/Provinces/Update";
  static deleteProvince: string = "/Provinces/Delete";

  //Customer
  static allCustomers: string = "/Customers";
  static customerById: string = "/Customers";
  static createCustomer: string = "/Customers";
  static updateCustomer: string = "/Customers";
  static deleteCustomer: string = "/Customers";
  static checkCodeUnique: string = "/Customers/check-code-unique";

  // Individual Customer
  static individualCustomerByCustomerId: string = "/IndividualCustomers/by-customer";
  static createIndividualCustomer: string = "/IndividualCustomers";
  static updateIndividualCustomer: string = "/IndividualCustomers";
  static deleteIndividualCustomer: string = "/IndividualCustomers";

  // Organization Customer
  static organizationCustomerByCustomerId: string = "/OrganizationCustomers/by-customer";
  static createOrganizationCustomer: string = "/OrganizationCustomers";
  static updateOrganizationCustomer: string = "/OrganizationCustomers";
  static deleteOrganizationCustomer: string = "/OrganizationCustomers";

  // Customer Address
  static customerAddressById: string = "/CustomerAddresses";
  static customerAddressByCustomerId: string = "/CustomerAddresses/by-customer";
  static createCustomerAddress: string = "/CustomerAddresses";
  static updateCustomerAddress: string = "/CustomerAddresses";
  static deleteCustomerAddress: string = "/CustomerAddresses";

  // Contact Person
  static contactPersonById: string = "/ContactPersons";
  static contactPersonByCustomerId: string = "/ContactPersons/by-customer";
  static createContactPerson: string = "/ContactPersons";
  static updateContactPerson: string = "/ContactPersons";
  static deleteContactPerson: string = "/ContactPersons";

  // Tags
  static tagById: string = "/Tags";
  static allTags: string = "/Tags";
  static createTag: string = "/Tags";
  static updateTag: string = "/Tags";
  static deleteTag: string = "/Tags";

  // Customer Tags
  static customerTagsByCustomer(customerId: number): string {
    return `/CustomerTags/by-customer/${customerId}`;
  }

  static customerTagsByTag(tagId: number): string {
    return `/CustomerTags/by-tag/${tagId}`;
  }

  static addCustomerTag(): string {
    return `/CustomerTags`;
  }

  static removeCustomerTag(): string {
    return `/CustomerTags/remove`;
  }

  // Notes
  static noteById(id: number): string {
    return `/Notes/${id}`;
  }

  static notesByCustomer(customerId: number): string {
    return `/Notes/by-customer/${customerId}`;
  }

  static createNote(): string {
    return `/Notes`;
  }

  static updateNote(): string {
    return `/Notes`;
  }

  static deleteNote(id: number): string {
    return `/Notes/${id}`;
  }

  // Interactions
  static interactionById(id: number): string {
    return `/Interactions/${id}`;
  }

  static interactionsByCustomer(customerId: number): string {
    return `/Interactions/by-customer/${customerId}`;
  }

  static interactionsByType(interactionType: number): string {
    return `/Interactions/by-type/${interactionType}`;
  }

  static createInteraction(): string {
    return `/Interactions`;
  }

  static updateInteraction(): string {
    return `/Interactions`;
  }

  static deleteInteraction(id: number): string {
    return `/Interactions/${id}`;
  }
  // FollowUps
  static followUps: string = "/FollowUps";
  static followUpById: string = "/FollowUps";
  static followUpsByCustomer: string = "/FollowUps/by-customer";
  static upcomingFollowUps: string = "/FollowUps/upcoming";

  // Dashboard
  static dashboardSummary: string = "/Dashboard/summary";
  static upcomingActivities: string = "/Dashboard/upcoming-activities";
  static recentActivities: string = "/Dashboard/recent-activities";

  // Users
  static users: string = "/Users";
  static userById: string = "/Users";
  static userDetail: string = "/Users/detail";
  static userChangePassword: string = "/Users/change-password";
  static userToggleLockout: string = "/Users/toggle-lockout";

  // Roles
  static roles: string = "/Roles";
  static roleById: string = "/Roles";

  // Projects
  static projects: string = "/Projects";
  static projectById(projectId: number): string {
    return `/Projects/GetById/${projectId}`;
  }
  static getAllProjects: string = "/Projects/GetAll";
  static createProject: string = "/Projects/CreateProject";
  static updateProject: string = "/Projects/UpdateProject";
  static changeProjectStatus(projectId: number): string {
    return `/Projects/ChangeStatus/${projectId}`;
  }
  static deleteProject: string = "/Projects/DeleteProject";

  static getAllTasks: string = "/TaskItems/GetAllTasks";
  static getTask(taskItemId: number): string {
    return `/TaskItems/GetTask/${taskItemId}`;
  }
  static createTask: string = "/TaskItems/CreateTask";
  static updateTask: string = "/TaskItems/UpdateTask";
  static deleteTask(taskItemId: number): string {
    return `/TaskItems/DeleteTask/${taskItemId}`;
  }

  // Attachments
  static getAttachments(taskItemId: number): string {
    return `/Attachments/GetAttachments/${taskItemId}`;
  }

  static uploadFile: string = "/Attachments/UploadFile";

  static deleteAttachment(attachmentId: number): string {
    return `/Attachments/DeleteAttachment/${attachmentId}`;
  }

  // Units
  static allUnits: string = "/Units";
  static unitById(id: number): string {
    return `/Units/${id}`;
  }
  static unitsByCategory(category: number): string {
    return `/Units/by-category/${category}`;
  }
  static createUnit: string = "/Units";
  static updateUnit: string = "/Units";
  static deleteUnit(id: number): string {
    return `/Units/${id}`;
  }

  // Currencies
  static allCurrencies: string = "/Currencies";
  static currencyById(id: number): string {
    return `/Currencies/${id}`;
  }
  static defaultCurrency: string = "/Currencies/default";
  static createCurrency: string = "/Currencies";
  static updateCurrency: string = "/Currencies";
  static deleteCurrency(id: number): string {
    return `/Currencies/${id}`;
  }
  static changeDefaultCurrency(currencyId: number): string {
    return `/Currencies/${currencyId}/set-default`;
  }
}
