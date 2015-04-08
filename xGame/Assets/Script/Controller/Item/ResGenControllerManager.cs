#region 版本注释
/*
 * ========================================================================
 * Copyright(c) 和萌, All Rights Reserved.
 * ========================================================================
 * 说明 ：
 * 
 * 创建日期：2015-4-6 21:40:22
 * 文件名：ResGenControllerManager
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
using com.CommonUtility.Common;

public class ResGenControllerManager : TSingleton<ResGenControllerManager> , IDisposable
{
    #region Public Functions

    public void Dispose()
    {
        foreach (var item in m_dicControllers)
        {
            item.Value.Dispose();
        }

        m_dicControllers.Clear();
    }

    public SingleRssGenControllerMgr GetGenControllerMgr(int GenID)
    {
        SingleRssGenControllerMgr mgr = null;

        m_dicControllers.TryGetValue(GenID, out mgr);

        return mgr;
    }

    //public double Generate(float fElapseTime)
    //{
    //    foreach (var item in m_dicControllers)
    //    {
    //        item.Value.Generate(fElapseTime);
    //    }
    //}

    #endregion

    #region Member

    private Dictionary<int, SingleRssGenControllerMgr> m_dicControllers = new Dictionary<int, SingleRssGenControllerMgr>();

    #endregion

    
}
