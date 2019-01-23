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
    public interface IArticlesService : IEntityService<Articles>, IEntityViewService<ArticlesView>, IEntityService, IEntityViewService
    {
        
    }

    public class ArticlesService : ServiceBase<Articles, ArticlesView>, IArticlesService
    {
        
    }
}