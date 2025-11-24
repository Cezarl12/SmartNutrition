import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DailyLog, DailyLogDto } from '../../shared/models/DailyLog';

@Injectable({
  providedIn: 'root'
})
export class DailyLogService {
  baseUrl = 'https://localhost:7000/api/dailylog/'
  public constructor(private http: HttpClient) { }

  private httpOptions = {
    withCredentials: true
  };

  saveDailyLog(dailyLogDto: DailyLogDto) {
    return this.http.post<DailyLogDto>(this.baseUrl + 'save', dailyLogDto, this.httpOptions)
  }

  getDailyLogs(pageNumber: number = 1, pageSize: number = 5) {

    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<DailyLog[]>(this.baseUrl, { params: params, withCredentials: true })
  }

  getDailyLogById(id: number) {
    return this.http.get<DailyLog>(this.baseUrl + id, this.httpOptions)
  }

  getDailyLogByRange(startDate: Date, endDate: Date, pageNumber: number = 1, pageSize: number = 10) {

    const startCopy = new Date(startDate);
    startCopy.setMinutes(startCopy.getMinutes() - startCopy.getTimezoneOffset());

    const endCopy = new Date(endDate);
    endCopy.setMinutes(endCopy.getMinutes() - endCopy.getTimezoneOffset());

    let params = new HttpParams()
      .set('startDate', startCopy.toISOString().split('T')[0])
      .set('endDate', endCopy.toISOString().split('T')[0])
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<DailyLog[]>(this.baseUrl + 'range', {
      params: params,
      withCredentials: true
    });
  }

  removeDailyLog(id: number) {
    return this.http.delete(this.baseUrl + id, this.httpOptions)
  }
}
