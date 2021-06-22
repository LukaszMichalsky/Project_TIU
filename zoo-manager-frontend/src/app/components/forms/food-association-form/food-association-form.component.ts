import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { FoodService } from 'src/app/services/food.service';
import { AnimalType } from 'src/models/animaltype';
import { Food } from 'src/models/food';
import { FoodAssociation } from 'src/models/foodassociation';

@Component({
  selector: 'app-food-association-form',
  templateUrl: './food-association-form.component.html',
  styles: [
  ]
})
export class FoodAssociationFormComponent implements OnInit {
  animalTypes: AnimalType[] = [];
  foodItems: Food[] = [];

  @ViewChild("foodItemInput") foodItemInput: ElementRef | undefined;
  @ViewChild("animalTypeInput") animalTypeInput: ElementRef | undefined;
  @Output() eventAddClicked: EventEmitter<FoodAssociation> = new EventEmitter<FoodAssociation>();

  constructor(private animalTypeService: AnimalTypeService, private foodService: FoodService) {}

  private loadData(): void {
    this.animalTypeService.get().subscribe(animalTypes => {
      this.foodService.get().subscribe(foodItems => {
        this.animalTypes = animalTypes;
        this.foodItems = foodItems;
      });
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  addAssociation(): void {
    this.eventAddClicked.emit({
      id: 0,
      animalTypeId: this.animalTypeInput?.nativeElement.value,
      foodId: this.foodItemInput?.nativeElement.value
    });
  }
}
