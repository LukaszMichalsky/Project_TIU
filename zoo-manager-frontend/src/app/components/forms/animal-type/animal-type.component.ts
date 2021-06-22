import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { AnimalType } from 'src/models/animaltype';
import { Category } from 'src/models/category';

@Component({
  selector: 'app-animal-type-form',
  templateUrl: 'animal-type.component.html',
  styles: [
  ]
})
export class AnimalTypeFormComponent implements OnInit {
  categories: Category[] = [];
  isValid: boolean | null = null;

  @ViewChild("typeCategoryInput") typeCategoryInput: ElementRef | undefined;
  @ViewChild("typeNameInput") typeNameInput: ElementRef | undefined;
  @Output() eventAddClicked: EventEmitter<AnimalType> = new EventEmitter<AnimalType>();

  constructor(private categoryService: CategoryService) {}

  private loadData(): void {
    this.categoryService.get().subscribe(categories => {
      this.categories = categories;
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  validate(): void {
    if (this.typeNameInput?.nativeElement.value.length < 5) {
      this.isValid = false;
    } else {
      this.isValid = true;
    }
  }

  addAnimalType(): void {
    this.eventAddClicked.emit({
      id: 0,
      typeName: this.typeNameInput?.nativeElement.value,
      typeCategoryId: this.typeCategoryInput?.nativeElement.value
    });
  }
}
