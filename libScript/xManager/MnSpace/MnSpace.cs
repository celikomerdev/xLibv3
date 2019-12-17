#if xLibv3
using UnityEngine;

namespace xLib
{
	public class MnSpace : SingletonM<MnSpace>
	{
		[SerializeField]private Transform trans = null;
		
		protected override void Awaked()
		{
			if(trans) ExtSpace.origin = trans.position;
		}
	}
}
#endif