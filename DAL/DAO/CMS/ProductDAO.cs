using MyOrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL
{
    public partial class ProductDAO : CObjectDAO<Product> { }

    public partial class ProductViewDAO : CObjectViewDAO<Product>
    {
    }
}