import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { Zookeeper } from 'src/models/zookeeper';

@Injectable({
  providedIn: 'root'
})
export class ZookeeperService {
  constructor(private http: HttpClient) {}

  public get(): Observable<Zookeeper[]> {
    return this.http.get<Zookeeper[]>(Config.getURL("Zookeeper"));
  }
}
