import { Component, OnInit } from '@angular/core';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { ZookeeperService } from 'src/app/services/zookeeper.service';
import { ZookeeperAssociationService } from 'src/app/services/zookeeperassociation.service';
import { AnimalType } from 'src/models/animaltype';
import { ZookeeperAssociation } from 'src/models/zookeeperassociation';
import { ZookeeperViewModel } from 'src/viewmodels/zookeeper';

@Component({
  selector: 'app-zookeeper',
  templateUrl: './zookeeper.component.html',
  styles: [
  ]
})
export class ZookeeperComponent implements OnInit {
  zookeeperAssociations: ZookeeperAssociation[] = [];
  zookeepers: ZookeeperViewModel[] = [];
  allAnimalTypes: AnimalType[] = [];
  selectedZookeeperTypes: AnimalType[] | null = null;

  constructor(private animalTypeService: AnimalTypeService, private zookeeperAssociationService: ZookeeperAssociationService, private zookeeperService: ZookeeperService) {}

  private loadData(): void {
    this.zookeeperService.get().subscribe(zookeepers => {
      this.zookeeperAssociationService.get().subscribe(zookeeperAssociations => {
        this.animalTypeService.get().subscribe(animalTypes => {
          this.zookeeperAssociations = zookeeperAssociations;
          this.allAnimalTypes = animalTypes;

          this.zookeepers = zookeepers.map(zookeeper => {
            return {
              id: zookeeper.id,
              zookeeperName: zookeeper.zookeeperName,
              zookeeperSurname: zookeeper.zookeeperSurname,
              zookeeperPhoneNumber: zookeeper.zookeeperPhoneNumber,

              typesCount: this.zookeeperAssociations.filter(association => {
                return association.typeZookeeperId === zookeeper.id;
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

  showTypes(zookeeperID: number): void {
    let associatedAnimalTypeIDs: number[] = this.zookeeperAssociations.filter(association => {
      return association.typeZookeeperId === zookeeperID;
    }).map(association => {
      return association.animalTypeId;
    });

    this.selectedZookeeperTypes = this.allAnimalTypes.filter(animalType => {
      return associatedAnimalTypeIDs.includes(animalType.id);
    });

    window.scrollTo(0, 0);
  }
}
