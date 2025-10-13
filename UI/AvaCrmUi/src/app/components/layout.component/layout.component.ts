import { Component } from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavSidebarComponent} from '../mainPage/nav-sidebar.component/nav-sidebar.component';
import {NavTopbarComponent} from '../mainPage/nav-topbar.component/nav-topbar.component';

@Component({
  selector: 'app-layout.component',
  imports: [
    RouterOutlet,
    NavSidebarComponent,
    NavTopbarComponent
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {

}
