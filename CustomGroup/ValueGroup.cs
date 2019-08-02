#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ValueGroup
	{
		[SerializeField]internal NodeSetting nodeSetting = new NodeSetting();
		
		internal int indexCurrent = 0;
		public ISerializableObject[] iSerializableObject;
		private ICall[] iCall;
		
		[Header("ISerializableObject")]
		[SerializeField]private Object[] arrayObject = new Object[0];
		public Object[] ArrayObject
		{
			get
			{
				return arrayObject;
			}
		}
		
		
		#region SetActive
		private bool isInit;
		internal void Init(bool value)
		{
			if(isInit == value) return;
			isInit = value;
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Init:{0}",isInit);
			Init();
			for (int i = 0; i < iSerializableObject.Length; i++)
			{
				ISerializableObject jsonInterface = iSerializableObject[i];
				jsonInterface.Init(value);
			}
		}
		
		private void Init()
		{
			indexCurrent = 0;
			iSerializableObject = arrayObject.GetGenericsArray<ISerializableObject>();
			iCall = arrayObject.GetGenericsArray<ICall>();
		}
		#endregion
		
		
		#region ISerializableBase
		internal virtual object SerializedObjectRaw
		{
			get
			{
				JObject jObject = new JObject();
				JObject Values = new JObject();
				for (int i = 0; i < iSerializableObject.Length; i++)
				{
					ISerializableObject jsonInterface = iSerializableObject[i];
					Values.Add(jsonInterface.Key,(JToken)jsonInterface.SerializedObjectRaw);
				}
				jObject.Add("Values",Values);
				
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":SerializedObject:Get:{0}:{1}",ViewCore.CurrentId,jObject.ToString());
				return jObject;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":SerializedObject:Set:{0}:{1}",ViewCore.CurrentId,value);
				if(string.IsNullOrEmpty(stringJson)) return;
				
				if(nodeSetting.UseRpc && !ViewCore.inRpc)
				{
					if(ViewCore.IsMy)
					{
						if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Call:{0}",ViewCore.CurrentId);
						ViewCore.RPC(nodeSetting.RpcTarget,nodeSetting.Key,stringJson);
						return;
					}
				}
				
				JObject jObject = JObject.Parse(stringJson);
				JObject Values = (JObject)jObject.GetValue("Values");
				if(Values==null) Values = jObject;
				
				if(Values != null)
				{
					if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Values:true:{0}",ViewCore.CurrentId);
					for (int i = 0; i < iSerializableObject.Length; i++)
					{
						ISerializableObject jsonInterface = iSerializableObject[i];
						
						JToken token = Values.GetValue(jsonInterface.Key);
						if(token==null) token = Values.GetValue(jsonInterface.Name);
						
						jsonInterface.SerializedObjectRaw = token;
					}
				}
			}
		}
		
		internal virtual object SerializedObject
		{
			get
			{
				return SerializedObjectRaw;
			}
			set
			{
				SerializedObjectRaw = value;
			}
		}
		
		internal virtual object SerializedObjectName
		{
			get
			{
				JObject jObject = new JObject();
				for (int i = 0; i < iSerializableObject.Length; i++)
				{
					ISerializableObject jsonInterface = iSerializableObject[i];
					jObject.Add(jsonInterface.Name,(JToken)jsonInterface.SerializedObjectName);
				}
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":SerializedObjectName:Get:{0}:{1}",ViewCore.CurrentId,jObject.ToString());
				return jObject;
			}
		}
		#endregion
		
		
		#region ISerializableBaseContext
		#if UNITY_EDITOR
		internal void ChildKeyName()
		{
			Init();
			for (int i = 0; i < iSerializableObject.Length; i++)
			{
				iSerializableObject[i].KeyName();
			}
		}
		
		internal void ChildKeyGuid()
		{
			Init();
			for (int i = 0; i < iSerializableObject.Length; i++)
			{
				iSerializableObject[i].KeyGuid();
			}
		}
		#endif
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
					xDebug.LogExceptionFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+"GetByKey:{0}",ex);
				}
			}
			if(indexCurrent==-1) indexCurrent=0;
			return GetByIndex(indexCurrent);
		}
		#endregion
		
		
		internal void Call()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Call:{0}",ViewCore.CurrentId);
			ViewCore.RPC(nodeSetting.RpcTarget,nodeSetting.Key,SerializedObjectRaw.ToString());
		}
		
		
		public void ListenerCall(bool register,UnityAction call,bool onRegister=false)
		{
			for (int i = 0; i < iCall.Length; i++)
			{
				iCall[i].ListenerCall(register,call,onRegister);
			}
		}
		
		public void ListenerEditor(bool addition,BaseActiveM call)
		{
			#if UNITY_EDITOR
			for (int i = 0; i < iCall.Length; i++)
			{
				iCall[i].ListenerEditor(addition,call);
			}
			#endif
		}
	}
}
#endif