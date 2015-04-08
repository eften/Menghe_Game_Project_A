#region 版本注释
/*
 * ========================================================================
 * Copyright(c) 和萌, All Rights Reserved.
 * ========================================================================
 * 说明 ：
 * 
 * 创建日期：2015-4-6 21:06:36
 * 文件名：ResGenController
 *  
 * 版本：V1.0.0
 * 
 *  ========================================================================
*/
#endregion

using System;
using System.Collections.Generic;
using com.CommonUtility.CommonData;


public class ResGenController : IDisposable
{

    #region Public Funxtions

    public void SetContext(BaseResGenerator context)
    {
        m_context = context;
    }

    public double Generate(float fElapseTime)
    {
        return _Generate(fElapseTime);
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
        m_context = null;
    }

    #endregion

    #region Private Functions



    private double _Generate(float fElapseTime)
    {
        if (null == m_context)
        {
            return 0.0f;
        }

        double fRate = m_seed.NextDouble();
        if (fRate <= m_context.Probality)
        {
            //Debug.Log(fElapseTime);
            return (fElapseTime * m_context.Production);           
        }

        return 0.0f;
    }



    #endregion

    #region Memebers

    private BaseResGenerator m_context = default(BaseResGenerator);
    private Random m_seed = new Random();
    
    #endregion

   
}

public class SingleRssGenControllerMgr : IDisposable
{
    protected SingleRssGenControllerMgr()
    { }

    public SingleRssGenControllerMgr(BaseResGenerator context)
    {
        m_context = context;
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
        for (int iIndex = 0; iIndex < m_listControllers.Count; iIndex++)
        {
            m_listControllers[iIndex].Dispose();
        }

        m_listControllers.Clear();
        m_context = null;

        m_iGenCount.Dispose();
    }


    public void AddGenCountChangedListener(TNotifyVariable<int>.VarChangeNotify callback)
    {
        m_iGenCount.AddVarChangeCallback(callback);
    }

    public void RemoGenCountChangedListener(TNotifyVariable<int>.VarChangeNotify callback)
    {
        m_iGenCount.RemoveVarChangeCallback(callback);
    }



    public void AddContronller()
    {
        ResGenController newController = new ResGenController();
        newController.SetContext(m_context);

        m_listControllers.Add(newController);

        m_iGenCount.Value = m_listControllers.Count;
    }

    public double Generate(float fElapseTime)
    {
        double fGen = 0.0f;
        for (int iIndex = 0; iIndex < m_listControllers.Count; iIndex++)
        {
            fGen += m_listControllers[iIndex].Generate(fElapseTime);
        }

        return fGen;
    }


    #region Members

    private BaseResGenerator m_context = default(BaseResGenerator);

    private List<ResGenController> m_listControllers = new List<ResGenController>();

    private TNotifyVariable<int> m_iGenCount = new TNotifyVariable<int>(0);

    //private int 

    #endregion
}
