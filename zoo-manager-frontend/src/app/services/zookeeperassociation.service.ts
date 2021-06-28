import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { ZookeeperAssociation } from 'src/models/zookeeperassociation';

@Injectable({
  providedIn: 'root'
})
export class ZookeeperAssociationService {
  private controllerURL: string = "ZookeeperAssociation";

  constructor(private http: HttpClient) {}

  public get(): Observable<ZookeeperAssociation[]> {
    return this.http.get<ZookeeperAssociation[]>(Config.getURL(this.controllerURL), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }

  public post(newAssociation: ZookeeperAssociation): Observable<ZookeeperAssociation> {
    return this.http.post<ZookeeperAssociation>(Config.getURL(this.controllerURL), newAssociation, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }

  public delete(id: number): Observable<ZookeeperAssociation> {
    return this.http.delete<ZookeeperAssociation>(Config.getURL(`${this.controllerURL}/${id}`), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }
}
