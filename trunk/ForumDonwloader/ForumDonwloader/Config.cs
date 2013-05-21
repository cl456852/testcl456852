using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using System.Configuration;
using System.Reflection;

namespace ForumDonwloader
{
    public class Config
    {
        private static volatile AbstractFactory factory;
		
		/// <summary>
		/// 辅助锁对象，本身没有意义 
		/// </summary>
		private static object syncRoot = new Object();

		/// <summary>
		/// 构造方法改为Private
		/// </summary>
        private Config()
		{
			
		}

        public static AbstractFactory Factory
		{
			get 
			{
                if (factory == null) 
				{
					lock (syncRoot) 
					{
                        if (factory == null)
                            factory = (AbstractFactory)Assembly.Load(ConfigurationSettings.AppSettings["assemblyName"]).CreateInstance(ConfigurationSettings.AppSettings["factoryName"]); 
					}
				}

                return factory;
			}
		}
    }
}
