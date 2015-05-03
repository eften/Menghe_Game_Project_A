using com.CommonUtility.Common;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MiniJSON;

namespace Common.Model.Item
{
	public class ItemDataManager : TSingleton<ItemDataManager>, IDisposable
	{
		#region IDisposable implementation

		void IDisposable.Dispose ()
		{
			if (null != m_ResItemList) 
			{
				m_ResItemList.Dispose();
				m_ResItemList = null;
			}

			if (null != m_GenItemList) 
			{
				m_GenItemList.Dispose();
				m_GenItemList = null;
			}
		}

		public void Init()
		{
			_Init ();
		}

		public ResourceItem GetResourceItem(int ID)
		{
			return m_ResItemList.GetByKey (ID.ToString ());		
		}

		public GeneratorItem GetGeneratorItem(int ID)
		{
			return m_GenItemList.GetByKey (ID.ToString ());		
		}

		#endregion

		#region Private Functions

		private void _Init()
		{
			string content = _LoadTextFromFile("Datas/Config/Resourceconfig");//Generatorconfig

			if(!string.IsNullOrEmpty(content))
			{
				m_ResItemList = new TBaseItemJsonDB<ResourceItem>("ID", content);
			}

			content = _LoadTextFromFile("Datas/Config/Generatorconfig");
			if(!string.IsNullOrEmpty(content))
			{
				m_GenItemList = new TBaseItemJsonDB<GeneratorItem>("GeneratorID", content);
			}
		}

		private string _LoadTextFromFile(string fileName)
		{

			TextAsset asset = ResourceManager.Instance.LoadResource (fileName) as TextAsset;
			if (null == asset) 
			{
				Debug.LogError("Failed to find file " + fileName);
				return string.Empty;
			}

			
			return asset.text;
		}

		#endregion 

		#region Memeber

		public TBaseItemJsonDB<ResourceItem> m_ResItemList = null;
		public TBaseItemJsonDB<GeneratorItem> m_GenItemList = null;

		#endregion 

	}


}