using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using TicTacTec.TA.Library;
using static TicTacTec.TA.Library.Core;

namespace AmengSoft.IBTrader.util
{
    public class TALibWrapper
    {
        public static RetCode EMA(int period, double[] data, out int outBegIdx, out int outNbElement, double[] output )
        {
            //The "lookback" is really your indicator's period minus one because it's expressed as an array index
            //At least that's true for movingAverage(...)
            int lookback = MovingAverageLookback(period, MAType.Ema);
            return MovingAverage(0, data.Length - 1, data, lookback + 1, MAType.Ema, out outBegIdx, out outNbElement, output);
        }

        public static void AddEMA(int period, Color color, double[] close, DateTime[] dtvalues, Chart chart)
        {
            int count = close.Length;
            double[] output = new double[count];
            int outBegIdx = 0, outNbElement = 0;
            EMA(period, close, out outBegIdx, out outNbElement, output);

            double[] indicator = new double[count];
            Array.Copy(output, 0, indicator, count - outNbElement, outNbElement);

            for (int i = 0; i < count - outNbElement; i++)
            {
                indicator[i] = indicator[count - outNbElement];
            }

            var series = chart.Series.Add(string.Format("MA{0}", period));
            series.ChartArea = chart.Series[0].ChartArea;
            series.ChartType = SeriesChartType.Line;
            series.IsXValueIndexed = true;
            series.XValueType = ChartValueType.DateTime;
            series.Color = color;

            for (int i = 0; i < count; i++)
            {
                if (indicator[i] <= 0.001)
                    continue;

                series.Points.AddXY(dtvalues[i], indicator[i]);
            }
        }
    }
}
