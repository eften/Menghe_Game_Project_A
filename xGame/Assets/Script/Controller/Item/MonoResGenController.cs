using UnityEngine;
using System.Collections;
using Util;

public class MonoResGenController : MonoBehaviour {

	public int ResID = 0;
	public string ResName = string.Empty;
	public string ResDes = string.Empty;
	public string ResIcon = string.Empty;
	public float Probality = 1.0f;
	public float Production = 0.5f;


	public BaseResGenerator Context
	{
		get {return m_context;}
		set 
		{
			if(null != m_context)
			{
				_onRemoveContext();
				m_context = null;
			}

			m_context = value;
			if(null != m_context)
			{
				_onSetContext();
			}
		}
	}

	public int CollectCurrent()
	{
		int iCur = m_iCurGen.Value;

		m_iCurGen.Value = 0;
		m_fCurGen = 0.0f;
		return iCur;
	}

	public void AddToCurrent(float fAdd)
	{
		m_fCurGen += fAdd;
	}

	public void AddTotalCountChangeListener(TVariableNotifier<int>.TVariableCallback callback)
	{
		m_iTotalGen.AddVariableChangeCallback (callback);
	}

	public void RemoveTotalCountChangeListener(TVariableNotifier<int>.TVariableCallback callback)
	{
		m_iTotalGen.RemoveVariableChangeCallback (callback);
	}

	public void AddCurrentCountChangeListener(TVariableNotifier<int>.TVariableCallback callback)
	{
		m_iCurGen.AddVariableChangeCallback (callback);
	}

	public void RemoveCurrentCountChangeListener(TVariableNotifier<int>.TVariableCallback callback)
	{
		m_iCurGen.RemoveVariableChangeCallback (callback);
	}

	void Awake()
	{
		BaseResGenerator context = new BaseResGenerator ();
		context.ID = ResID;
		context.Name = ResName;
		context.Description = ResDes;
		context.Icon = ResIcon;
		context.Probality = Probality;
		context.Production = Production;

		Context = context;

		m_iCurGen.AddVariableChangeCallback (_OnCurrentChanged);
	}


	void FixedUpdate () {

		if (null == m_context) {
		
			return;
		}

		_UpdateGenerate (Time.deltaTime);
	}

	void OnDestroy()
	{
		m_iCurGen.Dispose ();
		m_iTotalGen.Dispose ();
	}

	private void _onSetContext()
	{
	}

	private void _onRemoveContext()
	{
		m_iCurGen.Value = 0;
		m_iTotalGen.Value = 0;
		m_fCurGen = 0.0f;
	}

	protected virtual void _UpdateGenerate(float fElapseTime)
	{
		m_fRate = Random.Range (0.0f, 1.0f);
		if (m_fRate <= m_context.Probality) 
		{
			//Debug.Log(fElapseTime);
			m_fGrowPerSecond = fElapseTime * m_context.Production;
			m_fCurGen += m_fGrowPerSecond;
			m_iCurGen.Value = (int)(m_fCurGen);
			//m_iTotalGen.Value += Mathf.Max(0, (m_iCurGen.Value - m_iCurGen.Last));
		}

	}

	private void _OnCurrentChanged(int Cur, int Last)
	{
		m_iTotalGen.Value += Mathf.Max(0,  Cur - Last);
	}


	private float m_fGrowPerSecond = 0;
	private float m_fRate = 0.0f;
	private BaseResGenerator m_context = default(BaseResGenerator);
	private float m_fCurGen = 0.0f;
	private TVariableNotifier<int> m_iTotalGen = new TVariableNotifier<int> (0);
	private TVariableNotifier<int> m_iCurGen = new TVariableNotifier<int> (0);
}
