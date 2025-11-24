import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Ingredient } from '../../shared/models/Ingredient';

@Injectable({
  providedIn: 'root'
})
export class IngredintService {
  baseUrl = 'https://localhost:7000/api/ingredient/'
  constructor(private http: HttpClient) { }

  getIngredientById(id: number) {
    return this.http.get<Ingredient>(this.baseUrl + id)
  }
} 
