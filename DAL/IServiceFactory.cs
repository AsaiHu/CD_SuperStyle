using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;

namespace DAL.Business
{
    public interface IServiceFactory
    {
        IUsersService UsersService { get; }
        IRolesService RolesService { get; }
        IUserInRoleService UserInRoleService { get; }
        IRightsService RightsService { get; }
        IModulesService ModulesService { get; }
        IParamsService ParamsService { get; }
        IParamsTypeService ParamsTypeService { get; }
        ILogsService LogsService { get; }

        IClassesService ClassesService { get; }
        IArticlesService ArticlesService { get; }
        IConfigsService ConfigsService { get; }
    }
}
