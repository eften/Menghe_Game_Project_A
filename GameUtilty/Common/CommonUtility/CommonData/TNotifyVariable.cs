#region 版本注释
/*
 * ========================================================================
 * Copyright(c) 和萌, All Rights Reserved.
 * ========================================================================
 * 说明 ：
 * 
 * 创建日期：2015-3-7 19:13:52
 * 文件名：TNotifyVariable
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

namespace com.CommonUtility.CommonData
{
    public class TNotifyVariable<T> : IDisposable
        where T : IComparable 
    {

        public delegate void VarChangeNotify(T iCur, T iLast);
        
        /// <summary>
        /// constructor with default value
        /// </summary>
        /// <param name="defalutValue"></param>
        public TNotifyVariable(T defalutValue)
        {
            m_iCurrent = defalutValue;
        }

        public void Dispose()
        {
            RemoveAllCallback();
        } 

        /// <summary>
        /// set and get current value
        /// </summary>
        public T Value
        {
            get
            {
                return m_iCurrent;
            }
            set
            {
                if (m_iCurrent.CompareTo(value) != 0)
                {
                    m_iLast = m_iCurrent;
                    m_iCurrent = value;

                    if (null != m_callback)
                    {
                        m_callback.Invoke(m_iCurrent, m_iLast);
                    }
                }
            }
        }

        /// <summary>
        /// add variable change callback
        /// </summary>
        /// <param name="callback">function</param>
        public void AddVarChangeCallback(VarChangeNotify callback)
        {
            if(null == m_callback)
            {
                m_callback = callback;

                return;
            }

            m_callback += callback;
        }

        /// <summary>
        /// remove variable change call back
        /// </summary>
        /// <param name="callback">function</param>
        public void RemoveVarChangeCallback(VarChangeNotify callback)
        {
            if(null != m_callback)
            {
                m_callback -= callback;
            }
        }

        /// <summary>
        /// remove all variable change call back
        /// </summary>
        public void RemoveAllCallback()
        {
            m_callback = null;
        }

        private T m_iCurrent;
        private T m_iLast;
        private VarChangeNotify m_callback = null;
    

    }
}
