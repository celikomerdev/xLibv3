#if xLibv3
using UnityEngine;
using UnityEngine.UI;

namespace xLib
{
	public class xButton : BaseMainM
	{
		[SerializeField]private Button target = null;
		
		public void OnClick()
		{
			target.onClick.Invoke();
		}
		
		#if UNITY_EDITOR
		[ContextMenu("Fill")]
		private void Fill()
		{
			if(!target) target = GetComponent<Button>();
			if(!target) target = GetComponentInParent<Button>();
		}
		#endif
	}
}
#endif