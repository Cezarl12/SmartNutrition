import { Component, Input } from '@angular/core';
import { ChartOptions, Chart } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import ChartDataLabels, { Context } from 'chartjs-plugin-datalabels';
Chart.register(ChartDataLabels);

@Component({
  selector: 'app-pie-chart',
  standalone: true,
  imports: [BaseChartDirective],
  templateUrl: './pie-chart.html',
})


export class PieChart {

  @Input() data: number[] | null = null;

  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { display: false },
      tooltip: { enabled: false },
      datalabels: {
        display: true,
        color: '#fff',
        font: {
          weight: 'bold',
          size: 30,
        },
        formatter: (value, context: Context) => {
          const dataset = context.chart.data.datasets[0].data as number[];
          if (!dataset) return '';
          const total = dataset.reduce((acc, val) => acc + val, 0);
          if (total === 0) return '';
          const percentage = ((value / total) * 100);
          return percentage > 5 ? percentage.toFixed(0) + '%' : '';
        }
      }
    },

  };

  public pieChartLabels: string[] = ['Proteine', 'Glucide', 'Lipide'];
  public pieChartColors: string[] = ['#a855f7', '#eab308', '#60a5fa'];
}