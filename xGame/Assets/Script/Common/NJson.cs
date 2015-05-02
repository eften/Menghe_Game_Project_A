#if UNITY_EDITOR
using UnityEngine;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using JsonFx;

public class NJson {
	
	public static string Encode(object p_Object)
	{
		try
		{
			return JsonFx.Json.JsonWriter.Serialize( p_Object );
		}
		catch (Exception e)
		{
#if UNITY_EDITOR			
			Debug.Log( string.Format("Error Json Encode : {0}", e.Message) );	
#else
			System.Console.Out.Write(e.Message);			
#endif
		} 
		return "";		
	}

	public static IEnumerator LateEncode(object p_Object,System.Action<string> EndAction)
	{
		string ResultStr = "";
		ResultStr = JsonFx.Json.JsonWriter.Serialize( p_Object );
		yield return ResultStr;
		if (EndAction != null)EndAction (ResultStr);
	}
	
	public static T Decode<T>(string p_Value)
	{
		try
		{
			return JsonFx.Json.JsonReader.Deserialize<T>( p_Value );
		}
		catch (Exception e)
		{
#if UNITY_EDITOR						
			Debug.Log( string.Format("Error Json Decode : {0}", e.Message) );	
#else
			System.Console.Out.Write(e.Message);			
#endif
		}
		
		return default(T);
	}

	public static IEnumerator LateDecode<T>(string p_Value,System.Action<T> EndAction)
	{
		T TempT = default(T);

		TempT = JsonFx.Json.JsonReader.Deserialize<T>( p_Value );
		yield return TempT;

		if (EndAction != null)EndAction (TempT);
	}

	public static object Decode(string p_Value, Type p_Type)
	{
		try
		{
			return JsonFx.Json.JsonReader.Deserialize( p_Value, p_Type );
		}
		catch (Exception e)
		{
#if UNITY_EDITOR						
			Debug.Log( string.Format("Error Json Decode : {0}", e.Message) );	
#else
			System.Console.Out.Write(e.Message);
#endif			
		}
		return null;
	}
}

