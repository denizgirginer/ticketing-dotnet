﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;

namespace PaymentsApi.Events
{
    public interface IOrderCancelledListener: IListenerBase
    {
    }
}
