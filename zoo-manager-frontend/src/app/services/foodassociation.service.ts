import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { FoodAssociation } from 'src/models/foodassociation';

@Injectable({
  providedIn: 'root'
})
export class FoodAssociationService {
  private controllerURL: string = "FoodAssociation";

  constructor(private http: HttpClient) {}

  public get(): Observable<FoodAssociation[]> {
    return this.http.get<FoodAssociation[]>(Config.getURL(this.controllerURL), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }

  public post(newAssociation: FoodAssociation): Observable<FoodAssociation> {
    return this.http.post<FoodAssociation>(Config.getURL(this.controllerURL), newAssociation, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }

  public delete(id: number): Observable<FoodAssociation> {
    return this.http.delete<FoodAssociation>(Config.getURL(`${this.controllerURL}/${id}`), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }
}
