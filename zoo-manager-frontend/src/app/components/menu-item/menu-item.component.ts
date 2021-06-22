import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styles: [
  ]
})
export class MenuItemComponent implements OnInit {
  @Input() cardText: string = '';
  @Input() cardTitle: string = '';
  @Input() classType: string = '';
  @Input() imageSource: string = '';
  @Input() routerLink: string = '';

  constructor() {}

  ngOnInit(): void {
  }
}
