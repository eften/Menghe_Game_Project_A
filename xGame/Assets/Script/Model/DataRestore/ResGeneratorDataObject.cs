using UnityEngine;
using System.Collections.Generic;

public class ResGeneratorDataObject : ScriptableObject {

	#region Public Functions

	public BaseResGenerator GetGeneratorByID(int iID)
	{
		return m_listDatas.Find (cur => {
						return cur.ID == iID;});
	}

	public bool HasGenerator(int iID)
	{
		return GetGeneratorByID (iID) != null;
	}


	#endregion

	#region Member
	[SerializeField]
	public List<BaseResGenerator> m_listDatas = new List<BaseResGenerator>();

	#endregion 
}
