#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolOperation
{
	public class ToggleBool : BaseInitM
	{
		[SerializeField]private bool value = false;
		public EventBool eventBool = new EventBool();
		
		protected override void OnInit(bool init)
		{
			if(!init) return;
			eventBool.Invoke(value);
		}
		
		public bool Value
		{
			get
			{
				return value;
			}
			set
			{
				if(this.value == value) return;
				this.value = value;
				eventBool.Invoke(value);
			}
		}
		
		public void Toggle()
		{
			Value = !Value;
		}
	}
}
#endif