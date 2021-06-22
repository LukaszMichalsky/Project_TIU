import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { ZookeeperService } from 'src/app/services/zookeeper.service';
import { AnimalType } from 'src/models/animaltype';
import { Zookeeper } from 'src/models/zookeeper';
import { ZookeeperAssociation } from 'src/models/zookeeperassociation';

@Component({
  selector: 'app-zookeeper-association-form',
  templateUrl: './zookeeper-association-form.component.html',
  styles: [
  ]
})
export class ZookeeperAssociationFormComponent implements OnInit {
  animalTypes: AnimalType[] = [];
  zookeepers: Zookeeper[] = [];

  @ViewChild("zookeeperInput") zookeeperInput: ElementRef | undefined;
  @ViewChild("animalTypeInput") animalTypeInput: ElementRef | undefined;
  @Output() eventAddClicked: EventEmitter<ZookeeperAssociation> = new EventEmitter<ZookeeperAssociation>();

  constructor(private animalTypeService: AnimalTypeService, private zookeeperService: ZookeeperService) {}

  private loadData(): void {
    this.animalTypeService.get().subscribe(animalTypes => {
      this.zookeeperService.get().subscribe(zookeepers => {
        this.animalTypes = animalTypes;
        this.zookeepers = zookeepers;
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
      typeZookeeperId: this.zookeeperInput?.nativeElement.value
    });
  }
}
