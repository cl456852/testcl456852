using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CbipWp.Resources;
using Lxt2.Cbip.Api;
using Lxt2.Cbip.Api.Code.Cbip20;

namespace CbipWp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }
      
        CbipSender cbipSender;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (cbipSender == null)
            {
                ApiReceiver apiRecv = new ApiReceiver();
                RespReceiver respRecv = new RespReceiver();
                cbipSender = new CbipSender("192.168.4.89", 1237, 1, "1", "1", apiRecv, respRecv);
                
            }
            if (!cbipSender.IsConnected())
                cbipSender.Open();

            Submit submit = new Submit();
            submit.ClientSeq = 12345678;
            submit.SrcNumber = "";
            submit.MessagePriority = 1;
            submit.ReportType = 1;
            submit.MessageFormat = 15;
            submit.LinkID = "";
            submit.SendGroupID = 0;
            submit.ProductID = 2401;
            submit.MessageType = 0;
            //submit.DestMobileCount = 1;
            String[] tempMobile = new String[1];
            tempMobile[0] = "13511111111";
            submit.DestMobile = tempMobile;
            submit.MessageContent = "短信内容1";
            submit.Sign = "无线天利";
            submit.Custom = "扩展字段";
            cbipSender.SendSubmit(submit);
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}