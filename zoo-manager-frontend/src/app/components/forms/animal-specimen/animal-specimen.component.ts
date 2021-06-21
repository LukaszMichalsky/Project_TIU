import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { AnimalSpecimen } from 'src/models/animalspecimen';
import { AnimalType } from 'src/models/animaltype';

@Component({
  selector: 'app-animal-specimen-form',
  templateUrl: './animal-specimen.component.html',
  styles: [
  ]
})
export class AnimalSpecimenFormComponent implements OnInit {
  animalTypes: AnimalType[] = [];
  isValid: boolean | null = null;

  @ViewChild("specimenNameInput") specimenNameInput: ElementRef | undefined;
  @ViewChild("specimenTypeInput") specimenTypeInput: ElementRef | undefined;
  @Output() eventAddClicked: EventEmitter<AnimalSpecimen> = new EventEmitter<AnimalSpecimen>();

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

  addAnimalSpecimen(): void {
    this.eventAddClicked.emit({
      id: 0,
      animalName: this.specimenNameInput?.nativeElement.value,
      typeId: this.specimenTypeInput?.nativeElement.value
    });
  }
}
