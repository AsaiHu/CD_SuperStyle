﻿using MyOrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL
{
    public partial class LogsDAO : CObjectDAO<Logs> { }

    public partial class LogsViewDAO : CObjectViewDAO<Logs>
    {
        
    }
}