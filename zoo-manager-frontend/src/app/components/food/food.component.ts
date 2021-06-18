import { Component, OnInit } from '@angular/core';
import { FoodService } from 'src/app/services/food.service';
import { Food } from 'src/models/food';

@Component({
  selector: 'app-food',
  templateUrl: './food.component.html',
  styles: [
  ]
})
export class FoodComponent implements OnInit {
  foods: Food[] = [];

  constructor(private foodService: FoodService) {}

  ngOnInit(): void {
    this.foodService.get().subscribe((foods) => {
      this.foods = foods;
    });
  }
}
