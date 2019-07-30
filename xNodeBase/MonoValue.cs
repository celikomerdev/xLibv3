#if xLibv3
using UnityEngine.Events;

namespace xLib.xNode.NodeObject
{
	public abstract class MonoValue<V> : MonoSave, ICall
	{
		protected abstract xValue<V> Node
		{
			get;
		}
		
		protected override void SetDebug()
		{
			Node.nodeSetting.canDebug = CanDebug;
			Node.nodeSetting.objDebug = this;
			if(string.IsNullOrEmpty(Key)) KeyGuid();
		}
		
		public override string Key
		{
			get
			{
				return Node.nodeSetting.Key;
			}
			protected set
			{
				Node.nodeSetting.Key = value;
			}
		}
		
		public override string Name
		{
			get
			{
				return Node.nodeSetting.Name;
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
		
		public V ValueCache
		{
			get
			{
				return Node.ValueCache;
			}
			set
			{
				Node.ValueCache = value;
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
		
		public V ValueAdd
		{
			set
			{
				Node.ValueAdd = value;
			}
		}
		
		public void Refresh()
		{
			Node.Refresh();
		}
		
		public void Consume()
		{
			Node.Consume();
		}
		
		public void Listener(bool register,UnityAction<V> call,bool onRegister=false)
		{
			Node.Listener(register,call,onRegister);
		}
		
		public void ListenerCall(bool register,UnityAction call,bool onRegister=false)
		{
			Node.ListenerCall(register,call,onRegister);
		}
		
		public void ListenerEditor(bool addition,BaseActiveM call)
		{
			Node.ListenerEditor(addition,call);
		}
		
		public void CleanListener()
		{
			Node.CleanListener();
		}
		
		public override void CallMulti()
		{
			Node.CallMulti();
		}
	}
}
#endif