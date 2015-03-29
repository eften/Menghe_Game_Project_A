using UnityEngine;
using System.Collections;

public class MonoResGenViewer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		m_controller = GetComponent<MonoResGenController> ();
		if (null == m_controller) {
		
			enabled = false;

			return;
		}

		m_controller.AddCurrentCountChangeListener (_OnCurChanged);
		m_controller.AddTotalCountChangeListener (_OnTotalChanged);
	}
	
	void OnGUI()
	{
		if (null == m_controller) {
		
			return;
		}

		GUILayout.BeginVertical ();

		if (GUILayout.Button ("Collect")) {
			
			m_iCollect += m_controller.CollectCurrent();
		}

		if (GUILayout.Button ("Add")) {
		
			m_controller.AddToCurrent(1.0f);
		}

		GUILayout.Label(string.Format("context cur {0} total {1} collect {2}", m_iCurGen, m_iTotalGen, m_iCollect));



		GUILayout.EndVertical ();
	}

	private void _OnCurChanged(int iCur, int iLast)
	{
		//Debug.Log (iCur);
		m_iCurGen = iCur;
	}

	private void _OnTotalChanged(int iCur, int iLast)
	{
		m_iTotalGen = iCur;
	}

	private MonoResGenController m_controller = null;
	private int m_iCurGen = 0;
	private int m_iTotalGen = 0;
	private int m_iCollect = 0;
}
