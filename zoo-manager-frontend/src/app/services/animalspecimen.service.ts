import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { AnimalSpecimen } from 'src/models/animalspecimen';

@Injectable({
  providedIn: 'root'
})
export class AnimalSpecimenService {
  constructor(private http: HttpClient) {}

  public get(): Observable<AnimalSpecimen[]> {
    return this.http.get<AnimalSpecimen[]>(Config.getURL("AnimalSpecimen"));
  }
}
