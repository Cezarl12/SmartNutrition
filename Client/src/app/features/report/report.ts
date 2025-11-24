import { ChangeDetectorRef, Component, OnInit, signal, ViewChild } from '@angular/core';
import { AverageSummaryDto, DailyDataDto } from '../../shared/models/Report';
import { ReportService } from '../../core/services/reportService';
import { ActivatedRoute } from '@angular/router';
import { Chart, ChartConfiguration, ChartOptions, registerables } from 'chart.js';
import { DatePipe, NgClass } from '@angular/common';
import ChartDataLabels, { Context } from 'chartjs-plugin-datalabels';
import { BaseChartDirective } from 'ng2-charts';
Chart.register(...registerables, ChartDataLabels);

@Component({
  selector: 'app-report',
  imports: [NgClass, BaseChartDirective, DatePipe],
  templateUrl: './report.html',
  styleUrl: './report.css',
})
export class Report implements OnInit {

  public summary = signal<AverageSummaryDto | null>(null);
  public dailyData = signal<DailyDataDto[]>([]);
  public dateStart = signal<Date | null>(null)
  public dateFinish = signal<Date | null>(null)

  @ViewChild(BaseChartDirective) baseChart?: BaseChartDirective;

  public constructor(public reportService: ReportService, public route: ActivatedRoute) { }
  public lineChartPlugins = [ChartDataLabels];
  public lineChartOptions: ChartOptions<'line'> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { display: false },
      datalabels: {
        display: true,
        color: '#06b6d4',
        anchor: 'end',
        align: 'top',
        font: {
          weight: 'bold',
          size: 14
        },
        formatter: (value: number) => {
          return `${Math.round(value)} kcal`;
        }
      }
    },

    scales: {
      x: {
        display: true,
        grid: { display: true },
        ticks: {
          color: '#6b7280',
          font: { size: 12 }
        }
      },
      y: {
        display: true,
        grid: { display: true },
        ticks: {
          color: '#6b7280',
          font: { size: 12 }
        }
      },
    }
  };

  public lineChartData: ChartConfiguration<'line'>['data'] = {
    labels: [],
    datasets: [
      {
        data: [],
        fill: true,
        tension: 0.4,
        borderColor: '#06b6d4',
        backgroundColor: 'rgba(6, 182, 212, 0.2)',
        pointBackgroundColor: '#fff',
        pointBorderColor: '#06b6d4',
        pointHoverBackgroundColor: '#06b6d4',
        pointHoverBorderColor: '#fff',
      },
    ],
  };



  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const startStr = params['startDate'];
      const endStr = params['endDate'];
      const startDate = new Date(startStr);
      this.dateStart.set(startDate)
      const endDate = new Date(endStr);
      this.dateFinish.set(endDate)

      console.log('Generare raport pentru:', startDate, endDate);
      this.loadData(startDate, endDate);
    });
  }

  loadData(start: Date, end: Date) {

    this.reportService.getAverageSummary(start, end).subscribe({
      next: (res) => {
        this.summary.set(res)
        console.log(res)
      },
      error: (err) => console.error('Eroare la sumar:', err)
    });

    this.reportService.getDailyData(start, end).subscribe({
      next: (res) => {
        this.updateChart(res),
          this.dailyData.set(res),
          console.log(res)
      },
      error: (err) => console.error('Eroare la grafic:', err)
    });
  }

  private updateChart(data: DailyDataDto[]) {
    const labels = data.map(d => new Date(d.date).toLocaleDateString('ro-RO', { day: 'numeric', month: 'short' }));
    const values = data.map(d => d.totalKcal);
    this.lineChartData = {
      labels: labels,
      datasets: [
        {
          ...this.lineChartData.datasets[0],
          data: values
        }
      ]
    };
    this.baseChart?.update();
  }


}
