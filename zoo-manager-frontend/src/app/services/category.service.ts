import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { Category } from 'src/models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private controllerURL: string = "Category";

  constructor(private http: HttpClient) {}

  public get(): Observable<Category[]> {
    return this.http.get<Category[]>(Config.getURL(this.controllerURL), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }

  public post(newType: Category): Observable<Category> {
    return this.http.post<Category>(Config.getURL(this.controllerURL), newType, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }

  public delete(id: number): Observable<Category> {
    return this.http.delete<Category>(Config.getURL(`${this.controllerURL}/${id}`), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }
}
