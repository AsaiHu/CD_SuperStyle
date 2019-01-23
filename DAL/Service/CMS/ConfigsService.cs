using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Business;
using MyOrm.Common;

namespace DAL
{
    public interface IConfigsService : IEntityService<Configs>, IEntityViewService<ConfigsView>, IEntityService, IEntityViewService
    {
        bool Save(Modules entity);
        bool Delete(int ModuleID);
    }

    public class ConfigsService : ServiceBase<Configs, ConfigsView>, IConfigsService
    {
        public bool Save(Modules entity)
        {
            IConfigsService Service = ServiceFactory.Factory.ConfigsService;
            ConditionSet Condition = new ConditionSet();
            Condition.Add(new SimpleCondition("ModuleID", entity.ID));
            Configs configs = Service.SearchOne(Condition);

            if (configs == null)
            {
                Configs config = new Configs();
                config.ModuleID = Convert.ToInt16(entity.ID);
                config.TitleFlag = false;
                config.ShortTitleFlag = false;
                config.PicPathFlag = false;
                config.ArticleContentFlag = false;
                config.DescriptionFlag = false;
                config.CutPathFlag = false;
                config.AuthorFlag = false;
                config.KeywordsFlag = false;
                config.PublishFlag = false;
                config.PublishTimeFlag = false;

                Service.Insert(config);
               
            }
            else
            {
                return true;
            }
            return true;
        }

        public bool Delete(int ModuleID) {
            IConfigsService Service = ServiceFactory.Factory.ConfigsService;
            ConditionSet Condition = new ConditionSet();
            Condition.Add(new SimpleCondition("ModuleID",ModuleID));
            Configs configs = Service.SearchOne(Condition);

            ServiceFactory.Factory.ConfigsService.DeleteID(configs.ID);

            return true;
        }
    }
}
