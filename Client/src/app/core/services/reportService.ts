import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AverageSummaryDto, DailyDataDto } from '../../shared/models/Report';

@Injectable({
    providedIn: 'root'
})
export class ReportService {
    private baseUrl = 'https://localhost:7000/api/report/';
    constructor(private http: HttpClient) { }

    getAverageSummary(startDate: Date, endDate: Date): Observable<AverageSummaryDto> {
        const params = this.createDateParams(startDate, endDate);

        return this.http.get<AverageSummaryDto>(this.baseUrl + 'summary', { params: params, withCredentials: true });
    }

    getDailyData(startDate: Date, endDate: Date): Observable<DailyDataDto[]> {
        const params = this.createDateParams(startDate, endDate);

        return this.http.get<DailyDataDto[]>(this.baseUrl + 'daily', { params: params, withCredentials: true });
    }



    private createDateParams(start: Date, end: Date): HttpParams {
        const startStr = this.formatDate(start);
        const endStr = this.formatDate(end);

        return new HttpParams()
            .set('startDate', startStr)
            .set('endDate', endStr);
    }

    private formatDate(date: Date): string {
        const copy = new Date(date);
        copy.setMinutes(copy.getMinutes() - copy.getTimezoneOffset());
        return copy.toISOString().split('T')[0];
    }
}