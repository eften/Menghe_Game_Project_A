/// 
/// 版本          : 1.0
/// 创建者        : 和萌
/// 简介          ：
/// 最后修改者    ：
/// 最后修改时间  ：
///

using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public static class ScriptableObjectHelper
{
	/// <summary>
	/// creat new scriptable object and save to specific path with defined name
	/// </summary>
	/// <typeparam name="T">sriptable object class type</typeparam>
	/// <param name="path">save path</param>
	/// <param name="name">save name</param>
	/// <returns>create result</returns>
	public static T CreateAndSaveScriptableObject<T>(string path, string name)
		where T : ScriptableObject
	{
		
		if (new DirectoryInfo(path).Exists == false)
		{
			Debug.LogError(string.Format("can't create asset, path \"{0}\" not found ", path));
			return null;
		}
		
		if (string.IsNullOrEmpty(name))
		{
			Debug.LogError("can't create asset, the name is empty");
			return null;
		}
		
		string assetPath = Path.Combine(path, name + ".asset");
		
		T newT = ScriptableObject.CreateInstance<T>();
		
		if (null == newT)
		{
			Debug.LogError(string.Format("can't create asset {0} , create {1} instance failed", name, typeof(T).Name));                
		}
		
		try
		{
			AssetDatabase.CreateAsset(newT, assetPath);
			return newT;
		}
		catch (Exception e)
		{
			Debug.LogError(string.Format("can't create asset {0} , exception {1}", name, e.ToString()));
			return null;
		}
		
	}       
	
	/// <summary>
	/// quick create and save scriptable object 
	/// </summary>
	/// <typeparam name="T">ScriptalbeObject class type</typeparam>
	public static T QuickCreateAndSaveScriptableObject<T>()
		where T : ScriptableObject
	{
		#region Creat New Data Object
		string assetName = typeof(T).Name;
		string assetPath = "Assets";
		if (Selection.activeObject)
		{
			assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			if (Path.GetExtension(assetPath) != string.Empty)
			{
				assetPath = Path.GetDirectoryName(assetPath);
			}
		}
		bool doCreate = true;
		string path = Path.Combine(assetPath, assetName + ".asset");
		FileInfo fileInfo = new FileInfo(path);
		if (fileInfo.Exists)
		{
			doCreate = EditorUtility.DisplayDialog(string.Format("{0} already exist", assetName), "replace file ?", "Yes", "No");
		}
		
		if (doCreate)
		{
			T newData = CreateAndSaveScriptableObject<T>(assetPath, assetName);
			if (newData != null)
			{
				Selection.activeObject = newData;
				EditorUtility.SetDirty(newData);
				
				return newData;
			}
		}
		
		#endregion
		
		return default(T);
	}
	
	/// <summary>
	/// 绘制创建器
	/// </summary>
	/// <typeparam name="T">目标类</typeparam>
	/// <param name="WidgetColor">颜色</param>
	public static void DrawDataObjectCreatorWidget<T>(Color WidgetColor)
		where T : ScriptableObject
	{
		GUILayout.BeginVertical("box");
		
		var org = GUI.backgroundColor;
		GUI.backgroundColor = WidgetColor;
		
		if (GUILayout.Button("Create new data"))
		{
			#region Creat New Data Object
			string assetName = typeof(T).Name;
			string assetPath = "Assets";
			if (Selection.activeObject)
			{
				assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
				if (Path.GetExtension(assetPath) != string.Empty)
				{
					assetPath = Path.GetDirectoryName(assetPath);
				}
			}
			bool doCreate = true;
			string path = Path.Combine(assetPath, assetName + ".asset");
			FileInfo fileInfo = new FileInfo(path);
			if (fileInfo.Exists)
			{
				doCreate = EditorUtility.DisplayDialog(string.Format("{0} already exist", assetName), "replace file ?", "Yes", "No");
			}
			
			if (doCreate)
			{
				T newData = CreateAndSaveScriptableObject<T>(assetPath, assetName);
				if (newData != null)
				{
					Selection.activeObject = newData;
					EditorUtility.SetDirty(newData);
				}
			}
			
			#endregion
		}
		
		GUI.backgroundColor = org;
		
		GUILayout.EndVertical();
	}
	
	
}
