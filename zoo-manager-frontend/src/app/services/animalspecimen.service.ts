import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { AnimalSpecimen } from 'src/models/animalspecimen';

@Injectable({
  providedIn: 'root'
})
export class AnimalSpecimenService {
  private controllerURL: string = "AnimalSpecimen";

  constructor(private http: HttpClient) {}

  public get(): Observable<AnimalSpecimen[]> {
    return this.http.get<AnimalSpecimen[]>(Config.getURL(this.controllerURL));
  }

  public post(newSpecimen: AnimalSpecimen): Observable<AnimalSpecimen> {
    return this.http.post<AnimalSpecimen>(Config.getURL(this.controllerURL), newSpecimen);
  }

  public delete(id: number): Observable<AnimalSpecimen> {
    return this.http.delete<AnimalSpecimen>(Config.getURL(`${this.controllerURL}/${id}`));
  }
}
