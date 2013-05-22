using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
namespace Lxt2.Communication.Framework.Util
{
    /// <summary>
    /// 阻塞Dictionary
    /// </summary>
    public class BlockDictionary<T, V> : Dictionary<T, V>
    {
        public readonly int SizeLimit = 0;
        private ManualResetEvent _add_wait = null;
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
        public BlockDictionary(int sizeLimit)
        {
            this.SizeLimit = sizeLimit;
            this._add_wait = new ManualResetEvent(false);
        }
        public void Add(T key, V value)
        {
            try
            {
                while (true)
                {
                    lock (this)
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
        public V Remove(T key)
        {
            try
            {
                lock (this)
                {
                    if (this.Count > 0)
                    {
                        V value;
                        if (base.TryGetValue(key, out value))
                        {
                            base.Remove(key);
                            Interlocked.Increment(ref output);
                            this.isBlock = false;
                            this._add_wait.Set();
                            return value;
                        }
                    }
                }
                return default(V);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<V> RemoveAll()
        {
            List<V> removeList = new List<V>();
            lock (this)
            {
                //遍历滑动窗口，找到过期和重发的数据
                foreach (KeyValuePair<T, V> thing in this)
                {
                    removeList.Add(thing.Value);
                }
                base.Clear();
                this._add_wait.Set();
            }
            return removeList;
        }
        public V Get(T key)
        {
            try
            {
                V value;
                this.TryGetValue(key, out value);
                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<V> GetAll()
        {
            List<V> list = new List<V>();
            lock (this)
            {
                //遍历滑动窗口，找到过期和重发的数据
                foreach (KeyValuePair<T, V> thing in this)
                {
                    list.Add(thing.Value);
                }
            }
            return list;
        }
    }
}
