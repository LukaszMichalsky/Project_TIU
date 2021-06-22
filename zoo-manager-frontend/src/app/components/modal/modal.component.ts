import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: "./modal.component.html",
  styles: [
  ]
})
export class ModalComponent implements OnInit {
  @Input() modalID: string = '';
  @Input() modalTitle: string = '';

  constructor() {}

  ngOnInit(): void {
  }
}
