import { Component, OnInit } from '@angular/core';
import { AnimalSpecimenService } from 'src/app/services/animalspecimen.service';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { CategoryService } from 'src/app/services/category.service';
import { AnimalSpecimen } from 'src/models/animalspecimen';
import { AnimalTypeViewModel } from 'src/viewmodels/animaltype';
import { Category } from 'src/models/category';

@Component({
  selector: 'app-animaltype',
  templateUrl: './animaltype.component.html',
  styles: [
  ]
})
export class AnimalTypeComponent implements OnInit {
  animalTypes: AnimalTypeViewModel[] = [];
  animalSpecimens: AnimalSpecimen[] = [];
  selectedTypeSpecimens: AnimalSpecimen[] | null = null;

  constructor(private animalTypeService: AnimalTypeService, private categoryService: CategoryService, private animalSpecimenService: AnimalSpecimenService) {}

  private loadData(): void {
    this.animalTypeService.get().subscribe(animalTypes => {
      this.categoryService.get().subscribe(categories => {
        this.animalSpecimenService.get().subscribe(specimens => {
          this.animalSpecimens = specimens;

          this.animalTypes = animalTypes.map(animalType => {
            let categoryID = animalType.typeCategoryId;
            let categoryWithID: Category | undefined = categories.find(category => {
              return category.id === categoryID;
            });

            return {
              id: animalType.id,
              typeName: animalType.typeName,
              typeCategoryId: animalType.typeCategoryId,

              categoryName: (categoryWithID == undefined) ? "<brak>" : categoryWithID.categoryName,
              specimensCount: this.animalSpecimens.filter(specimen => {
                return specimen.typeId === animalType.id;
              }).length
            }
          });
        });
      });
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  showSpecimens(animalTypeID: number): void {
    this.selectedTypeSpecimens = this.animalSpecimens.filter(specimen => {
      return specimen.typeId === animalTypeID;
    });

    window.scrollTo(0, 0);
  }
}
