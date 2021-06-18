import { Component, OnInit } from '@angular/core';
import { AnimaltypeService } from 'src/app/services/animaltype.service';
import { AnimalType } from 'src/models/animaltype';

@Component({
  selector: 'app-animaltype',
  templateUrl: './animaltype.component.html',
  styles: [
  ]
})
export class AnimaltypeComponent implements OnInit {
  animalTypes: AnimalType[] = [];

  constructor(private animalTypeService: AnimaltypeService) {}

  ngOnInit(): void {
    this.animalTypeService.get().subscribe((animalTypes) => {
      this.animalTypes = animalTypes;
    });
  }
}
