#if xLibv3
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public abstract class xValue<V>
	{
		[SerializeField]public NodeSetting nodeSetting = new NodeSetting();
		
		#region Field
		public string Key
		{
			get
			{
				return nodeSetting.Key;
			}
			set
			{
				nodeSetting.Key = value;
			}
		}
		
		[Header("Value")]
		[SerializeField]internal V value = default(V);
		#if UNITY_EDITOR
		private V valueDebug;
		#endif
		
		private ValueBase<V> valueBase = new ValueSingle<V>();
		public V ValueDefault
		{
			get
			{
				return valueBase.valueDefault;
			}
			set
			{
				valueBase.valueDefault = value;
			}
		}
		#endregion
		
		
		#region Init
		private bool isInit;
		public void Init(bool init)
		{
			if(isInit == init) return;
			isInit = init;
			if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:Init:{isInit}",nodeSetting.objDebug);
			OnInit(init);
		}
		
		protected virtual void CreateDefault(){}
		protected virtual void OnInit(bool value)
		{
			CreateDefault();
			CleanValue();
			CleanListener();
			
			ValueDefaultReset(this.value);
		}
		
		public void ValueDefaultReset(V value)
		{
			ValueDefault = value;
			string lastViewId = ViewCore.CurrentId;
			ViewCore.CurrentId = "Client";
			Value = ValueDefault;
			ViewCore.CurrentId = lastViewId;
		}
		#endregion
		
		
		#region Call
		private void CallClient()
		{
			if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:CallClient:{ValueToString}",nodeSetting.objDebug);
			actionSortedBase.Invoke(Value,viewId:"Client");
			actionSortedBaseCall.Invoke(Value,viewId:"Client");
			analyticDirty = true;
		}
		
		private void CallClientFirst()
		{
			if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:CallClientFirst:{ValueToString}",nodeSetting.objDebug);
			actionSortedBase.InvokeFirst(Value,viewId:"Client");
			actionSortedBaseCall.InvokeFirst(Value,viewId:"Client");
			analyticDirty = true;
		}
		
		private void CallClientLast()
		{
			if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:CallClientLast:{ValueToString}",nodeSetting.objDebug);
			actionSortedBase.InvokeLast(Value,viewId:"Client");
			actionSortedBaseCall.InvokeLast(Value,viewId:"Client");
			analyticDirty = true;
		}
		#endregion
		
		
		#region Listener
		#region Runtime
		private ActionSortedBase<V> actionSortedBase = new ActionSortedSingle<V>();
		public void Listener(bool register,UnityAction<V> call,string viewId,int order,bool onRegister=false,BaseWorkerI worker=null)
		{
			Object objDebug = nodeSetting.objDebug;
			if(worker!=null) objDebug = worker.UnityObject;
			if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:Listener:register:{register}:view:{viewId}:order:{order}:call:{call.Target}",objDebug);
			
			#if UNITY_EDITOR
			ListenerEditor(register,worker);
			#endif
			
			actionSortedBase.Listener(register,call,viewId,order);
			if(!register) return;
			if(!onRegister) return;
			call.Invoke(Value);
		}
		
		private ActionSortedBase<object> actionSortedBaseCall = new ActionSortedSingle<object>();
		public virtual void ListenerCall(bool register,UnityAction<object> call,string viewId,int order,bool onRegister=false,BaseWorkerI worker=null)
		{
			Object objDebug = nodeSetting.objDebug;
			if(worker!=null) objDebug = worker.UnityObject;
			if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:ListenerCall:register:{register}:view:{viewId}:order:{order}:call:{call.Target}",objDebug);
			
			#if UNITY_EDITOR
			ListenerEditor(register,worker);
			#endif
			
			actionSortedBaseCall.Listener(register,call,viewId,order);
			if(!register) return;
			if(!onRegister) return;
			call.Invoke(Value);
		}
		#endregion
		
		#region Editor
		#if UNITY_EDITOR
		private List<Object> listenerEditor = new List<Object>();
		private void ListenerEditor(bool register,BaseWorkerI worker)
		{
			if(!Application.isPlaying) return;
			if(worker==null) return;
			worker.CheckErrors();
			if(register)
			{
				if(listenerEditor.Contains(worker.UnityObject))
				{
					Debug.LogError($"{nodeSetting.objDebug.name}:ListenerEditor:++:{worker.UnityObject.name}",worker.UnityObject);
					return;
				}
				listenerEditor.Add(worker.UnityObject);
			}
			else
			{
				if(!listenerEditor.Contains(worker.UnityObject))
				{
					Debug.LogError($"{nodeSetting.objDebug.name}:ListenerEditor:--:{worker.UnityObject.name}",worker.UnityObject);
					return;
				}
				listenerEditor.Remove(worker.UnityObject);
			}
		}
		#endif
		#endregion
		
		#region Clean
		private void CleanValue()
		{
			if(nodeSetting.isMulti) valueBase = new ValueMulti<V>();
			else valueBase = new ValueSingle<V>();
		}
		
		public void CleanListener()
		{
			if(nodeSetting.isMulti)
			{
				actionSortedBase = new ActionSortedMulti<V>();
				actionSortedBaseCall = new ActionSortedMulti<object>();
			}
			else
			{
				actionSortedBase = new ActionSortedSingle<V>();
				actionSortedBaseCall = new ActionSortedSingle<object>();
			}
			
			#if UNITY_EDITOR
			listenerEditor = new List<Object>();
			#endif
		}
		#endregion
		#endregion
		
		
		#region Virtual
		public virtual string ValueToString
		{
			get
			{
				return Value.ToString();
			}
		}
		
		protected virtual bool CanChange(V value)
		{
			return true;
		}
		
		protected virtual bool CanCache(V value)
		{
			return true;
		}
		
		protected virtual void KeepProperties(V value){}
		#endregion
		
		
		#region Property
		#region Value
		public virtual V Value
		{
			get
			{
				return valueBase.ValueGet(ViewCore.CurrentId);
			}
			set
			{
				if(!CanChange(value))
				{
					if(nodeSetting.canDebug) Debug.LogWarning($"{nodeSetting.objDebug.name}:!CanChange:{ViewCore.CurrentId}:{ValueToString}",nodeSetting.objDebug);
					return;
				}
				KeepProperties(value);
				
				#if UNITY_EDITOR
				if(ViewCore.IsMy) valueDebug = value;
				#endif
				
				valueBase.ValueSet(value,ViewCore.CurrentId);
				
				if(nodeSetting.UseRpc && !ViewCore.inRpc)
				{
					if(ViewCore.IsMy)
					{
						Call();
						return;
					}
				}
				
				CallClient();
			}
		}
		
		public virtual void Call()
		{
			if(!nodeSetting.UseRpc) CallClient();
			else
			{
				if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:Call:{ViewCore.CurrentId}:{ValueToString}",nodeSetting.objDebug);
				ViewCore.RPC(nodeSetting.RpcTarget,Key,SerializedObject.ToString());
			}
		}
		
		public virtual void CallFirst()
		{
			if(!nodeSetting.UseRpc) CallClientFirst();
			else
			{
				if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:CallFirst:{ViewCore.CurrentId}:{ValueToString}",nodeSetting.objDebug);
				ViewCore.RPC(nodeSetting.RpcTarget,Key,SerializedObject.ToString());
			}
		}
		
		public virtual void CallLast()
		{
			if(!nodeSetting.UseRpc) CallClientLast();
			else
			{
				if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:CallLast:{ViewCore.CurrentId}:{ValueToString}",nodeSetting.objDebug);
				ViewCore.RPC(nodeSetting.RpcTarget,Key,SerializedObject.ToString());
			}
		}
		
		private V valueCache;
		public virtual V ValueCache
		{
			get
			{
				return valueCache;
			}
			set
			{
				if(!CanCache(value)) return;
				valueCache = value;
			}
		}
		#endregion
		
		
		#region ValueAdd
		public virtual V ValueAdd
		{
			set
			{
				Value = value;
			}
		}
		#endregion
		
		
		#region ValueUpdate
		public void Refresh()
		{
			Value = valueCache;
		}
		
		public void Consume()
		{
			Value = valueCache;
			valueCache = ValueDefault;
		}
		#endregion
		#endregion
		
		
		#region SerializedObject
		public virtual object SerializedObject
		{
			get
			{
				JToken jToken;
				if(Value.Equals(null)) jToken = JToken.FromObject("");
				else jToken = JToken.FromObject(Value);
				return jToken;
			}
			set
			{
				if(value==null) return;
				if(string.IsNullOrEmpty(value.ToString())) return;
				JToken jToken = JToken.FromObject(value);
				Value = jToken.ToObject<V>();
			}
		}
		
		public virtual object SerializedObjectName
		{
			get
			{
				return SerializedObject;
			}
		}
		#endregion
		
		
		#region Analytics
		internal bool analyticDirty = false;
		internal virtual object AnalyticObject
		{
			get
			{
				return Value;
			}
		}
		
		internal virtual string AnalyticString
		{
			get
			{
				return ValueToString;
			}
		}
		
		internal virtual double AnalyticDigit
		{
			get
			{
				return 0;
			}
		}
		#endregion
	}
}
#endif