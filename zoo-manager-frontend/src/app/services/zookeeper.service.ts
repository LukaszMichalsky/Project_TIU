import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { Zookeeper } from 'src/models/zookeeper';

@Injectable({
  providedIn: 'root'
})
export class ZookeeperService {
  private controllerURL: string = "Zookeeper";

  constructor(private http: HttpClient) {}

  public get(): Observable<Zookeeper[]> {
    return this.http.get<Zookeeper[]>(Config.getURL(this.controllerURL));
  }

  public post(newType: Zookeeper): Observable<Zookeeper> {
    return this.http.post<Zookeeper>(Config.getURL(this.controllerURL), newType);
  }

  public delete(id: number): Observable<Zookeeper> {
    return this.http.delete<Zookeeper>(Config.getURL(`${this.controllerURL}/${id}`));
  }
}
