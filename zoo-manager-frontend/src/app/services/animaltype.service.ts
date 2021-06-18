import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Config } from 'src/config';
import { AnimalType } from 'src/models/animaltype';

@Injectable({
  providedIn: 'root'
})
export class AnimaltypeService {
  constructor(private http: HttpClient) {}

  public getAnimalTypes(): Observable<AnimalType[]> {
    return this.http.get<AnimalType[]>(Config.getURL("AnimalType"));
  }
}
