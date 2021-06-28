import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { Food } from 'src/models/food';

@Injectable({
  providedIn: 'root'
})
export class FoodService {
  private controllerURL: string = "Food";

  constructor(private http: HttpClient) {}

  public get(): Observable<Food[]> {
    return this.http.get<Food[]>(Config.getURL(this.controllerURL), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }

  public post(newType: Food): Observable<Food> {
    return this.http.post<Food>(Config.getURL(this.controllerURL), newType, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }

  public delete(id: number): Observable<Food> {
    return this.http.delete<Food>(Config.getURL(`${this.controllerURL}/${id}`), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }
}
