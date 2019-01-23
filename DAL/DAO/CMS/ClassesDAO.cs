using MyOrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL
{
    public partial class ClassesDAO : CObjectDAO<Classes> { }

    public partial class ClassesViewDAO : CObjectViewDAO<ClassesView>
    {
        public DataTable GetAll()
        {
            try
            {
                using (IDbCommand command = MakeParamCommand("select *,Convert(int,ClassCode) as ClassSequence from CMS_Class", null))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable GetAllClassForDropdown()
        {
            try
            {
                using (IDbCommand command = MakeParamCommand("   select ID,ClassCode,"
                      +" case len(ClassCode) when 3 then '['+ClassCode+']'+ClassName "
                      +" when 5 then '---'+'['+ClassCode+']'+ClassName  "
                      + " when 7 then '-----'+'['+ClassCode+']'+ClassName end  as TextField from CMS_Class   order by ClassCode,convert(int,classCode)", null))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable GetSubClass(string parentClassCode)
        {
            try
            {
                using (IDbCommand command = MakeParamCommand("   select * from CMS_Class where UpperClassCode='" + parentClassCode + "' and ClassStatus=1   order by Sequence,ClassCode,convert(int,classCode)", null))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

       
    }
}