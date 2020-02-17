#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	public abstract class NodeBase : BaseInitS, ISerializableObject, IRpc, IAnalyticObject
	{
		#region Key
		public abstract string Key
		{
			get;
			#if UNITY_EDITOR
			protected set;
			#endif
		}
		
		public abstract string Name
		{
			get;
		}
		
		public virtual string ValueToString
		{
			get
			{
				return "";
			}
		}
		
		[ContextMenu ("ValueToString")]
		private void ValueToStringDebug()
		{
			Debug.LogFormat(this,this.name+":ValueToString:{0}",ValueToString);
		}
		
		[ContextMenu ("Key = Name")]
		public void KeyName()
		{
			#if UNITY_EDITOR
			Key = this.name;
			#endif
		}
		
		[ContextMenu ("Key = Guid")]
		public void KeyGuid()
		{
			#if UNITY_EDITOR
			string path = UnityEditor.AssetDatabase.GetAssetPath(this);
			Key = UnityEditor.AssetDatabase.AssetPathToGUID(path);
			#endif
		}
		#endregion
		
		#region Property
		public abstract bool UseRpc
		{
			set;
		}
		
		public abstract string RpcTarget
		{
			set;
		}
		
		public abstract object SerializedObjectRaw
		{
			get;
			set;
		}
		
		public virtual object SerializedObject
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
		
		public abstract object SerializedObjectName
		{
			get;
		}
		#endregion
		
		public virtual void Call(){}
		public virtual void CallFirst(){}
		public virtual void CallLast(){}
		public virtual void Globalize(){}
		
		
		#region IAnalyticObject
		public abstract bool AnalyticDirty
		{
			get;
			set;
		}
		
		public abstract object AnalyticObject
		{
			get;
		}
		
		public abstract string AnalyticString
		{
			get;
		}
		
		public abstract double AnalyticDigit
		{
			get;
		}
		#endregion
	}
}
#endif