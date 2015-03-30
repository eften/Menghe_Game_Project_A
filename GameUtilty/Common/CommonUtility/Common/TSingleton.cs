#region 版本注释
/*
 * ========================================================================
 * Copyright(c) 和萌, All Rights Reserved.
 * ========================================================================
 * 说明 ：
 * 
 * 创建日期：2015-3-20 22:09:44
 * 文件名：TSingleton
 *  
 * 版本：V1.0.0
 * 
 *  ========================================================================
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.CommonUtility.Common
{
    public class TSingleton<T>
        where T : IDisposable, new()
    {
        public static T Instance
        {
            get 
            {
                if(null == m_instance)
                {
                    m_instance = new T();
                }

                return m_instance;
            }
        }

        private static T m_instance = default(T);
    }
}
