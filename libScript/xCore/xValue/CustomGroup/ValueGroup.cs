#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;
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
			Value.UnityObject = nodeSetting.UnityObject;
			Value.CanDebug = nodeSetting.CanDebug;
			Value.Init(init);
		}
		
		#region Call
		public override void Call()
		{
			if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:Call:CurrentId:{ViewCore.CurrentId}",nodeSetting.UnityObject);
			ViewCore.RPC(nodeSetting.RpcTarget,nodeSetting.Key,SerializedObjectRaw.ToJsonString());
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
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueGroup:Get",nodeSetting.UnityObject);
				for (int i = 0; i < Value.iSerializableObject.Length; i++)
				{
					ISerializableObject jsonInterface = Value.iSerializableObject[i];
					JToken tempToken = (JToken)jsonInterface.SerializedObjectRaw;
					if(tempToken==null) continue;
					Values.Add(jsonInterface.Key,tempToken);
				}
				Profiler.EndSample();
				
				if(!Values.HasValues) return null;
				jObject.Add("Values",Values);
				
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:SerializedObject:Get:CurrentId:{ViewCore.CurrentId}:jObject:{jObject}",nodeSetting.UnityObject);
				return jObject;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:SerializedObject:Set:CurrentId:{ViewCore.CurrentId}:jObject:{value}",nodeSetting.UnityObject);
				if(string.IsNullOrEmpty(stringJson)) return;
				
				if(nodeSetting.UseRpc && !ViewCore.inRpc)
				{
					if(ViewCore.IsMy)
					{
						if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:RPC:CurrentId:{ViewCore.CurrentId}",nodeSetting.UnityObject);
						ViewCore.RPC(nodeSetting.RpcTarget,nodeSetting.Key,stringJson);
						return;
					}
				}
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueGroup:Set",nodeSetting.UnityObject);
				JObject jObject = JObject.Parse(stringJson);
				JObject Values = (JObject)jObject.GetValue("Values");
				if(Values==null) Values = jObject;
				
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:Distribute:CurrentId:{ViewCore.CurrentId}",nodeSetting.UnityObject);
				for (int i = 0; i < Value.iSerializableObject.Length; i++)
				{
					ISerializableObject jsonInterface = Value.iSerializableObject[i];
					
					JToken token = Values.GetValue(jsonInterface.Key);
					if(token==null) token = Values.GetValue(jsonInterface.Name);
					
					jsonInterface.SerializedObjectRaw = token;
				}
				Profiler.EndSample();
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
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:SerializedObjectName:Get:CurrentId:{ViewCore.CurrentId}:jObject:{jObject}",nodeSetting.UnityObject);
				return jObject;
			}
		}
		#endregion
	}
}
#endif