import { Component, OnInit } from '@angular/core';
import { ZookeeperService } from 'src/app/services/zookeeper.service';
import { Zookeeper } from 'src/models/zookeeper';

@Component({
  selector: 'app-zookeeper',
  templateUrl: './zookeeper.component.html',
  styles: [
  ]
})
export class ZookeeperComponent implements OnInit {
  zookeepers: Zookeeper[] = [];

  constructor(private zookeeperService: ZookeeperService) {}

  ngOnInit(): void {
    this.zookeeperService.get().subscribe((zookeepers) => {
      this.zookeepers = zookeepers;
    });
  }
}
