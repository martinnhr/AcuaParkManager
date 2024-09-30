import { Component } from '@angular/core';
import { MenuItem } from './models/menu-item.model';
import { MenuItemComponent } from "./menu-item/menu-item.component";
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [MenuItemComponent, MatIconModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent {

  isCollapsed = false;


  toggleMenu() {
    this.isCollapsed = !this.isCollapsed;
  }


  MenuList: MenuItem[] = [
    {
      name: 'item1',
      id: 1
    },
    {
      name: 'item2',
      id: 2
    },
    {
      name: 'item3',
      id: 3
    },
    {
      name: 'item4',
      id: 4
    },
    {
      name: 'item5',
      id: 5
    },

  ]

}
