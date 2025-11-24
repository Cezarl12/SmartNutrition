import { Component, OnInit, Signal, signal } from '@angular/core';
import { DailyLog } from '../../../shared/models/DailyLog';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NutritionChartService } from '../../../core/services/nutritionChartServic';
import { DailyLogService } from '../../../core/services/dailyLogService';
import { SnackBarService } from '../../../core/services/snackbar';
import { PieChart } from "../../../shared/components/pie-chart/pie-chart";
import { User } from '../../../shared/models/User';
import { Account } from '../../../core/services/account';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-daily-item',
  imports: [PieChart, RouterLink, DatePipe],
  templateUrl: './daily-item.html',
  styleUrl: './daily-item.css',
})
export class DailyItem implements OnInit {


  public dailyLog = signal<DailyLog | null>(null)
  public account: Signal<User | null>;

  public constructor(private dailyLogService: DailyLogService, private router: Router,
    public nutritionChartService: NutritionChartService, private route: ActivatedRoute, private snackBar: SnackBarService, private accountService: Account) {
    this.account = this.accountService.currentUser
  }

  ngOnInit(): void {
    this.getDailyLogByid()
  }

  getDailyLogByid() {
    const idString = this.route.snapshot.paramMap.get('id');
    if (idString) {
      const dailyId = +idString;
      this.dailyLogService.getDailyLogById(dailyId).subscribe({
        next: (result) => {
          this.dailyLog.set(result)
          console.log(result)
        },
        error: () => {
          this.snackBar.error("Nu a fost găsit!")
          this.router.navigateByUrl('/dailylogs');
        }
      })
    }
  }


  public getPieChartDataTotal(): number[] | null {
    const dailylog = this.dailyLog()
    if (dailylog) {
      const p = dailylog.totalProtein;
      const c = dailylog.totalCarbs;
      const f = dailylog.totalFat;
      const data = this.nutritionChartService.calculateMacroSplit(p, c, f);
      return data ? data.caloriesData : null;
    }

    return null
  }

  remove() {
    const id = this.dailyLog()?.id;
    if (id) {
      this.dailyLogService.removeDailyLog(id).subscribe({
        next: () => {
          this.snackBar.success('Eliminat cu succes!')
          this.router.navigateByUrl('/dailylogs')
        },
        error: () => {
          this.snackBar.error('Eorare la eliminiare!')
        }
      })
    }
  }
}
