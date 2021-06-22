import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { Category } from 'src/models/category';

@Component({
  selector: 'app-category-form',
  templateUrl: './category.component.html',
  styles: [
  ]
})
export class CategoryFormComponent implements OnInit {
  isValid: boolean | null = null;

  @ViewChild("categoryNameInput") categoryNameInput: ElementRef | undefined;
  @Output() eventAddClicked: EventEmitter<Category> = new EventEmitter<Category>();

  constructor() {}

  ngOnInit(): void {
  }

  validate(): void {
    if (this.categoryNameInput?.nativeElement.value.length < 5) {
      this.isValid = false;
    } else {
      this.isValid = true;
    }
  }

  addCategory(): void {
    this.eventAddClicked.emit({
      id: 0,
      categoryName: this.categoryNameInput?.nativeElement.value
    });
  }
}
