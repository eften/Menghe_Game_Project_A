using UnityEngine;
using System.Collections;

public class BaseItem 
{
	#region Members
	[SerializeField]
	private int m_iID = 0;
	public int ID
	{
		get { return m_iID;}
		set { m_iID = value;}
	}

	[SerializeField]
	private string m_strName = string.Empty;
	public string Name
	{
		get {return m_strName;}
		set { m_strName = value;}
	}

	[SerializeField]
	private string m_strDescription = string.Empty;
	public string Description
	{
		get {return m_strDescription;}
		set {m_strDescription = value;}
	}

	[SerializeField]
	private string m_strIcon = string.Empty;
	public string Icon
	{
		get {return m_strIcon;}
		set {m_strIcon = value;}
	}

	#endregion
}
