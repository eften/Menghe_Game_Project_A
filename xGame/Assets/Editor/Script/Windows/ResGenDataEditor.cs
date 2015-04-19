using UnityEngine;
using System.Collections;
using UnityEditor;

public class ResGenDataEditor : EditorWindow {

	#region WindowDefine
	
	[MenuItem(@"Window/xGameTools/ResourceGenDataEditor")]
	static void Init()
	{
		m_sWindow = (ResGenDataEditor)EditorWindow.GetWindow(typeof(ResGenDataEditor));
		
		m_sWindow.Show();
	}
	
	[MenuItem("Assets/Create/ResourceGeneratorData")]
	[MenuItem("GameObject/Create Other/ResourceGeneratorData")]
	public static void CreateSoundDataGroup()
	{
		ScriptableObjectHelper.QuickCreateAndSaveScriptableObject<ResGeneratorDataObject>();
	}
	
	#endregion 

	#region Editor Field

	void OnGUI()
	{
		_UpdateSelection ();

		if (null == m_curData) 
		{
			return;
		}

		m_curData.DrawItemCreator ();

		m_scrollPos = EditorGUILayout.BeginScrollView (m_scrollPos);
		for (int iIndex = 0; iIndex < m_curData.m_listDatas.Count; iIndex ++) {
		
			m_curData.m_listDatas[iIndex].DrawItem();
		}

		EditorGUILayout.EndScrollView ();
	}

	private void _UpdateSelection()
	{
		
		ResGeneratorDataObject tmp = (ResGeneratorDataObject)EditorGUILayout.ObjectField("current data", m_curData, typeof(ResGeneratorDataObject), false);
		
		if (tmp != null && tmp != m_curData)
		{
			m_curData = tmp;

			Repaint();
		}
	}

	private void _Reload()
	{
		if (m_curData != null)
		{
			Repaint();
		}
	}
	
	private void _Save()
	{
		if (m_curData != null)
		{
			EditorUtility.SetDirty(m_curData);  
		}
	}

	#endregion

	#region Members

	private static ResGenDataEditor m_sWindow;
	private ResGeneratorDataObject m_curData = null;
	private Vector2 m_scrollPos = new Vector2 ();

	#endregion

}

public static class ResGenDataDrawer
{
	public static void DrawItem(this BaseResGenerator item)
	{
		if (null == item) 
		{
			return;
		}

		EditorGUILayout.BeginVertical("box");

		EditorGUILayout.LabelField ("ID", item.ID.ToString());
		item.Name = EditorGUILayout.TextField ("Name", item.Name);
		item.Description = EditorGUILayout.TextField ("Description", item.Description);
		item.Icon = EditorGUILayout.TextField ("Icon", item.Icon);
		item.ResID = EditorGUILayout.IntField ("ResourceID", item.ResID);
		item.Probality = (float)EditorGUILayout.DoubleField ("Probality", item.Probality);
		item.Production = (float)EditorGUILayout.DoubleField ("Production", item.Production);

		EditorGUILayout.EndVertical ();
	}

	public static void DrawItemCreator(this ResGeneratorDataObject data)
	{
		if(null == data)
		{
			return ;
		}

		EditorGUILayout.BeginVertical("box");

		EditorGUILayout.LabelField("Create new Data");

		m_iCurrentID = EditorGUILayout.IntField ("ID", m_iCurrentID);
		m_strName = EditorGUILayout.TextField ("Name", m_strName);
		m_strDes = EditorGUILayout.TextField ("Description", m_strDes);
		m_strIcon = EditorGUILayout.TextField ("Icon", m_strIcon);
		m_iResID = EditorGUILayout.IntField ("ResourceID", m_iResID);
		m_fProb = (float)EditorGUILayout.DoubleField ("Probality", m_fProb);
		m_fProduction = (float)EditorGUILayout.DoubleField ("Production", m_fProduction);

		if (data.HasGenerator (m_iCurrentID)) 
		{
			EditorGUILayout.LabelField("ID already exist!");
		}
		else
		{
			if(GUILayout.Button("Create"))
			{
				BaseResGenerator item = new BaseResGenerator();
				item.ID = m_iCurrentID;
				item.Name = m_strName;
				item.Description = m_strDes;
				item.Icon = m_strIcon;
				item.ResID = m_iResID;
				item.Probality = m_fProb;
				item.Production = m_fProduction;

				data.m_listDatas.Add(item);
				m_iCurrentID++;

				EditorUtility.SetDirty(data);
			}
		}

		EditorGUILayout.EndVertical ();
	}

	#region Members

	private static int m_iCurrentID = 0;
	private static string m_strName = string.Empty;
	private static string m_strDes = string.Empty;
	private static string m_strIcon = string.Empty;
	private static int m_iResID = 0;
	private static float m_fProb = 0.0f;
	private static float m_fProduction = 0.0f;

	#endregion
}