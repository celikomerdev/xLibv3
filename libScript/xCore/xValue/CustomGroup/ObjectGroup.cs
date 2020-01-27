#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ObjectGroup
	{
		internal int indexCurrent = 0;
		internal ISerializableObject[] iSerializableObject = new ISerializableObject[0];
		internal ICall[] iCall = new ICall[0];
		
		[Header("ISerializableObject")]
		[SerializeField]private Object[] arrayObject = new Object[0];
		public Object[] ArrayObject
		{
			get
			{
				return arrayObject;
			}
		}
		
		
		#region Init
		private bool isInit;
		internal void Init(bool value)
		{
			if(isInit == value) return;
			isInit = value;
			
			indexCurrent = 0;
			iSerializableObject = arrayObject.GetGenericsArray<ISerializableObject>();
			iCall = arrayObject.GetGenericsArray<ICall>();
		}
		#endregion
		
		
		#region Database
		public ISerializableObject GetByIndex(int value)
		{
			indexCurrent = Mathx.MathInt.Repeat(value,iSerializableObject.Length);
			return iSerializableObject[indexCurrent];
		}
		
		public ISerializableObject GetByOrder(int value)
		{
			return GetByIndex(indexCurrent+value);
		}
		
		public ISerializableObject GetByRandom()
		{
			return GetByIndex(UnityEngine.Random.Range(0,iSerializableObject.Length));
		}
		
		public ISerializableObject GetByKey(string value)
		{
			indexCurrent = -1;
			if(iSerializableObject!=null && iSerializableObject.Length>0)
			{
				try
				{
					indexCurrent = iSerializableObject.FindIndex(asset => ((ISerializableObject)asset).Key == value);
				}
				catch (System.Exception ex)
				{
					xLogger.LogException($"GetByKey:{ex.Message}");
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
			for (int i = 0; i < iSerializableObject.Length; i++)
			{
				iSerializableObject[i].KeyName();
			}
		}
		
		public void ChildKeyGuid()
		{
			Init(true);
			for (int i = 0; i < iSerializableObject.Length; i++)
			{
				iSerializableObject[i].KeyGuid();
			}
		}
		#endif
		#endregion
	}
}
#endif