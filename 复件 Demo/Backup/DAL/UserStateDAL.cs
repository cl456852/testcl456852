using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
using DB;
using System.Data.SqlClient;
namespace DAL
{
    public class UserStateDAL
    {
        public static int Insert(UserState us)
        {
            string sql = "insert into userstate_tab values('"+us.Name+"')";
            return DBHelper.ExecuteSql(sql);
        }
        public static int Delete(UserState us)
        {
            string sql = "delete userstate_tab where id="+us.Id;
            return DBHelper.ExecuteSql(sql);
        }
        public static int Update(UserState us)
        {
            string sql = "update userstate_tab set name='"+us.Name+"' where id="+us.Id;
            return DBHelper.ExecuteSql(sql);
        }
        public static UserState SelectById(int id)
        {
            UserState us = null;
            string sql = "select * from userstate_tab where id="+id;
            SqlDataReader rs = DBHelper.SearchSql(sql);
            if (rs.Read())
            {
                us = new UserState();
                us.Id = rs.GetInt32(0);
                us.Name = rs.GetString(1);
            }
            rs.Close();
            return us;
        }
        public static List<UserState> SelectByOther(string other)
        {
            List<UserState> list = new List<UserState>();
            string sql = "select * from userstate_tab where 1=1 " + other;
            SqlDataReader rs = DBHelper.SearchSql(sql);
           while(rs.Read())
            {
                UserState us = new UserState();
                us.Id = rs.GetInt32(0);
                us.Name = rs.GetString(1);
                list.Add(us);
            }
            rs.Close();
            return list;
        }

    }
}
