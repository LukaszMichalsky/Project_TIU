import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { Food } from 'src/models/food';

@Component({
  selector: 'app-food-form',
  templateUrl: './food.component.html',
  styles: [
  ]
})
export class FoodFormComponent implements OnInit {
  isNameValid: boolean | null = null;
  isPriceValid: boolean | null = null;
  isQuantityValid: boolean | null = null;

  @ViewChild("foodNameInput") foodNameInput: ElementRef | undefined;
  @ViewChild("foodPriceInput") foodPriceInput: ElementRef | undefined;
  @ViewChild("foodQuantityInput") foodQuantityInput: ElementRef | undefined;
  @Output() eventAddClicked: EventEmitter<Food> = new EventEmitter<Food>();

  constructor() {}

  ngOnInit(): void {
  }

  validateName(): void {
    if (this.foodNameInput?.nativeElement.value.length < 3) {
      this.isNameValid = false;
    } else {
      this.isNameValid = true;
    }
  }

  validatePrice(): void {
    let priceString = this.foodPriceInput?.nativeElement.value;
    let parsedPrice = parseFloat(priceString);

    if (isNaN(parsedPrice) === true) {
      this.isPriceValid = false;
    } else {
      this.isPriceValid = parsedPrice >= 0;
    }
  }

  validateQuantity(): void {
    let quantityString = this.foodQuantityInput?.nativeElement.value;
    let parsedQuantity = parseInt(quantityString, 10);

    if (isNaN(parsedQuantity) === true) {
      this.isQuantityValid = false;
    } else {
      this.isQuantityValid = parsedQuantity >= 0;
    }
  }

  addFood(): void {
    this.eventAddClicked.emit({
      id: 0,
      foodBuyPrice: this.foodPriceInput?.nativeElement.value,
      foodName: this.foodNameInput?.nativeElement.value,
      foodStorageQuantity: this.foodQuantityInput?.nativeElement.value
    });
  }
}
