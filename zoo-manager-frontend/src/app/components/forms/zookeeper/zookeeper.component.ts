import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { Zookeeper } from 'src/models/zookeeper';

@Component({
  selector: 'app-zookeeper-form',
  templateUrl: './zookeeper.component.html',
  styles: [
  ]
})
export class ZookeeperFormComponent implements OnInit {
  isNameValid: boolean | null = null;
  isSurnameValid: boolean | null = null;

  @ViewChild("zookeeperNameInput") zookeeperNameInput: ElementRef | undefined;
  @ViewChild("zookeeperSurnameInput") zookeeperSurnameInput: ElementRef | undefined;
  @ViewChild("zookeeperPhoneInput") zookeeperPhoneInput: ElementRef | undefined;
  @Output() eventAddClicked: EventEmitter<Zookeeper> = new EventEmitter<Zookeeper>();

  constructor() {}

  ngOnInit(): void {
  }

  validateName(): void {
    if (this.zookeeperNameInput?.nativeElement.value.length < 3) {
      this.isNameValid = false;
    } else {
      this.isNameValid = true;
    }
  }

  validateSurname(): void {
    if (this.zookeeperSurnameInput?.nativeElement.value.length < 3) {
      this.isSurnameValid = false;
    } else {
      this.isSurnameValid = true;
    }
  }

  addZookeeper(): void {
    this.eventAddClicked.emit({
      id: 0,
      zookeeperName: this.zookeeperNameInput?.nativeElement.value,
      zookeeperSurname: this.zookeeperSurnameInput?.nativeElement.value,
      zookeeperPhoneNumber: this.zookeeperPhoneInput?.nativeElement.value
    });
  }
}
