using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
namespace Lxt2.Communication.Framework.Util
{
    /// <summary>
    /// 阻塞Hashtable
    /// </summary>
    public class BlockHashtable : Dictionary<long,object>
    {
        public readonly int SizeLimit = 0;
        private ManualResetEvent _add_wait = null;
        private ManualResetEvent _remove_wait = null;
        /// <summary>
        /// 入数量
        /// </summary>
        public long input = 0;
        /// <summary>
        /// 出数量
        /// </summary>
        public long output = 0;
        /// <summary>
        /// 是否处于阻塞
        /// </summary>
        private Boolean isBlock = false;

        /// <summary>
        /// 是否阻塞
        /// </summary>
        public Boolean IsBlock
        {
            get { return isBlock; }
            set { isBlock = value; }
        }
        private object o = new object();

        public BlockHashtable(int sizeLimit)
        {
            this.SizeLimit = sizeLimit;
            this._add_wait = new ManualResetEvent(false);
            this._remove_wait = new ManualResetEvent(false);
        }
        public void Add(long key, InnerPackage value)
        {
            try
            {
                while (true)
                {
                    lock (o)
                    {
                        if (this.Count < this.SizeLimit)
                        {
                            base.Add(key, value);
                            Interlocked.Increment(ref input);
                            this._add_wait.Reset();
                            break;
                        }
                    }
                    this.isBlock = true;
                    this._add_wait.WaitOne();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(long key)
        {
            try
            {
                lock (o)
                {
                    if (this.Count > 0)
                    {
                        if (this.ContainsKey(key))
                        {
                            base.Remove(key);
                            Interlocked.Increment(ref output);
                            this.isBlock = false;
                            this._add_wait.Set();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<IPackage> RemoveAll()
        {
            List<IPackage> removeList = new List<IPackage>();
            lock (o)
            {
                //遍历滑动窗口，找到过期和重发的数据
                foreach (KeyValuePair<long,object> de in this)
                {
                    InnerPackage temp = (InnerPackage)de.Value;
                    removeList.Add(temp.Package);
                }
                base.Clear();
                this._add_wait.Set();
            }
            return removeList;
        }
        public IPackage Get(long key)
        {
            try
            {
                return ((InnerPackage)this[key]).Package;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
