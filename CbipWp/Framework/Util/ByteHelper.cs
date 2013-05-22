using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Lxt2.Communication.Framework.Util
{
    public static class ByteHelper
    {
        /// <summary>
        /// 从位置index开始向b中增加short类型数据
        /// </summary>
        /// <param name="b"></param>
        /// <param name="s"></param>
        /// <param name="index"></param>
        public static void PutShort(ref byte[] b, short s, int index)
        {
            b[index] = (byte)(s >> 8);
            b[index + 1] = (byte)(s >> 0);
        }

        /// <summary>
        /// 从位置index开始从b中取short类型数据
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static short GetShort(byte[] b, int index)
        {
            return (short)(((b[index] << 8) | b[index + 1] & 0xff));
        }

        /// <summary>
        /// 从位置index开始向b中增加int类型数据
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="x"></param>
        /// <param name="index"></param>
        public static void PutInt(ref byte[] bb, int x, int index)
        {
            bb[index + 0] = (byte)(x >> 24);
            bb[index + 1] = (byte)(x >> 16);
            bb[index + 2] = (byte)(x >> 8);
            bb[index + 3] = (byte)(x >> 0);
        }

        /// <summary>
        /// 从位置index开始从b中取int类型数据
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int GetInt(byte[] bb, int index)
        {
            return (int)((((bb[index + 0] & 0xff) << 24)
                    | ((bb[index + 1] & 0xff) << 16)
                    | ((bb[index + 2] & 0xff) << 8) | ((bb[index + 3] & 0xff) << 0)));
        }

        /// <summary>
        /// 从位置index开始向b中增加int类型数据
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="x"></param>
        /// <param name="index"></param>
        public static void PutUInt(ref byte[] bb, uint x, int index)
        {
            bb[index + 0] = (byte)(x >> 24);
            bb[index + 1] = (byte)(x >> 16);
            bb[index + 2] = (byte)(x >> 8);
            bb[index + 3] = (byte)(x >> 0);
        }

        /// <summary>
        /// 从位置index开始从b中取int类型数据
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static uint GetUInt(byte[] bb, int index)
        {
            return (uint)((((bb[index + 0] & 0xff) << 24)
                    | ((bb[index + 1] & 0xff) << 16)
                    | ((bb[index + 2] & 0xff) << 8) | ((bb[index + 3] & 0xff) << 0)));
        }

        /// <summary>
        /// 从位置index开始向b中增加long类型数据
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="x"></param>
        /// <param name="index"></param>
        public static void PutLong(ref byte[] bb, long x, int index)
        {
            bb[index + 0] = (byte)(x >> 56);
            bb[index + 1] = (byte)(x >> 48);
            bb[index + 2] = (byte)(x >> 40);
            bb[index + 3] = (byte)(x >> 32);
            bb[index + 4] = (byte)(x >> 24);
            bb[index + 5] = (byte)(x >> 16);
            bb[index + 6] = (byte)(x >> 8);
            bb[index + 7] = (byte)(x >> 0);
        }

        /// <summary>
        /// 从位置index开始从b中取long类型数据
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static long GetLong(byte[] bb, int index)
        {
            return ((((long)bb[index + 0] & 0xff) << 56)
                    | (((long)bb[index + 1] & 0xff) << 48)
                    | (((long)bb[index + 2] & 0xff) << 40)
                    | (((long)bb[index + 3] & 0xff) << 32)
                    | (((long)bb[index + 4] & 0xff) << 24)
                    | (((long)bb[index + 5] & 0xff) << 16)
                    | (((long)bb[index + 6] & 0xff) << 8) | (((long)bb[index + 7] & 0xff) << 0));
        }

        /// <summary>
        /// 从位置index开始向b中增加string类型数据
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="index"></param>
        /// <param name="str"></param>
        /// <param name="lenth"></param>
        //public static void PutString(ref byte[] bb, int index, String str, int lenth)
        //{
        //    if (str.Length <= lenth)
        //    {
        //        byte[] _temp = System.Text.Encoding.Default.GetBytes(str);
        //        for (int i = 0; i < _temp.Length; i++)
        //        {
        //            bb[i + index] = _temp[i];
        //        }
        //    }
        //}


        /// <summary>
        /// 从位置index开始从b中取string类型数据
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="index"></param>
        /// <param name="lenth"></param>
        /// <returns></returns>
        public static String GetString(byte[] bb, int index, int lenth)
        {
            byte[] _temp = new byte[lenth];
            for (int i = 0; i < _temp.Length; i++)
            {
                _temp[i] = bb[index + i];
            }
            return Encoding.GetEncoding("utf-8").GetString(_temp,0,_temp.Length).Trim();
        }

        /// <summary>
        /// 从index开始获取长度为lenth'的byte数组
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="index"></param>
        /// <param name="lenth"></param>
        /// <returns></returns>
        public static byte[] GetByteArr(byte[] bb, int index, int lenth)
        {
            byte[] _temp = new byte[lenth];
            for (int i = 0; i < _temp.Length; i++)
            {
                _temp[i] = bb[index + i];
            }
            return _temp;
        }

        /// <summary>
        /// 从bbt的index位置开始向bbt中增加bbo
        /// </summary>
        /// <param name="bbt"></param>
        /// <param name="index"></param>
        /// <param name="bbo"></param>
        public static void PutByteArr(ref byte[] bbt, int index, byte[] bbo)
        {
            byte[] temp;
            try
            {
                if (index > 0)
                {
                    temp = new byte[index + bbo.Length];
                    ByteHelper.GetByteArr(bbt, 0, index).CopyTo(temp, 0);
                    bbo.CopyTo(temp, index);
                }
                else
                {
                    temp = new byte[bbo.Length];
                    bbo.CopyTo(temp, 0);
                }
                bbt = temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static byte[] PutGuidByte(string GUIDString)
        {
            Guid g = new Guid(GUIDString);
            return g.ToByteArray();
        }

        public static string GetGuidString(byte[] guidByte)
        {


            Guid g = new Guid(guidByte);

            string s = g.ToString();

            return g.ToString();

            //byte[] _temp = new byte[lenth];
            //for (int i = 0; i < _temp.Length; i++)
            //{
            //    _temp[i] = bb[index + i];
            //}
            //Guid g = new Guid(_temp);
            //return g.ToString();
        }

        #region 公共方法
        /// <summary>
        /// 将LIST中的BYTE 数组相连
        /// </summary>
        /// <param name="bytesList"></param>
        /// <returns></returns>
        public static byte[] AddByte(List<byte[]> bytesList)
        {
            byte[] c = null;
            for (int i = 0; i < bytesList.Count; i++)
            {
                c = AddByte(c, bytesList[i]);
            }
            return c;
        }

        /// <summary>
        /// 相连2个BYTE数组
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte[] AddByte(byte[] a, byte[] b)
        {
            if (a == null)
                return b;
            if (b == null)
                return a;
            byte[] c = new byte[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }
        /// <summary>
        /// 将BYTE 数组颠倒
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// 
        public static byte[] ReverseByte(byte[] a)
        {
            byte[] b = new byte[a.Length];
            for (int i = a.Length - 1; i >= 0; i--)
            {
                b[b.Length - i - 1] = a[i];
            }
            return b;
        }

        public static byte[] IntToBytes(int i, int length)
        {
            byte[] resultBytes = null;
            byte[] bytes = bytes = BitConverter.GetBytes(i);
            bytes = ReverseByte(bytes);
            if (length > bytes.Length)
            {
                resultBytes = new byte[length];
                bytes.CopyTo(resultBytes, bytes.Length);
                return resultBytes;
            }
            return bytes;
        }


        /// <summary>
        /// 将字符串转换为BYTE数组，定长，LENGTH为长度，后补零
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string s, int length)
        {
            byte[] resultBytes = null;

            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(s);
            if (length > bytes.Length)
            {
                resultBytes = new byte[length];
                bytes.CopyTo(resultBytes, 0);
                return resultBytes;
            }
            return bytes;

        }
        /// <summary>
        /// 计算LIST中的数组总长度
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int CountLength(List<byte[]> list)
        {
            int length = 0;
            for (int i = 0; i < list.Count; i++)
                length += list[i].Length;
            return length;
        }

        #region 不同类型的数字转换为BYTE数组
        public static byte[] IntToBytes(int i)
        {

            byte[] bytes = BitConverter.GetBytes(i);
            return ReverseByte(bytes);
        }

        public static byte[] ShortToBytes(short i)
        {

            byte[] bytes = BitConverter.GetBytes(i);
            return ReverseByte(bytes);
        }


        public static byte[] LongToBytes(long i)
        {

            byte[] bytes = BitConverter.GetBytes(i);
            return ReverseByte(bytes);
        }

        public static byte[] UintToBytes(uint i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            return ReverseByte(bytes);
        }

        public static byte[] ByteToBytes(byte i)
        {
            return new byte[] { i };
        }

        public static short GetEncodedLength(string s)
        {
            try
            {
                return Convert.ToInt16(Encoding.GetEncoding("utf-8").GetBytes(s).Length);
            }
            catch (Exception e)
            {
                //Logger.Warning("ByteHelper", e.Message + e.StackTrace);
                return 0;
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// 得到从1970-1-1毫秒数
        /// </summary>
        /// <returns></returns>
        public static long GetMilliSec()
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1");
            return Convert.ToInt64(ts.TotalMilliseconds);
        }


        public static string ObjToString(object o)
        {
            string log="";
            Type t = o.GetType();
            log += t.Name+"   ";
            foreach (PropertyInfo pi in t.GetProperties())
            {
                object value1 = pi.GetValue(o, null);
                string name = pi.Name;
                {
                    if (name != "BuffHead" && name != "BuffBody")
                    {
                        string s = pi.Name + ":" + value1.ToString();
                        log += s + " ";
                    }
                }
            }
            //Console.WriteLine( log);
            return log;
        }

        public static string GetStringByFmt(byte[] bb, int index, int lenth, int fmt)
        {
            string result = "";
            byte[] _temp;
            if (fmt == 8)
            {
                _temp = new byte[lenth];
                for (int i = 0; i < _temp.Length; i++)
                {
                    _temp[i] = bb[index + i];
                }
                byte[] b = revertByte(_temp);

                result = Encoding.GetEncoding("iso-10646-ucs-2").GetString(_temp,0,_temp.Length).Trim();
            }
            else
            {
                _temp = new byte[lenth];
                for (int i = 0; i < _temp.Length; i++)
                {
                    _temp[i] = bb[index + i];
                }
                result = Encoding.GetEncoding("utf-8").GetString(_temp,0,_temp.Length).Trim();
            }

               
            return result;
        }

        public static byte[] revertByte(byte[] b)
        {
            byte[] a=new byte[b.Length];
            for (int i = 0; i <= b.Length - 2; i=i + 2)
            {
                a[i] = b[i + 1];
                a[i + 1] = b[i];
            }
            return a;

        }

    }
}
