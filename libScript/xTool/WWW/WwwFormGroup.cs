#if xLibv3
using UnityEngine;

namespace xLib
{
	[System.Serializable]
	public class WwwFormGroup : IDebug
	{
		public Object UnityObject{get;set;}
		public bool CanDebug{get;set;}
		
		[Header("ISerializableObject")]
		[SerializeField]private Object[] arrayObjectKey = new Object[0];
		[SerializeField]private Object[] arrayObjectName = new Object[0];
		
		public WWWForm FormData
		{
			get
			{
				WWWForm form = new WWWForm();
				
				ISerializableObject[] iSerializableObjectKey = arrayObjectKey.GetGenericsArray<ISerializableObject>(UnityObject);
				for (int i = 0; i < iSerializableObjectKey.Length; i++)
				{
					form.AddField(iSerializableObjectKey[i].Key,iSerializableObjectKey[i].SerializedObjectRaw.ToString());
				}
				
				ISerializableObject[] iSerializableObjectName = arrayObjectName.GetGenericsArray<ISerializableObject>(UnityObject);
				for (int i = 0; i < iSerializableObjectName.Length; i++)
				{
					form.AddField(iSerializableObjectName[i].Name,iSerializableObjectName[i].SerializedObjectName.ToString());
				}
				
				return form;
			}
		}
	}
}
#endif