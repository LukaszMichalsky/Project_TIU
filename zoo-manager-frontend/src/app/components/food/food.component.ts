import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { FoodService } from 'src/app/services/food.service';
import { FoodAssociationService } from 'src/app/services/foodassociation.service';
import { AnimalType } from 'src/models/animaltype';
import { Food } from 'src/models/food';
import { FoodAssociation } from 'src/models/foodassociation';
import { FoodViewModel } from 'src/viewmodels/food';
import { FoodAssociationFormComponent } from '../forms/food-association-form/food-association-form.component';

@Component({
  selector: 'app-food',
  templateUrl: './food.component.html',
  styles: [
  ]
})
export class FoodComponent implements OnInit {
  @ViewChild(FoodAssociationFormComponent) foodAssociationForm: FoodAssociationFormComponent | undefined;
  @ViewChild("buttonModalError") buttonModalError: ElementRef | undefined;

  foodAssociations: FoodAssociation[] = [];
  foodItems: FoodViewModel[] = [];
  allAnimalTypes: AnimalType[] = [];

  errorMessage: string = '';
  selectedFoodItemID: number = 0;
  selectedFoodItemTypes: AnimalType[] | null = null;

  constructor(private animalTypeService: AnimalTypeService, private foodAssociationService: FoodAssociationService, private foodService: FoodService) {}

  private loadData(onDataLoaded?: Function): void {
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

          // Invoke additional callback (if provided).
          onDataLoaded?.();
        });
      });
    });
  }

  private refresh(): void {
    this.loadData(() => {
      this.selectedFoodItemTypes = null;
      this.foodAssociationForm?.ngOnInit();
      this.showTypes(this.selectedFoodItemID);
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

    this.selectedFoodItemID = foodItemID;
    window.scrollTo(0, 0);
  }

  deleteFood(id: number): void {
    this.foodService.delete(id).subscribe(() => {
      this.refresh();
    });
  }

  deleteAssociation(animalTypeID: number): void {
    let associations: FoodAssociation[] = this.foodAssociations.filter(value => {
      return (value.animalTypeId === animalTypeID) && (value.foodId === this.selectedFoodItemID);
    });

    if (associations.length === 1) {
      this.foodAssociationService.delete(associations[0].id).subscribe(() => {
        this.refresh();
      });
    }
  }

  onFoodAdded(newFood: Food): void {
    this.foodService.post(newFood).subscribe(() => {
      this.refresh();
    });
  }

  onFoodAssociated(newAssociation: FoodAssociation) {
    this.foodAssociationService.post(newAssociation).subscribe(() => {
      this.refresh();
    }, error => {
      this.errorMessage = error.error;
      this.buttonModalError?.nativeElement.click();
    });
  }
}
