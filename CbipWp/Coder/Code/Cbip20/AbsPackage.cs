using System;
using Lxt2.Cbip.Api.StaticInfo;
using Lxt2.Communication.Framework;
using Lxt2.Communication.Framework.Util;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public abstract class AbsPackage : IPackage
    {
        // CBIP 2.0 commandId 定义	
        public const uint CBIP_LOGIN = 0x00000101;

        public const uint CBIP_LOGIN_RESP = 0x80000101;

        public const uint CBIP_LOGOUT = 0x00000102;

        public const uint CBIP_LOGOUT_RESP = 0x80000102;

        public const uint CBIP_ACTIVE = 0x00000103;

        public const uint CBIP_ACTIVE_RESP = 0x80000103;

        public const uint CBIP_DELIVER = 0x00000104;

        public const uint CBIP_DELIVER_RESP = 0x80000104;

        public const uint CBIP_SUBMIT = 0x00000105;

        public const uint CBIP_SUBMIT_RESPONSE = 0x80000105;

        public const uint CBIP_SUBMIT_MMS = 0x00000106;

        public const uint CBIP_SUBMITMMS_RESPONSE = 0x80000106;

        public const uint CBIP_REPORT = 0x00000107;

        public const uint CBIP_REPORT_RESP = 0x80000107;

        public const uint CBIP_CONTENT_MMS = 0x00000108;

        public const uint CBIP_CONTENT_MMS_RESPONSE = 0x80000108;

        int totalLength;

        /// <summary>
        /// 包长
        /// </summary>
        public int TotalLength
        {
            get { return totalLength; }
            set { totalLength = value; }
        }
        uint commandID;
        /// <summary>
        /// 命令ID
        /// </summary>
        public uint CommandID
        {
            get { return commandID; }
            set { commandID = value; }
        }
        long sequenceID;
        /// <summary>
        /// 事务ID
        /// </summary>
        public long SequenceID
        {
            get
            {
                if (sequenceID == 0)
                {
                    this.sequenceID = StaticTool.getSquenceID();
                    return sequenceID;
                }
                else
                    return this.sequenceID;
            }
            internal set { sequenceID = value; }
        }

        public long GetKey()
        {
            return SequenceID;
        }
        int commandStatus = 0;
        /// <summary>
        /// 命令状态
        /// </summary >
        public int CommandStatus
        {
            get { return commandStatus; }
            set { commandStatus = value; }
        }

        ByteBuffer buffHaed = new ByteBuffer();

        public ByteBuffer BuffHead
        {
            get { return buffHaed; }
            set { buffHaed = value; }
        }

        ByteBuffer buffBody = new ByteBuffer();

        public ByteBuffer BuffBody
        {
            get { return buffBody; }
            set { buffBody = value; }
        }

        #region IPackage 成员

        public byte[] GetHead()
        {
            BuffHead = new ByteBuffer();
            BuffHead.PutInt(this.totalLength);
            BuffHead.PutUint(this.commandID);
            BuffHead.PutLong(this.SequenceID);
            BuffHead.PutInt(this.commandStatus);
            return BuffHead.Buff;
        }

        public void SetHead(byte[] head)
        {
            buffHaed.Position = 0;
            buffHaed.Buff = head;
            this.TotalLength = buffHaed.GetInt();
            this.CommandID = buffHaed.GetUint();
            this.SequenceID = buffHaed.GetLong();
            this.CommandStatus = buffHaed.GetInt();
        }

        public abstract byte[] GetBody();

        public abstract void SetBody(byte[] body);

        public byte[] GetPackage()
        {
            this.BuffBody = new ByteBuffer();
            this.BuffHead = new ByteBuffer();
            byte[] package = ByteHelper.AddByte(this.GetHead(), this.GetBody());
            ByteHelper.PutInt(ref package, package.Length, 0);
            return package;
        }

        public void SetPackage(byte[] package)
        {
            this.SetHead(ByteHelper.GetByteArr(package, 0, 20));
            this.SetBody(ByteHelper.GetByteArr(package, 20, package.Length - 20));
        }

        public Boolean CheckMessage(ref IPackage package, byte[] packageBytes)
        {
            uint temp = ByteHelper.GetUInt(packageBytes, 4);
            if (this.CommandID == temp)
            {
                IPackage _package = this.GetInstance();
                _package.SetPackage(packageBytes);
                package = _package;
                return true;
            }
            else
                return false;
        }

        #endregion



        public abstract IPackage Clone();

        public abstract IPackage GetInstance();

        object o = new object();



        public override string ToString()
        {
            return StaticTool.ObjToString(this);
        }

    }
}
