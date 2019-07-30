#if xLibv2
using UnityEngine;
using UnityEngine.Events;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Basic/void")]
	public class NodeVoid : NodeBase, ICall
	{
		[SerializeField]internal ValueVoid nodeValue = new ValueVoid();
		private ValueVoid Node
		{
			get
			{
				return nodeValue;
			}
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
		
		public override bool UseRpc
		{
			set
			{
				Node.nodeSetting.UseRpc = value;
			}
		}
		
		public override xRpcTarget RpcTarget
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
				return "";
			}
			set
			{
				Call();
			}
		}
		
		public override object SerializedObjectName
		{
			get
			{
				return "";
			}
		}
		
		public void Call()
		{
			Node.Call();
		}
		
		public void Listener(bool register,UnityAction call,bool onRegister=false)
		{
			Node.Listener(register,call,onRegister);
		}
		
		public void ListenerCall(bool register,UnityAction call,bool onRegister=false)
		{
			Node.Listener(register,call,onRegister);
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