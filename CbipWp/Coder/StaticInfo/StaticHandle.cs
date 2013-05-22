using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Lxt2.Cbip.Api.API;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Cbip.Api.Receiver.Cbip20;
using Lxt2.Cbip.Api.Code.Cbip20;

namespace Lxt2.Cbip.Api.StaticInfo
{
    public static class StaticHandle
    {
        private static CbipConfig config = new CbipConfig();
        /// <summary>
        /// 存储用户配置信息属性
        /// </summary>
        public static CbipConfig Config
        {
            get { return StaticHandle.config; }
            set { StaticHandle.config = value; }
        }

        /// <summary>
        /// 未返回RESP的ACTIVE测试包的数量
        /// </summary>
        public static int activeCount = 0;


        public static ManualResetEvent mmsContentEvent = new ManualResetEvent(false);
        /// <summary>
        /// 发送彩信资源线程等待，用于模拟同步
        /// </summary>
        public static ManualResetEvent MMSContentEvent
        {
            get { return mmsContentEvent; }
        }

        private static ContentMms mmsResource = new ContentMms();
        /// <summary>
        /// 临时存储发送彩信资源对象,用于匹配response
        /// </summary>
        public static ContentMms MmsResource
        {
            get { return StaticHandle.mmsResource; }
            set { StaticHandle.mmsResource = value; }
        }

        private static string resourceID;
        /// <summary>
        /// 彩信资源ID
        /// </summary>
        public static string ResourceID
        {
            get { return StaticHandle.resourceID; }
            set { StaticHandle.resourceID = value; }
        }

        private static int status;
        /// <summary>
        /// 彩信资源发送状态
        /// </summary>
        public static int Status
        {
            get { return StaticHandle.status; }
            set { StaticHandle.status = value; }
        }

        static IApiReceiver<AbsPackage> apiReceiver;
        /// <summary>
        /// 接收上行，状态报告对象，用户实现
        /// </summary>
        public static IApiReceiver<AbsPackage> ApiReceiver
        {
            get { return StaticHandle.apiReceiver; }
            set { StaticHandle.apiReceiver = value; }
        }

        static IRespReceiver respReceiver;
        /// <summary>
        /// 接收RESP对象，用户实现
        /// </summary>
        public static IRespReceiver RespReceiver
        {
            get { return respReceiver; }
            set { respReceiver = value; }
        }

        private static bool hasMO=false;

        public static bool HasMO
        {
            get { return StaticHandle.hasMO; }
            set { StaticHandle.hasMO = value; }
        }

        static int loginTimes;

        public static int LoginTimes
        {
            get { return loginTimes; }
            set { loginTimes = value; }
        }

        public static int smsCount=0;

        public static int SmsCount
        {
            get { return StaticHandle.smsCount; }
            set { StaticHandle.smsCount = value; }
        }

        public static int mmsCount=0;

        public static int MmsCount
        {
            get { return StaticHandle.mmsCount; }
            set { StaticHandle.mmsCount = value; }
        }

        public static int respSMSCount=0;

        public static int RespSMSCount
        {
            get { return StaticHandle.respSMSCount; }
            set { StaticHandle.respSMSCount = value; }
        }

        public static int respMMSCount=0;

        public static int RespMMSCount
        {
            get { return StaticHandle.respMMSCount; }
            set { StaticHandle.respMMSCount = value; }
        }

        public static int reportSMSCount=0;

        public static int ReportSMSCount
        {
            get { return StaticHandle.reportSMSCount; }
            set { StaticHandle.reportSMSCount = value; }
        }


        public static int reportMMSCount=0;

        public static int ReportMMSCount
        {
            get { return StaticHandle.reportMMSCount; }
            set { StaticHandle.reportMMSCount = value; }
        }

        public static int deliverCount=0;

        public static int DeliverCount
        {
            get { return StaticHandle.deliverCount; }
            set { StaticHandle.deliverCount = value; }
        }


        public static int resourseCount=0;

        public static int ResourseCount
        {
            get { return StaticHandle.resourseCount; }
            set { StaticHandle.resourseCount = value; }
        }

        static int smsMessageLength = 1000;
        /// <summary>
        /// 短信内容长度限制
        /// </summary>
        public static int SmsMessageLength
        {
            get { return smsMessageLength; }
            set { smsMessageLength = value; }
        }
        static int mmsResoureceSize = 409600;
        /// <summary>
        /// 彩信资源大小限制
        /// </summary>
        public static int MmsResoureceSize
        {
            get { return mmsResoureceSize; }
            set { mmsResoureceSize = value; }
        }
        static int maxGroupSize = 100;
        /// <summary>
        /// 组包个数限制
        /// </summary>
        public static int MaxGroupSize
        {
            get { return maxGroupSize; }
            set { maxGroupSize = value; }
        }
    }
}
