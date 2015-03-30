using UnityEngine;
using System.Collections.Generic;

public abstract class TVariableModifier<T> 
{
	public delegate T AlgorithemFunction(T BaseValue, List<T> listParams);

	public abstract T Calculate(T BaseValue, List<T> listParams);

	public int Sequence
	{
		get {return m_iSequesce;}
		set {m_iSequesce = value;}
	}
	private int m_iSequesce = 0;
}
