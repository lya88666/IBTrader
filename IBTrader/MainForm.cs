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
        private bool _isConnected = false;
        private IBClient _ibClient;

        private MarketDataManager marketDataManager;
        private DeepBookManager deepBookManager;
        private HistoricalDataManager historicalDataManager;
        private RealTimeBarsManager realTimeBarManager;
        private ScannerManager scannerManager;
        private OrderManager orderManager;
        private AccountManager accountManager;
        private ContractManager contractManager;
        private AdvisorManager advisorManager;
        private OptionsManager optionsManager;

        delegate void MessageHandlerDelegate(IBMessage message);

        public MainForm()
        {
            InitializeComponent();
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; }
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
                            //status_CT.Text = "Connected! Your client Id: " + _ibClient.ClientId;
                            //connectButton.Text = "Disconnect";
                        }
                        else
                        {
                            //status_CT.Text = "Disconnected...";
                            //connectButton.Text = "Connect";
                        }
                        break;
                    }
                case MessageType.Error:
                    {
                        ErrorMessage error = (ErrorMessage)message;
                        //ShowMessageOnPanel("Request " + error.RequestId + ", Code: " + error.ErrorCode + " - " + error.Message + "\r\n");
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
                        deepBookManager.UpdateUI(message);
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
                        realTimeBarManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ScannerData:
                    {
                        scannerManager.UpdateUI(message);
                        break;
                    }
                case MessageType.OpenOrder:
                case MessageType.OpenOrderEnd:
                case MessageType.OrderStatus:
                case MessageType.ExecutionData:
                case MessageType.CommissionsReport:
                    {
                        orderManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ManagedAccounts:
                    {
                        orderManager.ManagedAccounts = ((ManagedAccountsMessage)message).ManagedAccounts;
                        accountManager.ManagedAccounts = ((ManagedAccountsMessage)message).ManagedAccounts;
                        //exerciseAccount.Items.AddRange(((ManagedAccountsMessage)message).ManagedAccounts.ToArray());
                        break;
                    }
                case MessageType.AccountSummaryEnd:
                    {
                        //accSummaryRequest.Text = "Request";
                        accountManager.UpdateUI(message);
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
                        accountManager.UpdateUI(message);
                        //if (exerciseAccount.SelectedItem != null)
                        //    optionsManager.HandlePosition((UpdatePortfolioMessage)message);
                        break;
                    }
                case MessageType.AccountSummary:
                case MessageType.AccountValue:
                case MessageType.Position:
                case MessageType.PositionEnd:
                    {
                        accountManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ContractDataEnd:
                    {
                        //searchContractDetails.Enabled = true;
                        contractManager.UpdateUI(message);
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
                        contractManager.UpdateUI(message);
                        break;
                    }
                case MessageType.ReceiveFA:
                    {
                        advisorManager.UpdateUI((AdvisorDataMessage)message);
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
                marketDataManager.UpdateUI(tickMessage);
            }
            else
            {
                //if (!queryOptionChain.Enabled)
                {
                //    queryOptionChain.Enabled = true;
                }
                optionsManager.UpdateUI(tickMessage);
            }

        }

        private void HandleContractDataMessage(ContractDetailsMessage message)
        {
            if (message.RequestId > ContractManager.CONTRACT_ID_BASE && message.RequestId < OptionsManager.OPTIONS_ID_BASE)
            {
                contractManager.UpdateUI(message);
            }
            else if (message.RequestId >= OptionsManager.OPTIONS_ID_BASE)
            {
                optionsManager.UpdateUI(message);
            }
        }

        private void HandleErrorMessage(ErrorMessage message)
        {
            if (message.RequestId > MarketDataManager.TICK_ID_BASE && message.RequestId < DeepBookManager.TICK_ID_BASE)
                marketDataManager.NotifyError(message.RequestId);
            else if (message.RequestId > DeepBookManager.TICK_ID_BASE && message.RequestId < HistoricalDataManager.HISTORICAL_ID_BASE)
                deepBookManager.NotifyError(message.RequestId);
            else if (message.RequestId == ContractManager.CONTRACT_DETAILS_ID)
            {
                contractManager.HandleRequestError(message.RequestId);
                //searchContractDetails.Enabled = true;
            }
            else if (message.RequestId == ContractManager.FUNDAMENTALS_ID)
            {
                contractManager.HandleRequestError(message.RequestId);
                //fundamentalsQueryButton.Enabled = true;
            }
            else if (message.RequestId == OptionsManager.OPTIONS_ID_BASE)
            {
                optionsManager.Clear();
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
    }
}
