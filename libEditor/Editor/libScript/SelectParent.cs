#if xLibv3
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace xLib.libEditor
{
	public class SelectParent 
	{
		public static void Work()
		{
			Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);
			List<GameObject> parents = new List<GameObject>();
			
			foreach (Transform t in transforms)
			{
				if(t == null) continue;
				if(t.parent == null) continue;
				//if(t.parent.gameObject == null) continue;
				
				parents.Add(t.parent.gameObject);
			}
			
			Selection.objects = parents.ToArray();
		}
	}
}
#endif
#endif