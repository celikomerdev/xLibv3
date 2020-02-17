#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	public abstract class NodeGroup : NodeValue<ObjectGroup>
	{
		#region ISerializableBaseContext
		#if UNITY_EDITOR
		[ContextMenu("ChildKey = Name")]
		private void ChildKeyName()
		{
			Node.Value.ChildKeyName();
		}
		
		[ContextMenu("ChildKey = Guid")]
		private void ChildKeyGuid()
		{
			Node.Value.ChildKeyGuid();
		}
		#endif
		#endregion
		
		
		#region Database
		public int IndexCurrent
		{
			get
			{
				return Node.Value.indexCurrent;
			}
		}
		
		public ISerializableObject GetByIndex(int value)
		{
			return Node.Value.GetByIndex(value);
		}
		
		public ISerializableObject GetByOrder(int value)
		{
			return Node.Value.GetByOrder(value);
		}
		
		public ISerializableObject GetByRandom()
		{
			return Node.Value.GetByRandom();
		}
		
		public ISerializableObject GetByKey(string value)
		{
			return Node.Value.GetByKey(value);
		}
		#endregion
	}
}
#endif