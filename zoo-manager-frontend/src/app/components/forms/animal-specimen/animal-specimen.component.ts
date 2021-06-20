import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { AnimalType } from 'src/models/animaltype';

@Component({
  selector: 'app-animal-specimen-form',
  templateUrl: './animal-specimen.component.html',
  styles: [
  ]
})
export class AnimalSpecimenFormComponent implements OnInit {
  @ViewChild("specimenName") specimenNameInput: ElementRef | undefined;
  animalTypes: AnimalType[] = [];
  isValid: boolean | null = null;

  constructor(private animalTypeService: AnimalTypeService) {}

  private loadData(): void {
    this.animalTypeService.get().subscribe(animalTypes => {
      this.animalTypes = animalTypes;
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  validate(): void {
    if (this.specimenNameInput?.nativeElement.value.length < 5) {
      this.isValid = false;
    } else {
      this.isValid = true;
    }
  }
}
