import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { ZookeeperAssociation } from 'src/models/zookeeperassociation';

@Injectable({
  providedIn: 'root'
})
export class ZookeeperAssociationService {
  constructor(private http: HttpClient) {}

  public get(): Observable<ZookeeperAssociation[]> {
    return this.http.get<ZookeeperAssociation[]>(Config.getURL("ZookeeperAssociation"));
  }
}
