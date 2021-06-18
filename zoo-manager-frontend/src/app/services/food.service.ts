import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { Food } from 'src/models/food';

@Injectable({
  providedIn: 'root'
})
export class FoodService {
  constructor(private http: HttpClient) {}

  public get(): Observable<Food[]> {
    return this.http.get<Food[]>(Config.getURL("Food"));
  }
}
