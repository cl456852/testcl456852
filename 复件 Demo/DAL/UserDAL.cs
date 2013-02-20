using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
using DB;
using System.Data.SqlClient;
namespace DAL
{
    public class UserDAL
    {
        public static int Insert(User u)
        {
            string sql = "insert into user_tab values('" + u.Name + "','"+u.Address+"','"+u.Tel+"','"+u.Email+"',"+u.User_state.Id+")";
            return DBHelper.ExecuteSql(sql);
        }
        public static int Delete(User us)
        {
            string sql = "delete user_tab where id=" + us.Id;
            return DBHelper.ExecuteSql(sql);
        }
        public static int Update(User u)
        {
            string sql = "update user_tab set name='" + u.Name + "',address='"+u.Address+"',tel='"+u.Tel+"',email='"+u.Email+"',state="+u.User_state.Id+" where id=" + u.Id;
            return DBHelper.ExecuteSql(sql);
        }
        public static User SelectById(int id)
        {
            User us = null;
            string sql = "select * from user_tab where id=" + id;
            SqlDataReader rs = DBHelper.SearchSql(sql);
            if (rs.Read())
            {
                us = new User();
                us.Id = rs.GetInt32(0);
                us.Name = rs.GetString(1);
                us.Address = rs.GetString(2);
                us.Tel = rs.GetString(3);
                us.Email = rs.GetString(4);
                us.User_state = UserStateDAL.SelectById(rs.GetInt32(5));
            }
            rs.Close();
            return us;
        }
        public static List<User> SelectByOther(string other)
        {
            List<User> list = new List<User>();
            string sql = "select * from user_tab where 1=1 " + other;
            SqlDataReader rs = DBHelper.SearchSql(sql);
            while (rs.Read())
            {
                User us = new User();
                us.Id = rs.GetInt32(0);
                us.Name = rs.GetString(1);
                us.Address = rs.GetString(2);
                us.Tel = rs.GetString(3);
                us.Email = rs.GetString(4);
                us.User_state = UserStateDAL.SelectById(rs.GetInt32(5));
                list.Add(us);
            }
            rs.Close();
            return list;
        }
    }
}
