using UnityEngine;
using System.Collections;

public class BaseResItem : BaseItem
{
	[SerializeField]
	private int m_iType = 0;
	public int ResType
	{
		get { return m_iType;}
		set { m_iType = value;}
	}
}
