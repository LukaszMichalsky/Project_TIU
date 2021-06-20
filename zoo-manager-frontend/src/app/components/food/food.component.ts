import { Component, OnInit } from '@angular/core';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { FoodService } from 'src/app/services/food.service';
import { FoodAssociationService } from 'src/app/services/foodassociation.service';
import { AnimalType } from 'src/models/animaltype';
import { FoodAssociation } from 'src/models/foodassociation';
import { FoodViewModel } from 'src/viewmodels/food';

@Component({
  selector: 'app-food',
  templateUrl: './food.component.html',
  styles: [
  ]
})
export class FoodComponent implements OnInit {
  foodAssociations: FoodAssociation[] = [];
  foodItems: FoodViewModel[] = [];
  allAnimalTypes: AnimalType[] = [];
  selectedFoodItemTypes: AnimalType[] | null = null;

  constructor(private animalTypeService: AnimalTypeService, private foodAssociationService: FoodAssociationService, private foodService: FoodService) {}

  private loadData(): void {
    this.foodService.get().subscribe(foodItems => {
      this.foodAssociationService.get().subscribe(foodAssociations => {
        this.animalTypeService.get().subscribe(animalTypes => {
          this.foodAssociations = foodAssociations;
          this.allAnimalTypes = animalTypes;

          this.foodItems = foodItems.map(foodItem => {
            return {
              id: foodItem.id,
              foodName: foodItem.foodName,
              foodBuyPrice: foodItem.foodBuyPrice,
              foodStorageQuantity: foodItem.foodStorageQuantity,

              typesCount: this.foodAssociations.filter(association => {
                return association.foodId === foodItem.id;
              }).length
            };
          });
        });
      });
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  showTypes(foodItemID: number): void {
    let associatedAnimalTypeIDs: number[] = this.foodAssociations.filter(association => {
      return association.foodId === foodItemID;
    }).map(association => {
      return association.animalTypeId;
    });

    this.selectedFoodItemTypes = this.allAnimalTypes.filter(animalType => {
      return associatedAnimalTypeIDs.includes(animalType.id);
    });
  }
}
