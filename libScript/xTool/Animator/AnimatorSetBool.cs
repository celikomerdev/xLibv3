#if xLibv3
using UnityEngine;

namespace xLib
{
	public class AnimatorSetBool : BaseMainM
	{
		[SerializeField]private string key = "";
		[SerializeField]private Animator target = null;
		
		public void Set(bool value)
		{
			target.SetBool(key,value);
		}
	}
}
#endif