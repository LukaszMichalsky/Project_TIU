import { Component, OnInit } from '@angular/core';
import { AnimalSpecimenService } from 'src/app/services/animalspecimen.service';
import { AnimaltypeService } from 'src/app/services/animaltype.service';
import { CategoryService } from 'src/app/services/category.service';
import { AnimalType } from 'src/models/animaltype';
import { CategoryViewModel } from 'src/viewmodels/category';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styles: [
  ]
})
export class CategoryComponent implements OnInit {
  animalTypes: AnimalType[] = [];
  categories: CategoryViewModel[] = [];
  selectedCategoryTypes: AnimalType[] | null = null;

  constructor(private animalTypeService: AnimaltypeService, private animalSpecimenService: AnimalSpecimenService, private categoryService: CategoryService) {}

  private loadData(): void {
    this.categoryService.get().subscribe(categories => {
      this.animalTypeService.get().subscribe(animalTypes => {
        this.animalSpecimenService.get().subscribe(animalSpecimens => {
          this.animalTypes = animalTypes;

          this.categories = categories.map(category => {
            return {
              id: category.id,
              categoryName: category.categoryName,

              typesCount: this.animalTypes.filter(animalType => {
                return animalType.typeCategoryId === category.id
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

  showTypes(categoryID: number): void {
    this.selectedCategoryTypes = this.animalTypes.filter(type => {
      return type.typeCategoryId === categoryID;
    });

    window.scrollTo(0, 0);
  }
}
