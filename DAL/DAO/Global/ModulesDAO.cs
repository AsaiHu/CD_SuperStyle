using MyOrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL
{
    public partial class ModulesDAO : CObjectDAO<Modules> { }

    public partial class ModulesViewDAO : CObjectViewDAO<Modules>
    {
        public string GenBusiNo(string prefix, string format, int length)
        {
            try
            {
                using (IDbCommand command = MakeParamCommand("exec GenBusiNo @0,@1,@2", new object[] { prefix, DateTime.Now.ToString(format), length }))
                {
                    string busiNo = command.ExecuteScalar().ToString();
                    return busiNo;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        public DataTable GetAll(bool? hide)
        {
            try
            {
                string sql = "select * from Sys_Module where 1=1";
                if (hide != null) sql += " and Hide=" + ((bool)hide ? "1" : "0");
                using (IDbCommand command = MakeParamCommand(sql, null))
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

        public DataTable GetByUserId(int userId)
        {
            try
            {
                string sql1 = "select * from Sys_Module where Hide=0 and Src is not null and Src<>''";
                string sql2 = "select RoleID from Sys_Role_User where UserID=@0";
                string sql = "select * from (" + sql1 + ")module where ID in (select ModuleID from Sys_Right where RoleID in (" + sql2 + "))";
                string _sql = "select * from Sys_Module where ID in (select UpperModuleID from Sys_Module where ID in (select ModuleID from Sys_Right where RoleID in (" + sql2 + ")))";
                using (IDbCommand command = MakeParamCommand(sql + " union " + _sql, new object[] { userId }))
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