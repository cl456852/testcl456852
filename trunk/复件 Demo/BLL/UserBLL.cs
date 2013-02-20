using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
using DAL;
namespace BLL
{
    public class UserBLL
    {
        public static int Save(string name,string address,string tel,string email,int state)
        {
            User u = new User();
            u.Name = name;
            u.Address = address;
            u.Tel = tel;
            u.Email = email;
            u.User_state = UserStateDAL.SelectById(state);
            return UserDAL.Insert(u);
        }
        public static int Remove(int id)
        {
            User u = new User();
            u.Id = id;
            return UserDAL.Delete(u);
        }
        public static List<User> FindAllUser()
        {
            return UserDAL.SelectByOther("");
        }
    }
}
