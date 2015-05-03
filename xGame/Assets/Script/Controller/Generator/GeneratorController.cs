using UnityEngine;
using System;
using System.Collections;
using General.GeneralInterface;
using Common.Model.Item;
using System.Collections.Generic;
using Controller.GameData;

namespace Controller.Generator
{
	public class GeneratorController : IController<GeneratorItem>, IDisposable
	{
		#region IDisposable implementation

		public void Dispose ()
		{
			//throw new NotImplementedException ();
			Context = null;
		}

		#endregion

		#region IGenerator implementation


		public GeneratorItem Context
		{
			get { return m_context;}
			set
			{
				if(null != m_context)
				{
					_OnRemoveContext();
					m_context = null;
				}

				m_context = value;
				if(null != m_context)
				{
					_OnSetContext();
				}
			}
		}

		#endregion

		public void Generate()
		{
			//Debug.Log (m_listGenerators.Count);
			for (int iIndex = 0; iIndex < m_listGenerators.Count; iIndex++) 
			{
				_ProcessSingleGenerate(m_listGenerators[iIndex]);
			}
		}

		#region Private Functions

		protected virtual void _ProcessSingleGenerate(BaseGenerator generator)
		{
			float fGen = 0f;

			float consumeItemValue = GameDataManager.Instance.GetResourceItemValue (generator.ComsumeID);
			float consumeOut = 0;
			fGen = generator.Generate (consumeItemValue, out consumeOut);
			consumeOut *= -1;
			GameDataManager.Instance.AddValueToResourceItem (generator.ComsumeID, consumeOut);
			GameDataManager.Instance.AddValueToResourceItem (generator.GenerateID, fGen);			
		}

		protected virtual void _OnSetContext()
		{
			BaseGenerator newGen = new BaseGenerator (m_context.ConsumablesID, m_context.GeneratResID1, m_context.Probability1, m_context.GeneratResCount1, m_context.ConsumablesCount1);
			m_listGenerators.Add (newGen);

			newGen = new BaseGenerator (m_context.ConsumablesID2, m_context.GeneratResID2, m_context.Probability2, m_context.GeneratResCount2, m_context.ConsumablesCount2);
			m_listGenerators.Add (newGen);
		}

		protected virtual void _OnRemoveContext()
		{
			m_listGenerators.Clear ();
		}

		#endregion

		#region Members

		private GeneratorItem m_context = default(GeneratorItem);
		private List<BaseGenerator> m_listGenerators = new List<BaseGenerator>();

		#endregion
	}

	public class BaseGenerator : IGenerator
	{
		protected BaseGenerator()
		{
				
		}

		public BaseGenerator(int ConsumeID, int GenID,float fProb, float fGenCount, float fConsumeCount)
		{
			m_iConsumeID = ConsumeID;
			m_iGenID = GenID;
			m_fProb = fProb;
			m_fGenCount = fGenCount;
			m_fConsumeCount = fConsumeCount;
		}

		public int ComsumeID
		{
			get { return m_iConsumeID;}
		}

		public int GenerateID
		{
			get { return m_iGenID;}
		}

		#region IGenerator implementation

		public float Generate(float input, out float consume)
		{	
			consume = 0f;
			//Debug.Log (string.Format ("inpu {0} , prob {1} gen {2}", input, m_fProb, m_fGenCount));
			if (input < m_fConsumeCount) 
			{
				//Debug.Log(1111);
				return 0f;
			}

			float Rate = UnityEngine.Random.Range(0.0f, 1.0f);
			if (Rate <= m_fProb) 
			{
				consume = m_fConsumeCount;

				return m_fGenCount;
			}

			return 0f;
		}

		#endregion

		#region Members

		private int m_iConsumeID = 0;
		private int m_iGenID = 0;
		private float m_fProb = 0.0f;
		private float m_fGenCount = 0;
		private float m_fConsumeCount = 0; 

		#endregion 


	}
}
