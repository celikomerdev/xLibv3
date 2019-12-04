#if xLibv2
using System;
using UnityEngine;

namespace xLib.ToolWorldTime
{
	public abstract class WorldTimeBase : BaseWorkM
	{
		[Header("Base")]
		[SerializeField]protected xDateTime dateTimeOrigin;
		[SerializeField]protected string url;
		
		private void Awake()
		{
			dateTimeOrigin.Fill();
		}
		
		public virtual void Refresh()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Refresh");
		}
	}
}
#endif