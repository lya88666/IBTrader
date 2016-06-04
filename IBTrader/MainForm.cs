using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBApi;

namespace AmengSoft.IBTrader
{
    public partial class MainForm : Form
    {
        private bool isConnected = false;
        private IBClient ibClient;

        private HistoricalDataManager historicalDataManager;

        //private MarketDataManager marketDataManager;
        //private DeepBookManager deepBookManager;
        //private RealTimeBarsManager realTimeBarManager;
        //private ScannerManager scannerManager;
        //private OrderManager orderManager;
        //private AccountManager accountManager;
        //private ContractManager contractManager;
        //private AdvisorManager advisorManager;
        //private OptionsManager optionsManager;

        delegate void MessageHandlerDelegate(IBMessage message);

        public MainForm()
        {
            InitializeComponent();

            ibClient = new IBClient(this);
            historicalDataManager = new HistoricalDataManager(ibClient, historicalChart);

        }

        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }

        //This is the "UI entry point" and as such will handle the UI update by another thread
        public void HandleMessage(IBMessage message)
        {
            if (this.InvokeRequired)
            {
                MessageHandlerDelegate callback = new MessageHandlerDelegate(HandleMessage);
                this.Invoke(callback, new object[] { message });
            }
            else
            {
                UpdateUI(message);
            }
        }

        private void UpdateUI(IBMessage message)
        {
            switch (message.Type)
            {
                case MessageType.ConnectionStatus:
                    {
                        ConnectionStatusMessage statusMessage = (ConnectionStatusMessage)message;
                        if (statusMessage.IsConnected)
                        {
                            status_CT.Text = "Connected! Your client Id: " + ibClient.ClientId;

                            mnuFileConnect.Text = "&Disconnect";
                            IsConnected = true;
                        }
                        else
                        {
                            status_CT.Text = "Disconnected...";

                            mnuFileConnect.Text = "&Connect";
                            IsConnected = true;
                        }
                        break;
                    }
                case MessageType.Error:
                    {
                        ErrorMessage error = (ErrorMessage)message;
                        ShowMessageOnPanel("Request " + error.RequestId + ", Code: " + error.ErrorCode + " - " + error.Message + "\r\n");
                        HandleErrorMessage(error);
                        break;
                    }
                case MessageType.TickOptionComputation:
                case MessageType.TickPrice:
                case MessageType.TickSize:
                    {
                        HandleTickMessage((MarketDataMessage)message);
                        break;
                    }
                case MessageType.MarketDepth:
                case MessageType.MarketDepthL2:
                    {
                        //deepBookManager.UpdateUI(message);
                        break;
                    }
                case MessageType.HistoricalData:
                case MessageType.HistoricalDataEnd:
                    {
                        historicalDataManager.UpdateUI(message);
                        break;
                    }
                case MessageType.RealTimeBars:
                    {
                        //realTimeBarManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ScannerData:
                    {
                        //scannerManager.UpdateUI(message);
                        break;
                    }
                case MessageType.OpenOrder:
                case MessageType.OpenOrderEnd:
                case MessageType.OrderStatus:
                case MessageType.ExecutionData:
                case MessageType.CommissionsReport:
                    {
                        //orderManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ManagedAccounts:
                    {
                        //orderManager.ManagedAccounts = ((ManagedAccountsMessage)message).ManagedAccounts;
                        //accountManager.ManagedAccounts = ((ManagedAccountsMessage)message).ManagedAccounts;
                        //exerciseAccount.Items.AddRange(((ManagedAccountsMessage)message).ManagedAccounts.ToArray());
                        break;
                    }
                case MessageType.AccountSummaryEnd:
                    {
                        //accSummaryRequest.Text = "Request";
                        //accountManager.UpdateUI(message);
                        break;
                    }
                case MessageType.AccountDownloadEnd:
                    {
                        break;
                    }
                case MessageType.AccountUpdateTime:
                    {
                        //accUpdatesLastUpdateValue.Text = ((UpdateAccountTimeMessage)message).Timestamp;
                        break;
                    }
                case MessageType.PortfolioValue:
                    {
                        //accountManager.UpdateUI(message);
                        //if (exerciseAccount.SelectedItem != null)
                        //    optionsManager.HandlePosition((UpdatePortfolioMessage)message);
                        break;
                    }
                case MessageType.AccountSummary:
                case MessageType.AccountValue:
                case MessageType.Position:
                case MessageType.PositionEnd:
                    {
                        //accountManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ContractDataEnd:
                    {
                        //searchContractDetails.Enabled = true;
                        //contractManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ContractData:
                    {
                        HandleContractDataMessage((ContractDetailsMessage)message);
                        break;
                    }
                case MessageType.FundamentalData:
                    {
                        //fundamentalsQueryButton.Enabled = true;
                        //contractManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ReceiveFA:
                    {
                        //advisorManager.UpdateUI((AdvisorDataMessage)message);
                        break;
                    }
                default:
                    {
                        HandleMessage(new ErrorMessage(-1, -1, message.ToString()));
                        break;
                    }
            }
        }

        private void HandleTickMessage(MarketDataMessage tickMessage)
        {
            if (tickMessage.RequestId < OptionsManager.OPTIONS_ID_BASE)
            {
                //marketDataManager.UpdateUI(tickMessage);
            }
            else
            {
                //if (!queryOptionChain.Enabled)
                {
                //    queryOptionChain.Enabled = true;
                }
                //optionsManager.UpdateUI(tickMessage);
            }

        }

        private void HandleContractDataMessage(ContractDetailsMessage message)
        {
            if (message.RequestId > ContractManager.CONTRACT_ID_BASE && message.RequestId < OptionsManager.OPTIONS_ID_BASE)
            {
                //contractManager.UpdateUI(message);
            }
            else if (message.RequestId >= OptionsManager.OPTIONS_ID_BASE)
            {
                //optionsManager.UpdateUI(message);
            }
        }

        private void HandleErrorMessage(ErrorMessage message)
        {
            if (message.RequestId > MarketDataManager.TICK_ID_BASE && message.RequestId < DeepBookManager.TICK_ID_BASE)
            {
                //marketDataManager.NotifyError(message.RequestId);
            }
            else if (message.RequestId > DeepBookManager.TICK_ID_BASE && message.RequestId < HistoricalDataManager.HISTORICAL_ID_BASE)
            {
                //deepBookManager.NotifyError(message.RequestId);
            }
            else if (message.RequestId == ContractManager.CONTRACT_DETAILS_ID)
            {
                //contractManager.HandleRequestError(message.RequestId);
                //searchContractDetails.Enabled = true;
            }
            else if (message.RequestId == ContractManager.FUNDAMENTALS_ID)
            {
                //contractManager.HandleRequestError(message.RequestId);
                //fundamentalsQueryButton.Enabled = true;
            }
            else if (message.RequestId == OptionsManager.OPTIONS_ID_BASE)
            {
                //optionsManager.Clear();
                //queryOptionChain.Enabled = true;
            }
            else if (message.RequestId > OptionsManager.OPTIONS_ID_BASE)
            {
                //queryOptionChain.Enabled = true;
            }
            if (message.ErrorCode == 202)
            {
            }
        }

        private void mnuFileConnect_Click(object sender, EventArgs e)
        {
            DoConnect();
        }

        private void DoConnect()
        {
            if (!IsConnected)
            {
                try
                {
                    int port = 7496;
                    string host = "localhost";

                    ibClient.ClientId = 88666;
                    ibClient.ClientSocket.eConnect(host, port, ibClient.ClientId);
                }
                catch (Exception)
                {
                    HandleMessage(new ErrorMessage(-1, -1, "Please check your connection attributes."));
                }
            }
            else
            {
                ibClient.ClientSocket.eDisconnect();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cleanup();
        }

        private void Cleanup()
        {
            if (IsConnected)
            {
                ibClient.ClientSocket.eDisconnect();
            }
        }

        private void ShowMessageOnPanel(string message)
        {
            this.messageBox.Text += (message);
            messageBox.Select(messageBox.Text.Length, 0);
            messageBox.ScrollToCaret();
        }

        private void mnuShowHistoryData_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                Contract contract = new Contract
                {
                    SecType = "CASH",
                    Symbol = "EUR",
                    Exchange = "IDEALPRO",
                    Currency = "USD",
                    Expiry = "",
                    PrimaryExch = "",
                    IncludeExpired = false,
                    Right = null,
                    Strike = 0,
                    Multiplier = "",
                    LocalSymbol = ""
                };
                string endTime = "20130808 23:59:59 GMT";
                string duration = "30 D";
                // 1 secs, 5 secs, 10 secs, 15 secs, 30 secs, 
                // 1 min, 2 mins, 3 mins, 5 mins, 10 mins, 15 mins, 
                // 20 mins, 30 mins, 1 hour, 2 hours, 3 hours, 4 hours, 8 hours, 1 day, 1W, 1M
                string barSize = "1 hour";
                string whatToShow = "MIDPOINT";
                int outsideRTH = 0;
                historicalDataManager.AddRequest(contract, endTime, duration, barSize, whatToShow, outsideRTH, 1);
            }
        }
    }
}
