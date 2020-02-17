#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using xLib.xValueClass;

namespace xLib
{
	[System.Serializable]
	public class ValueGroup : xValue<ObjectGroup>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueGroup global = new ValueGroup();
			global.Globalize();
		}
		#endregion
		
		protected override void OnInit(bool init)
		{
			base.OnInit(init);
			Value.Init(init);
		}
		
		#region Call
		public override void Call()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Call:{0}",ViewCore.CurrentId);
			ViewCore.RPC(nodeSetting.RpcTarget,nodeSetting.Key,SerializedObjectRaw.ToString());
		}
		
		public override void ListenerCall(bool register,UnityAction<object> call,string view,int order,bool onRegister=false,BaseWorkerI worker=null)
		{
			for (int i = 0; i < Value.iCall.Length; i++)
			{
				Value.iCall[i].ListenerCall(register,call,view,order,onRegister,worker);
			}
		}
		#endregion
		
		
		#region ISerializableBase
		internal virtual object SerializedObjectRaw
		{
			get
			{
				JObject jObject = new JObject();
				JObject Values = new JObject();
				for (int i = 0; i < Value.iSerializableObject.Length; i++)
				{
					ISerializableObject jsonInterface = Value.iSerializableObject[i];
					Values.Add(jsonInterface.Key,(JToken)jsonInterface.SerializedObjectRaw);
				}
				jObject.Add("Values",Values);
				
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":SerializedObject:Get:{0}:{1}",ViewCore.CurrentId,jObject);
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
				
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Values:true:{0}",ViewCore.CurrentId);
				for (int i = 0; i < Value.iSerializableObject.Length; i++)
				{
					ISerializableObject jsonInterface = Value.iSerializableObject[i];
					
					JToken token = Values.GetValue(jsonInterface.Key);
					if(token==null) token = Values.GetValue(jsonInterface.Name);
					
					jsonInterface.SerializedObjectRaw = token;
				}
			}
		}
		
		public override object SerializedObject
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
		
		public override object SerializedObjectName
		{
			get
			{
				JObject jObject = new JObject();
				for (int i = 0; i < Value.iSerializableObject.Length; i++)
				{
					ISerializableObject jsonInterface = Value.iSerializableObject[i];
					jObject.Add(jsonInterface.Name,(JToken)jsonInterface.SerializedObjectName);
				}
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":SerializedObjectName:Get:{0}:{1}",ViewCore.CurrentId,jObject);
				return jObject;
			}
		}
		#endregion
	}
}
#endif