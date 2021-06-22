import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { AnimalType } from 'src/models/animaltype';

@Injectable({
  providedIn: 'root'
})
export class AnimalTypeService {
  private controllerURL: string = "AnimalType"

  constructor(private http: HttpClient) {}

  public get(): Observable<AnimalType[]> {
    return this.http.get<AnimalType[]>(Config.getURL(this.controllerURL));
  }

  public post(newType: AnimalType): Observable<AnimalType> {
    return this.http.post<AnimalType>(Config.getURL(this.controllerURL), newType);
  }

  public delete(id: number): Observable<AnimalType> {
    return this.http.delete<AnimalType>(Config.getURL(`${this.controllerURL}/${id}`));
  }
}
