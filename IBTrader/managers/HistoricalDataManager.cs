/* Copyright (C) 2013 Interactive Brokers LLC. All rights reserved.  This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using IBApi;
using System.Globalization;
using System.Windows.Forms;
using AmengSoft.IBTrader.util;
using System.Drawing;

namespace AmengSoft.IBTrader
{
    public class HistoricalDataManager : DataManager
    {
        public const int HISTORICAL_ID_BASE = 30000000;

        private string fullDatePattern = "yyyyMMdd  HH:mm:ss";
        private string yearMonthDayPattern = "yyyyMMdd";

        protected int barCounter = -1;
        
        private List<HistoricalDataMessage> historicalData;

        public HistoricalDataManager(IBClient ibClient, Chart chart) : base(ibClient, chart) 
        {
            Chart historicalChart = (Chart)uiControl;
            historicalChart.Series[0]["PriceUpColor"] = "Green";
            historicalChart.Series[0]["PriceDownColor"] = "Red";
        }

        public void AddRequest(Contract contract, string endDateTime, string durationString, string barSizeSetting, string whatToShow, int useRTH, int dateFormat)
        {
            Clear();
            ibClient.ClientSocket.reqHistoricalData(currentTicker + HISTORICAL_ID_BASE, contract, endDateTime, durationString, barSizeSetting, whatToShow, useRTH, 1, new List<TagValue>());
        }

        public override void Clear()
        {
            barCounter = -1;
            Chart historicalChart = (Chart)uiControl;
            historicalChart.Series[0].Points.Clear();
            historicalData = new List<HistoricalDataMessage>();
        }

        public override void NotifyError(int requestId)
        {
        }

        public override void UpdateUI(IBMessage message)
        {
            switch (message.Type)
            {
                case MessageType.HistoricalData:
                    historicalData.Add((HistoricalDataMessage)message);
                    break;
                case MessageType.HistoricalDataEnd:
                    PaintChart();
                    break;
            }
        }

        private void PaintChart()
        {
            DateTime dt;
            Chart historicalChart = (Chart)uiControl;

            double[] close = new double[historicalData.Count];
            DateTime[] timevalues = new DateTime[historicalData.Count];

            for (int i = 0; i < historicalData.Count; i++)
            {
                if (historicalData[i].Date.Length == fullDatePattern.Length)
                    DateTime.TryParseExact(historicalData[i].Date, fullDatePattern, null, DateTimeStyles.None, out dt);
                else if (historicalData[i].Date.Length == yearMonthDayPattern.Length)
                    DateTime.TryParseExact(historicalData[i].Date, yearMonthDayPattern, null, DateTimeStyles.None, out dt);
                else
                    continue;

                // adding date and high
                historicalChart.Series[0].Points.AddXY(dt, historicalData[i].High);
                // adding low
                historicalChart.Series[0].Points[i].YValues[1] = historicalData[i].Low;
                //adding open
                historicalChart.Series[0].Points[i].YValues[2] = historicalData[i].Open;
                // adding close
                historicalChart.Series[0].Points[i].YValues[3] = historicalData[i].Close;

                timevalues[i] = dt;
                close[i] = historicalData[i].Close;
            }
            TALibWrapper.AddEMA(10, Color.Red, close, timevalues, historicalChart);
            TALibWrapper.AddEMA(20, Color.Blue, close, timevalues, historicalChart);
            TALibWrapper.AddEMA(50, Color.Brown, close, timevalues, historicalChart);
            TALibWrapper.AddEMA(100, Color.Green, close, timevalues, historicalChart);
            TALibWrapper.AddEMA(200, Color.DarkOrange, close, timevalues, historicalChart);
        }

        protected void PopulateGrid(IBMessage message)
        {
            HistoricalDataMessage bar = (HistoricalDataMessage)message;
        }
    }
}
