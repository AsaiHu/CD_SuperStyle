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
    public interface ICharacterService : IEntityService<Character>, IEntityViewService<Character>, IEntityService, IEntityViewService
    {
        
    }

    public class CharacterService : ServiceBase<Character, Character>, ICharacterService
    {
        
    }
}