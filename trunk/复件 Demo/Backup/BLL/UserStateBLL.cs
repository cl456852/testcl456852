using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
using DAL;
namespace BLL
{
    public class UserStateBLL
    {
        public static List<UserState> GetAllUserState()
        {
            return UserStateDAL.SelectByOther("");
        }
    }
}
