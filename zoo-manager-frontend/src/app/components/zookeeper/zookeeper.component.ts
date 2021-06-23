import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AnimalTypeService } from 'src/app/services/animaltype.service';
import { ZookeeperService } from 'src/app/services/zookeeper.service';
import { ZookeeperAssociationService } from 'src/app/services/zookeeperassociation.service';
import { AnimalType } from 'src/models/animaltype';
import { Zookeeper } from 'src/models/zookeeper';
import { ZookeeperAssociation } from 'src/models/zookeeperassociation';
import { ZookeeperViewModel } from 'src/viewmodels/zookeeper';
import { ZookeeperAssociationFormComponent } from '../forms/zookeeper-association-form/zookeeper-association-form.component';

@Component({
  selector: 'app-zookeeper',
  templateUrl: './zookeeper.component.html',
  styles: [
  ]
})
export class ZookeeperComponent implements OnInit {
  @ViewChild(ZookeeperAssociationFormComponent) zookeeperAssociationForm: ZookeeperAssociationFormComponent | undefined;
  @ViewChild("buttonModalError") buttonModalError: ElementRef | undefined;

  zookeeperAssociations: ZookeeperAssociation[] = [];
  zookeepers: ZookeeperViewModel[] = [];
  allAnimalTypes: AnimalType[] = [];

  errorMessage: string = '';
  selectedZookeeperID: number = 0;
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

  private refresh(): void {
    this.loadData();
    this.selectedZookeeperTypes = null;
    this.zookeeperAssociationForm?.ngOnInit();
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

    this.selectedZookeeperID = zookeeperID;
    window.scrollTo(0, 0);
  }

  deleteZookeeper(id: number): void {
    this.zookeeperService.delete(id).subscribe(() => {
      this.refresh();
    });
  }

  onZookeeperAdded(newZookeeper: Zookeeper): void {
    this.zookeeperService.post(newZookeeper).subscribe(() => {
      this.refresh();
    });
  }

  deleteAssociation(animalTypeID: number): void {
    let associations: ZookeeperAssociation[] = this.zookeeperAssociations.filter(value => {
      return (value.animalTypeId === animalTypeID) && (value.typeZookeeperId === this.selectedZookeeperID);
    });

    if (associations.length === 1) {
      this.zookeeperAssociationService.delete(associations[0].id).subscribe(() => {
        this.refresh();
      });
    }
  }

  onZookeeperAssociated(newAssociation: ZookeeperAssociation) {
    this.zookeeperAssociationService.post(newAssociation).subscribe(() => {
      this.refresh();
    }, error => {
      this.errorMessage = error.error;
      this.buttonModalError?.nativeElement.click();
    });
  }
}
