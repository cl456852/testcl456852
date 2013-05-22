using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Communication.Framework.Util
{
    /// <summary>
    /// 此类为编解码用工具类
    /// </summary>
    public class ByteBuffer
    {

        public ByteBuffer()
        {
        }
        /// <summary>
        /// 用于存储BYTE数组
        /// </summary>
        byte[] buff = new byte[0];

        public byte[] Buff
        {
            get { return buff; }
            set { buff = value; }
        }
        /// <summary>
        /// 游标，用于记录PUT和GET到的位置
        /// </summary>
        int position=0;

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        #region 用于编码，向BYTE数组中追加数据同时移动游标,位移为数据所占长度
        public void PutInt(int num)
        {
            this.Buff = ByteHelper.AddByte(this.Buff, ByteHelper.IntToBytes(num));
            //Position += 4;
        }

        public void PutLong(long num)
        {
            this.Buff = ByteHelper.AddByte(this.Buff, ByteHelper.LongToBytes(num));
            //Position += 8;
        }

        public void PutShort(short num)
        {
            this.Buff = ByteHelper.AddByte(this.Buff, ByteHelper.ShortToBytes(num));
            //Position += 2;
        }

        public void PutByte(byte num)
        {
            this.Buff = ByteHelper.AddByte(this.Buff, ByteHelper.ByteToBytes(num));
            //Position += 1;
        }

        public void PutUint(uint num)
        {
            this.Buff = ByteHelper.AddByte(this.Buff, ByteHelper.UintToBytes(num));
            //Position += 4;
        }

        public void PutString(string s, int length)
        {
            if (s == null)
            {
                s = "";
            }
            this.Buff = ByteHelper.AddByte(this.Buff, ByteHelper.StringToBytes(s, length));
            //Position += length;
        }

        public void PutGuidString(string s, int length)
        {
            byte[] _temp = new byte[length];
            byte[] guidByte = ByteHelper.PutGuidByte(s);
            guidByte.CopyTo(_temp,0);

            this.Buff = ByteHelper.AddByte(this.Buff, _temp);
        }
        #endregion

        #region 用于解码方法，得到不同的数据类型，同时移动游标，位移为数据所占长度

        public int GetInt()
        {
            try
            {
                int i = ByteHelper.GetInt(this.Buff, Position);
                this.Position += 4;
                return i;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public long GetLong()
        {
            long l = ByteHelper.GetLong(this.Buff, Position);
            this.Position += 8;
            return l;
        }

        public byte GetByte()
        {
            byte b = this.Buff[Position];
            this.Position += 1;
            return b;
        }

        public uint GetUint()
        {
            uint u = ByteHelper.GetUInt(this.Buff,Position);
            this.Position += 4;
            return u;
        }

        public short GetShort()
        {
            short sh = ByteHelper.GetShort(this.Buff,Position);
            this.Position += 2;
            return sh;
        }

        public string GetString(int length)
        {
            string s = ByteHelper.GetString(this.Buff, this.Position, length);
            this.Position += length;
            return s;
        }

        public string GetStringByFmt(int length, int fmt)
        {
            string s = ByteHelper.GetStringByFmt(this.Buff, this.Position, length, fmt);
            this.Position += length;
            return s;
        }

        public string GetGuidString(int length)
        {        
            byte[] b1 = ByteHelper.GetByteArr(this.buff, this.position, 16);

            string s = ByteHelper.GetGuidString(b1);
            //转换.net生成的Guid与java生成的一致（高低位转换）
            string[] d = s.ToString().Split('-');
            string ld = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                int index = d[i].Length - 2;
                for (int j = 0; j < d[i].Length / 2; j++)
                {


                    ld += d[i].Substring(index, 2);

                    index = index - 2;
                }
                ld += "-";

            }
            ld += d[3];
            ld += "-";
            ld += d[4];
            this.position += length;
            return ld;
            
        }
        #endregion

    }
}
