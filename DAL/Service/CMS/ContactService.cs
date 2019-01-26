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
    public interface IContactService : IEntityService<Contact>, IEntityViewService<Contact>, IEntityService, IEntityViewService
    {
        
    }

    public class ContactService : ServiceBase<Contact, Contact>, IContactService
    {
        
    }
}