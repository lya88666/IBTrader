using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmengSoft.IBTrader
{
    public abstract class DataManager
    {
        protected Control uiControl;
        protected IBClient ibClient;
        protected int currentTicker = 1;

        protected delegate void UpdateUICallback(IBMessage msg);

        public DataManager(IBClient client, Control dataGrid)
        {
            ibClient = client;
            uiControl = dataGrid;
        }

        public abstract void NotifyError(int requestId);

        public abstract void Clear();

        public abstract void UpdateUI(IBMessage message);

    }
}
