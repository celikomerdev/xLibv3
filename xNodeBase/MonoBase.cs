#if xLibv3
using UnityEngine;

namespace xLib.xNode.NodeObject
{
	public abstract class MonoBase : MonoInit, ISerializableObject, IRpc
	{
		#region Key
		public abstract string Key
		{
			get;
			protected set;
		}
		
		public abstract string Name
		{
			get;
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
		
		public virtual void CallMulti(){}
	}
}
#endif