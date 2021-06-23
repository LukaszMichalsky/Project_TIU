import { Component, OnInit, ViewChild } from '@angular/core';
import { AnimalSpecimenService } from 'src/app/services/animalspecimen.service';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { CategoryService } from 'src/app/services/category.service';
import { AnimalSpecimen } from 'src/models/animalspecimen';
import { AnimalTypeViewModel } from 'src/viewmodels/animaltype';
import { Category } from 'src/models/category';
import { AnimalType } from 'src/models/animaltype';
import { AnimalSpecimenFormComponent } from '../forms/animal-specimen/animal-specimen.component';

@Component({
  selector: 'app-animaltype',
  templateUrl: './animaltype.component.html',
  styles: [
  ]
})
export class AnimalTypeComponent implements OnInit {
  @ViewChild(AnimalSpecimenFormComponent) animalSpecimenForm: AnimalSpecimenFormComponent | undefined;

  animalTypes: AnimalTypeViewModel[] = [];
  animalSpecimens: AnimalSpecimen[] = [];

  selectedTypeID: number = 0;
  selectedTypeSpecimens: AnimalSpecimen[] | null = null;

  constructor(private animalTypeService: AnimalTypeService, private categoryService: CategoryService, private animalSpecimenService: AnimalSpecimenService) {}

  private loadData(onDataLoaded?: Function): void {
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

          // Invoke additional callback (if provided).
          onDataLoaded?.();
        });
      });
    });
  }

  private refresh(): void {
    this.loadData(() => {
      this.selectedTypeSpecimens = null;
      this.animalSpecimenForm?.ngOnInit();
      this.showSpecimens(this.selectedTypeID);
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  showSpecimens(animalTypeID: number): void {
    this.selectedTypeSpecimens = this.animalSpecimens.filter(specimen => {
      return specimen.typeId === animalTypeID;
    });

    this.selectedTypeID = animalTypeID;
    window.scrollTo(0, 0);
  }

  deleteSpecimen(id: number): void {
    this.animalSpecimenService.delete(id).subscribe(() => {
      this.refresh();
    });
  }

  onAnimalSpecimenAdded(newSpecimen: AnimalSpecimen): void {
    this.animalSpecimenService.post(newSpecimen).subscribe(() => {
      this.refresh();
    });
  }

  deleteType(id: number): void {
    this.animalTypeService.delete(id).subscribe(() => {
      this.refresh();
    });
  }

  onAnimalTypeAdded(newType: AnimalType): void {
    this.animalTypeService.post(newType).subscribe(() => {
      this.refresh();
    });
  }
}
