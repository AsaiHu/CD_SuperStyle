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
    public interface IProductService : IEntityService<Product>, IEntityViewService<Product>, IEntityService, IEntityViewService
    {
        
    }

    public class ProductService : ServiceBase<Product, Product>, IProductService
    {
        
    }
}