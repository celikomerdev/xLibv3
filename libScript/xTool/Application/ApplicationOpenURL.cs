#if xLibv2
using UnityEngine;

namespace xLib.ToolApplication
{
	public class ApplicationOpenURL : BaseM
	{
		[SerializeField]private string value;
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}
		
		public void OpenURL()
		{
			if(string.IsNullOrEmpty(this.value)) return;
			Application.OpenURL(this.value);
		}
		
		public void OpenURL(string url)
		{
			if(string.IsNullOrEmpty(url)) return;
			Application.OpenURL(url);
		}
	}
}
#endif