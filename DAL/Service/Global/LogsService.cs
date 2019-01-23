
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using DAL.Business;
using System.Data;
using MyOrm.Common;

namespace DAL
{
    public interface ILogsService : IEntityService<Logs>, IEntityViewService<Logs>, IEntityService, IEntityViewService
    {
    }

    public class LogsService : ServiceBase<Logs, Logs>, ILogsService
    {

    }
}