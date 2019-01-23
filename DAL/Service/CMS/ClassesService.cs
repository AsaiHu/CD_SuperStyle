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
    public interface IClassesService : IEntityService<Classes>, IEntityViewService<ClassesView>, IEntityService, IEntityViewService
    {
        DataTable GetAll();
        DataTable GetAllClassForDropdown();
        DataTable GetSubClass(string parentClassCode);
    }

    public class ClassesService : ServiceBase<Classes, ClassesView>, IClassesService
    {
        public DataTable GetAll()
        {
            try
            {
                return ((ClassesViewDAO)ObjectViewDAO).GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAllClassForDropdown()
        {
            try
            {
                return ((ClassesViewDAO)ObjectViewDAO).GetAllClassForDropdown();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSubClass(string parentClassCode)
        {
            try
            {
                return ((ClassesViewDAO)ObjectViewDAO).GetSubClass(parentClassCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
