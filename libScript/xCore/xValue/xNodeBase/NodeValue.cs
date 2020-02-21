#if xLibv3
using UnityEngine.Events;

namespace xLib.xValueClass
{
	public abstract class NodeValue<V> : NodeSave, ICall
	{
		protected abstract xValue<V> Node
		{
			get;
		}
		
		protected override void SetDebug()
		{
			Node.nodeSetting.CanDebug = CanDebug;
			Node.nodeSetting.UnityObject = this;
			if(string.IsNullOrEmpty(Key)) KeyGuid();
		}
		
		public override string Key
		{
			get
			{
				return Node.nodeSetting.Key;
			}
			#if UNITY_EDITOR
			protected set
			{
				Init(true);
				Node.nodeSetting.Key = value;
			}
			#endif
		}
		
		public override string ValueToString
		{
			get
			{
				return Node.ValueToString;
			}
		}
		
		public override bool AnalyticDirty
		{
			get
			{
				return Node.analyticDirty;
			}
			set
			{
				Node.analyticDirty = value;
			}
		}
		
		public override object AnalyticObject
		{
			get
			{
				return Node.AnalyticObject;
			}
		}
		
		public override string AnalyticString
		{
			get
			{
				return Node.AnalyticString;
			}
		}
		
		public override double AnalyticDigit
		{
			get
			{
				return Node.AnalyticDigit;
			}
		}
		
		protected override void OnInit(bool init)
		{
			Node.Init(init);
		}
		
		#region SerializedObject
		public override bool UseRpc
		{
			set
			{
				Node.nodeSetting.UseRpc = value;
			}
		}
		
		public override string RpcTarget
		{
			set
			{
				Node.nodeSetting.RpcTarget = value;
			}
		}
		
		public override object SerializedObjectRaw
		{
			get
			{
				return Node.SerializedObject;
			}
			set
			{
				Node.SerializedObject = value;
			}
		}
		
		public override object SerializedObjectName
		{
			get
			{
				return Node.SerializedObjectName;
			}
		}
		#endregion
		
		public V Value
		{
			get
			{
				return Node.Value;
			}
			set
			{
				Node.Value = value;
			}
		}
		
		public V ValueDefault
		{
			get
			{
				return Node.ValueDefault;
			}
			set
			{
				Node.ValueDefault = value;
			}
		}
		
		public void ValueDefaultReset(V value)
		{
			Node.ValueDefaultReset(value);
		}
		
		public void ValueDefaultReset()
		{
			Node.ValueDefaultReset(Node.ValueDefault);
		}
		
		public V ValueAdd
		{
			set
			{
				Node.ValueAdd = value;
			}
		}
		
		public void Listener(bool register,UnityAction<V> call,string viewId="",int order=0,bool onRegister=false,BaseWorkerI worker=null)
		{
			Node.Listener(register,call,viewId,order,onRegister,worker);
		}
		
		public void ListenerCall(bool register,UnityAction<object> call,string viewId="",int order=0,bool onRegister=false,BaseWorkerI worker=null)
		{
			Node.ListenerCall(register,call,viewId,order,onRegister,worker);
		}
		
		public void CleanListener()
		{
			Node.CleanListener();
		}
		
		public override void Call()
		{
			Node.Call();
		}
		
		public override void CallFirst()
		{
			Node.CallFirst();
		}
		
		public override void CallLast()
		{
			Node.CallLast();
		}
		
		public override void Globalize()
		{
			Node.Globalize();
		}
	}
}
#endif