#if DiscardxLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMono
{
	public class MonoOnTriggerEnter : BaseM
	{
		public string[] excludeName;
		public string[] excludeTag;
		public string[] includeName;
		public string[] includeTag;
		public EventBool onTriggerEnter;
		
		private void OnTriggerEnter(Collider value)
		{
			if(Comparision(value.gameObject)) onTriggerEnter.Invoke(true);
		}
		
		private void OnTriggerExit(Collider value)
		{
			if(Comparision(value.gameObject)) onTriggerEnter.Invoke(false);
		}
		
		#region Comparison
		private bool Comparision(GameObject value)
		{
			bool fallback = true;
			if(excludeName.Length>0)
			{
				fallback = true;
				for(int i = 0; i < excludeName.Length; i++)
				{
					if(value.name == excludeName[i]) return false;
				}
			}
			if(excludeTag.Length>0)
			{
				fallback = true;
				for(int i = 0; i < excludeTag.Length; i++)
				{
					if(value.tag == excludeTag[i]) return false;
				}
			}
			if(includeName.Length>0)
			{
				fallback = false;
				for(int i = 0; i < includeName.Length; i++)
				{
					if(value.name == includeName[i]) return true;
				}
			}
			if(includeTag.Length>0)
			{
				fallback = false;
				for(int i = 0; i < includeTag.Length; i++)
				{
					if(value.tag == includeTag[i]) return true;
				}
			}
			return fallback;
		}
		#endregion
	}
}
#endif