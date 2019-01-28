using MyOrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL
{
    public partial class ContactDAO : CObjectDAO<Contact> { }

    public partial class ContactViewDAO : CObjectViewDAO<Contact>
    {
    }
}