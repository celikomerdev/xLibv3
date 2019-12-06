#if xLibv3
using System.Collections.Generic;
using UnityEngine;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ObjectGroup
	{
		internal int indexCurrent = 0;
		internal List<ISerializableObject> iSerializableObject = new List<ISerializableObject>();
		internal List<ICall> iCall = new List<ICall>();
		
		[Header("ISerializableObject")]
		[SerializeField]private Group[] group = new Group[0];
		[System.Serializable]private class Group
		{
			public string info = "Info";
			[SerializeField]internal Object[] arrayObject = new Object[0];
		}
		
		#region Init
		private bool isInit;
		internal void Init(bool value)
		{
			if(isInit == value) return;
			isInit = value;
			
			indexCurrent = 0;
			for (int i = 0; i < group.Length; i++)
			{
				iSerializableObject.AddRange(group[i].arrayObject.GetGenericsArray<ISerializableObject>());
				iCall.AddRange(group[i].arrayObject.GetGenericsArray<ICall>());
			}
		}
		#endregion
		
		
		#region Database
		public ISerializableObject GetByIndex(int value)
		{
			indexCurrent = Mathx.MathInt.Repeat(value,iSerializableObject.Count);
			return iSerializableObject[indexCurrent];
		}
		
		public ISerializableObject GetByOrder(int value)
		{
			return GetByIndex(indexCurrent+value);
		}
		
		public ISerializableObject GetByRandom()
		{
			return GetByIndex(UnityEngine.Random.Range(0,iSerializableObject.Count));
		}
		
		public ISerializableObject GetByKey(string value)
		{
			indexCurrent = -1;
			if(iSerializableObject!=null && iSerializableObject.Count>0)
			{
				try
				{
					indexCurrent = iSerializableObject.FindIndex(asset => ((ISerializableObject)asset).Key == value);
				}
				catch (System.Exception ex)
				{
					xDebug.LogExceptionFormat("GetByKey:{0}",ex);
				}
			}
			if(indexCurrent==-1) indexCurrent=0;
			return GetByIndex(indexCurrent);
		}
		#endregion
		
		
		#region ISerializableBaseContext
		#if UNITY_EDITOR
		public void ChildKeyName()
		{
			Init(true);
			for (int i = 0; i < iSerializableObject.Count; i++)
			{
				iSerializableObject[i].KeyName();
			}
		}
		
		public void ChildKeyGuid()
		{
			Init(true);
			for (int i = 0; i < iSerializableObject.Count; i++)
			{
				iSerializableObject[i].KeyGuid();
			}
		}
		#endif
		#endregion
	}
}
#endif