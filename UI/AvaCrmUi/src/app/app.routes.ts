import {Routes} from '@angular/router';
import {LayoutComponent} from './components/layout.component/layout.component';
import {HomeComponent} from './pages/home.component/home.component';
import {LoginComponent} from './pages/login.component/login.component';
import {AuthGuard} from './guards/auth.guard';
import {CountriesComponent} from './pages/Common/countries.component/countries.component';
import {ProvincesComponent} from './pages/Common/provinces.component/provinces.component';
import {
  CustomerListComponent
} from './pages/customerManagement/customers/customer-list.component/customer-list.component';
import {
  CustomerCreateComponent
} from './pages/customerManagement/customers/customer-create.component/customer-create.component';

import {
  CustomerAddressesComponent
} from './pages/customerManagement/CustomerAddresses/customer-addresses.component/customer-addresses.component';
import {
  CustomerContactPersonsComponent
} from './pages/customerManagement/ContactPerson/customer-contact-persons.component/customer-contact-persons.component';
import {TagListComponent} from './pages/Common/tag-list.component/tag-list.component';
import {
  CustomerTagsComponent
} from './pages/customerManagement/CustomerTag/customer-tags.component/customer-tags.component';
import {
  CustomerNotesComponent
} from './pages/customerManagement/customer-notes/customer-notes.component/customer-notes.component';
import {
  CustomerInteractionsComponent
} from './pages/customerManagement/customer-interactions.component/customer-interactions.component';
import {FollowUpListComponent} from './pages/customerManagement/follow-up-list.component/follow-up-list.component';
import {UserListComponent} from './pages/Common/user-list.component/user-list.component';
import {RoleListComponent} from './pages/Common/role-list.component/role-list.component';
import {ProjectListComponent} from './pages/ProjectManagement/project-list.component/project-list.component';
import {ProjectDetailComponent} from './pages/ProjectManagement/project-detail.component/project-detail.component';
import {UnitsComponent} from './pages/Common/units.component/units.component';
import {CurrenciesComponent} from './pages/Common/currencies.component/currencies.component';

export const routes: Routes = [
  {
    path: '', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      {path: 'home', component: HomeComponent},
      {path: '', redirectTo: '/home', pathMatch: 'full'},
      {path: 'countries', component: CountriesComponent},
      {path: 'provinces/:countryId', component: ProvincesComponent},
      {path: 'customers', component: CustomerListComponent},
      {path: 'customers/create', component: CustomerCreateComponent},
      {path: 'customers/:id/addresses', component: CustomerAddressesComponent},
      {path: 'customers/:id/contact-persons', component: CustomerContactPersonsComponent},
      {path: 'tags', component: TagListComponent},
      {path: 'customers/tags/:id', component: CustomerTagsComponent},
      {path: 'customers/notes/:id', component: CustomerNotesComponent},
      {path: 'customers/interactions/:id', component: CustomerInteractionsComponent},
      { path: 'customers/follow-ups/:id', component: FollowUpListComponent },
      { path: 'users', component: UserListComponent },
      { path: 'roles', component: RoleListComponent },
      // { path: 'customers/edit/:id', component: CustomerEditComponent },
      // Project Management
      {path: 'projects', component: ProjectListComponent},
      {path: 'projects/:id', component: ProjectDetailComponent},
      {
        path: 'settings/units', component: UnitsComponent,   data: { title: 'مدیریت واحدها' }
      },

      // مسیر ارزها
      {
        path: 'settings/currencies', component: CurrenciesComponent,  data: { title: 'مدیریت ارزها' }
      },

    ],

  },
  {path: 'login', component: LoginComponent}
];
