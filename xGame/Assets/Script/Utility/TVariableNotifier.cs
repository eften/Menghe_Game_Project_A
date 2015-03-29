#region Author
/// <summary>  
/// Author：meng he  
/// Time：2014/10/11 11:06:06  
/// Company:Microsoft  
/// Version：2014-2014 
/// CLR Version：4.0.30319.18444     
/// UID：6d9a7892-bfac-4ad7-8a10-423ac4b3879d  
/// </summary>  
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class TVariableNotifier<T> : IDisposable
        where T : IComparable
    {

        public delegate void TVariableCallback(T CurrentValue, T LastValue);

        #region Public Functions

        public TVariableNotifier(T defaultValue)
        {
            m_curValue = defaultValue;
        }

        public void Dispose()
        {
            m_changeCallback = null;
            m_curValue = default(T);
        }

        public T Value
        {
            get { return m_curValue; }
            set { _SetValue(value);  }
        }

		public T Last
		{
			get {return m_lastValue;}
		}

        public void AddVariableChangeCallback(TVariableCallback callback)
        {
            if (null == callback)
            {
                return;
            }

            if (null == m_changeCallback)
            {
                m_changeCallback = callback;

				callback.Invoke(m_curValue, m_lastValue);

                return;
            }

            m_changeCallback += callback;
			callback.Invoke(m_curValue, m_lastValue);
			
        }

        public void RemoveVariableChangeCallback(TVariableCallback callback)
        {
            if (null != callback && null != m_changeCallback)
            {
                m_changeCallback -= callback;
            }
        }

        #endregion

        #region Private Functions

        private void _SetValue(T value)
        {
            // fix float variable later     meng.he
            if (m_curValue.CompareTo(value) != 0)
            {
				m_lastValue = m_curValue;
                m_curValue = value;

                if (null != m_changeCallback)
                {
					m_changeCallback.Invoke(m_curValue, m_lastValue);
                }
            }
        }

        #endregion


        #region Members

        private T m_curValue = default(T);
		private T m_lastValue = default(T);
        private TVariableCallback m_changeCallback = null;

        #endregion

        
    }
}
