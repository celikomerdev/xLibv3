#if xLibv2
using System;
using System.Collections.Generic;
using UnityEngine;
using xLib.ToolEventClass;
using xLib.ToolWWW;

namespace xLib.ToolShowcase
{
	[Serializable]
	public class ItemShowcase
	{
		public string nameShort;
		public string name;
		public string link;
		public List<LoadTexture> arrayImage = new List<LoadTexture>();
		
		public EventUnity onRefresh = new EventUnity();
		public void OnRefresh(Texture texture)
		{
			onRefresh.Invoke();
		}
	}
}
#endif