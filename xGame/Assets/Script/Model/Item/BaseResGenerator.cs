using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseResGenerator : BaseItem
{
	[SerializeField]
	private int m_iResID = 0;
	public int ResID
	{
		get {return m_iResID;}
		set {m_iResID = value;}
	}
	
	[SerializeField]
	private float m_fProbality = 1.0f;
	public float Probality
	{
		get {return m_fProbality;}
		set { m_fProbality = value;}
	}
	
	[SerializeField]
	private float m_fProduction = 0.1f;	// procudtion per second
	public float Production
	{
		get {return m_fProduction;}
		set {m_fProduction = value;}
	}
}
