#if xLibv3
#if ModWebWWW
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class WwwLoad : BaseWorkerM
	{
		[SerializeField]private EventWWW eventWWW = new EventWWW();
		
		#region Url
		private string url = "";
		public string Url
		{
			set
			{
				if(string.IsNullOrWhiteSpace(value)) return;
				if(url == value) return;
				url = value;
				
				this.WwwYield(url:value,call:(www)=>
				{
					eventWWW.Invoke(www);
				});
			}
		}
		#endregion
	}
}
#endif
#endif