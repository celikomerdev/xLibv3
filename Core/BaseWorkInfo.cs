#if xLibv3
using UnityEngine;

namespace xLib
{
	[System.Serializable]internal class BaseWorkInfo
	{
		[SerializeField]internal bool canDebug = false;
		[SerializeField]private bool canWork = true;
		[SerializeField]private bool runInEditMode;
		
		internal bool CanDebug
		{
			get
			{
				#if CanDebug
				return canDebug;
				#else
				return false;
				#endif
			}
			set
			{
				canDebug = value;
			}
		}
		
		internal bool CanWork
		{
			get
			{
				if(!canWork) return false;
				
				#if UNITY_EDITOR
				if(!Application.isPlaying && !runInEditMode) return false;
				#endif
				
				return true;
			}
			set
			{
				canWork = value;
			}
		}
		
		internal bool RunInEditMode
		{
			get
			{
				return runInEditMode;
			}
			set
			{
				#if UNITY_EDITOR
				runInEditMode = value;
				#endif
			}
		}
	}
}
#endif