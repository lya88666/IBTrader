﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmengSoft.IBTrader
{
    public abstract class IBMessage
    {
        protected MessageType type;

        public MessageType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
