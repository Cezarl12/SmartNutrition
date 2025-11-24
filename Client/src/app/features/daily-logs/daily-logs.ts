import { ChangeDetectorRef, Component, OnInit, signal } from '@angular/core';
import { DailyLogService } from '../../core/services/dailyLogService';
import { DailyLog } from '../../shared/models/DailyLog';
import { CommonModule, DatePipe } from '@angular/common';
import { NutritionChartService } from '../../core/services/nutritionChartServic';
import { PieChart } from "../../shared/components/pie-chart/pie-chart";
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';
import { SnackBarService } from '../../core/services/snackbar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Observable } from 'rxjs';
import { ReportService } from '../../core/services/reportService';

@Component({
  selector: 'app-daily-logs',
  imports: [CommonModule, ReactiveFormsModule, DatePipe, PieChart, RouterLink, MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatButtonModule],
  templateUrl: './daily-logs.html',
  styleUrl: './daily-logs.css',


})
export class DailyLogs implements OnInit {

  currentPage = 1;
  pageSize = 5;
  firstDate = signal<Date | null>(null)
  lastDate = signal<Date | null>(null)

  public range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });


  public constructor(private dailyLogService: DailyLogService, private cdr: ChangeDetectorRef, public nutritionChartService: NutritionChartService,
    public snackBar: SnackBarService, public reportService: ReportService, public router: Router) { }
  public dailyLogs = signal<DailyLog[] | null>(null)

  ngOnInit(): void {
    this.getDailyLogs();
  }

  getDailyLogs() {
    const { start, end } = this.range.value;

    let logsObservable: Observable<DailyLog[]>;

    if (start && end) {
      logsObservable = this.dailyLogService.getDailyLogByRange(
        start,
        end,
        this.currentPage,
        this.pageSize
      );
    } else {
      logsObservable = this.dailyLogService.getDailyLogs(
        this.currentPage,
        this.pageSize
      );
    }

    logsObservable.subscribe({
      next: (result) => {
        this.dailyLogs.set(result);

        if (result && result.length > 0) {

          const newestDate = new Date(result[0].date);
          const oldestDate = new Date(result[result.length - 1].date);

          this.firstDate.set(newestDate);
          this.lastDate.set(oldestDate);

        } else {
          this.firstDate.set(null);
          this.lastDate.set(null);
        }

        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error(err);
        this.dailyLogs.set([]);
        this.cdr.detectChanges();
      }
    });
  }

  public getPieChartDataTotal(dailylog: DailyLog): number[] | null {
    const p = dailylog.totalProtein;
    const c = dailylog.totalCarbs;
    const f = dailylog.totalFat;

    const data = this.nutritionChartService.calculateMacroSplit(p, c, f);
    return data ? data.caloriesData : null;
  }

  applyDateFilter(): void {
    const { start, end } = this.range.value;
    if (start && end) {
      this.firstDate.set(start);
      this.lastDate.set(end);
      this.currentPage = 1;
      this.getDailyLogs();
    }
  }

  clearDateFilter(): void {
    this.range.reset();
    this.firstDate.set(null);
    this.lastDate.set(null);
    this.currentPage = 1;
    this.getDailyLogs();
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getDailyLogs();
    }
  }
  nextPage() {
    this.currentPage++;
    this.getDailyLogs();
  }

  remove(id: number) {
    this.dailyLogService.removeDailyLog(id).subscribe({
      next: () => {
        this.snackBar.success('Eliminat cu succes!')
        this.getDailyLogs()
      },
      error: () => {
        this.snackBar.error('Eorare la eliminiare!')
      }
    })
  }


  generateReport() {
    const { start, end } = this.range.value;

    if (start && end) {
      const s = new Date(start);
      const e = new Date(end);

      s.setMinutes(s.getMinutes() - s.getTimezoneOffset());
      e.setMinutes(e.getMinutes() - e.getTimezoneOffset());

      this.router.navigate(['/report'], {
        queryParams: {
          startDate: s.toISOString().split('T')[0],
          endDate: e.toISOString().split('T')[0]
        }
      });
    } else {
      this.snackBar.error('Te rugăm să selectezi un interval de date pentru raport.');
    }
  }



}
