import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Food } from '../../shared/models/Food';

@Injectable({
  providedIn: 'root'
})
export class FoodService {
  baseUrl = 'https://localhost:7000/api/food/'
  constructor(private http: HttpClient) { }

  getFoodById(id: number) {
    return this.http.get<Food>(this.baseUrl + id)
  }
  getFoodsByIngrdientId(ingredientId: number) {
    return this.http.get<Food[]>(this.baseUrl + 'by-ingredient/' + ingredientId)
  }

}
