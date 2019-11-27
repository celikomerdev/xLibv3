#if xLibv3
using UnityEngine;

namespace xLib
{
	public class AnimatorSetFloat : BaseMainM
	{
		[SerializeField]private string key = "";
		[SerializeField]private Animator target = null;
		
		public void Set(float value)
		{
			target.SetFloat(key,value);
		}
	}
}
#endif