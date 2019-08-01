#if xLibv3
using UnityEngine;
using UnityEngine.Events;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	public abstract class MonoGroup : MonoSave, ICall
	{
		protected abstract ValueGroup Node
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
		
		#region ISerializableBase
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
				return Node.SerializedObjectRaw;
			}
			set
			{
				Node.SerializedObjectRaw = value;
			}
		}
		
		public override object SerializedObject
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
		
		
		#region ISerializableBaseContext
		#if UNITY_EDITOR
		[ContextMenu ("ChildKey = Name")]
		private void ChildKeyName()
		{
			Node.ChildKeyName();
		}
		
		[ContextMenu ("ChildKey = Guid")]
		private void ChildKeyGuid()
		{
			Node.ChildKeyGuid();
		}
		#endif
		#endregion
		
		
		#region Database
		public int IndexCurrent
		{
			get
			{
				return Node.indexCurrent;
			}
		}
		
		public ISerializableObject GetByIndex(int value)
		{
			return Node.GetByIndex(value);
		}
		
		public ISerializableObject GetByOrder(int value)
		{
			return Node.GetByOrder(value);
		}
		
		public ISerializableObject GetByRandom()
		{
			return Node.GetByRandom();
		}
		
		public ISerializableObject GetByKey(string value)
		{
			return Node.GetByKey(value);
		}
		#endregion
		
		
		public override void CallMulti()
		{
			Node.CallMulti();
		}
		
		public void ListenerCall(bool register,UnityAction call,bool onRegister=false)
		{
			Node.ListenerCall(register,call,onRegister);
		}
		
		public void ListenerEditor(bool addition,BaseActiveM call)
		{
			Node.ListenerEditor(addition,call);
		}
	}
}
#endif