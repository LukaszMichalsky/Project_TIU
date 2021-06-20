import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { FoodAssociation } from 'src/models/foodassociation';

@Injectable({
  providedIn: 'root'
})
export class FoodAssociationService {
  constructor(private http: HttpClient) {}

  public get(): Observable<FoodAssociation[]> {
    return this.http.get<FoodAssociation[]>(Config.getURL("FoodAssociation"));
  }
}
