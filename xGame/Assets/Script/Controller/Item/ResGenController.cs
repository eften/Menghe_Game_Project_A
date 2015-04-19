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

	public double TotalGen
	{
		get { return m_totalGen.Value;}
	}

	public double CurrentGen
	{
		get { return m_CurGen.Value;}
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
		m_totalGen.Dispose ();
		m_CurGen.Dispose ();
    }


    public void AddGenCountChangedListener(TNotifyVariable<int>.VarChangeNotify callback)
    {
        m_iGenCount.AddVarChangeCallback(callback);
    }

    public void RemoGenCountChangedListener(TNotifyVariable<int>.VarChangeNotify callback)
    {
        m_iGenCount.RemoveVarChangeCallback(callback);
    }

	public void AddTotalGenChangedListener(TNotifyVariable<double>.VarChangeNotify callback)
	{
		m_totalGen.AddVarChangeCallback (callback);
	}

	public void RemvoeTotalGenChangedListner(TNotifyVariable<double>.VarChangeNotify callback)
	{
		m_totalGen.RemoveVarChangeCallback (callback);
	}

	public void AddCurGenChangedListener(TNotifyVariable<double>.VarChangeNotify callback)
	{
		m_CurGen.AddVarChangeCallback(callback);
	}
	
	public void RemvoeCurGenChangedListner(TNotifyVariable<double>.VarChangeNotify callback)
	{
		m_CurGen.RemoveVarChangeCallback(callback);
	}

	public double CollectCurrent()
	{
		double fValue = m_CurGen.Value;
		m_CurGen.Value = 0;

		return fValue;
	}

    public void AddContronller()
    {
        ResGenController newController = new ResGenController();
        newController.SetContext(m_context);

        m_listControllers.Add(newController);

        m_iGenCount.Value = m_listControllers.Count;
    }

	public double Update(float fElapseTime)
	{
		m_fTempValue += _Generate (fElapseTime);
		m_totalGen.Value += m_fTempValue;
		m_CurGen.Value += m_fTempValue;

		return m_fTempValue;
	}

	#region Protected Functions
    protected double _Generate(float fElapseTime)
    {
        double fGen = 0.0f;
        for (int iIndex = 0; iIndex < m_listControllers.Count; iIndex++)
        {
            fGen += m_listControllers[iIndex].Generate(fElapseTime);
        }

        return fGen;
    }
	#endregion

    #region Members

    private BaseResGenerator m_context = default(BaseResGenerator);

    private List<ResGenController> m_listControllers = new List<ResGenController>();

    private TNotifyVariable<int> m_iGenCount = new TNotifyVariable<int>(0);

	private TNotifyVariable<double> m_totalGen = new TNotifyVariable<double> (0.0f);

	private TNotifyVariable<double> m_CurGen = new TNotifyVariable<double> (0.0f);
	

	private double m_fTempValue = 0.0f;
    //private int 

    #endregion
}
