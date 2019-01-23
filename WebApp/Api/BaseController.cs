using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Api
{
    [RequestAuthorize]
    public class BaseController:Controller
    {
    }
}