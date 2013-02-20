using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BLL;
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string name = TextBox1.Text;
        string address = TextBox2.Text;
        string tel = TextBox3.Text;
        string email = TextBox4.Text;
        int state=Convert.ToInt32(DropDownList1.SelectedValue);
        if (UserBLL.Save(name, address, tel, email, state) > 0)
        {
            //Response.Write("<script>alert('添加成功');</script>");
        }
        else
        {
            //Response.Write("<script>alert('添加失败');</script>");
        }
        GridView1.DataBind();
    }
}
