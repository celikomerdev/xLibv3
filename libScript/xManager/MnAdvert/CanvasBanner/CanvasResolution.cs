#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolUI
{
	public class CanvasResolution : BaseWorkM
	{
		[SerializeField]private CanvasScaler canvasScaler = null;
		private Vector2 referenceResolution = Vector2.zero;
		private Vector2 offsetMin = Vector2.zero;
		private Vector2 offsetMax = Vector2.zero;
		
		
		private void Awake()
		{
			referenceResolution = canvasScaler.referenceResolution;
		}
		
		#region Work
		private void Work()
		{
			if(!CanWork) return;
			SetOffset();
			DebugWork();
		}
		
		private void SetOffset()
		{
			Vector2 tempResolution = Vector3.zero;
			
			tempResolution += referenceResolution;
			tempResolution += offsetMin;
			tempResolution += offsetMax;
			
			canvasScaler.referenceResolution = tempResolution;
		}
		
		private void DebugWork()
		{
			if(!CanDebug) return;
			
			Debug.LogFormat(this,this.name+":offsetMin:{0}",offsetMin.ToString());
			Debug.LogFormat(this,this.name+":offsetMax:{0}",offsetMax.ToString());
			Debug.LogFormat(this,this.name+":sizeDelta:{0}",canvasScaler.referenceResolution.ToString());
		}
		#endregion
		
		
		#region Left
		public float PixelLeft
		{
			set
			{
				// float pixelMultiplier = (rectCanvas.sizeDelta.x/Screen.width);
				float pixelMultiplier = 1f;
				SizeLeft = pixelMultiplier*value;
			}
		}
		
		public float SizeLeft
		{
			set
			{
				offsetMin.x = value;
				Work();
			}
		}
		#endregion
		
		
		#region Right
		public float PixelRight
		{
			set
			{
				// float pixelMultiplier = (rectCanvas.sizeDelta.x/Screen.width);
				float pixelMultiplier = 1f;
				SizeRight = pixelMultiplier*value;
			}
		}
		
		public float SizeRight
		{
			set
			{
				offsetMax.x = -value;
				Work();
			}
		}
		#endregion
		
		
		#region Bottom
		public float PixelBottom
		{
			set
			{
				// float pixelMultiplier = (rectCanvas.sizeDelta.y/Screen.height);
				float pixelMultiplier = 1f;
				SizeBottom = pixelMultiplier*value;
			}
		}
		
		public float SizeBottom
		{
			set
			{
				offsetMin.y = value;
				Work();
			}
		}
		#endregion
		
		
		#region Top
		public float PixelTop
		{
			set
			{
				// float pixelMultiplier = (rectCanvas.sizeDelta.y/Screen.height);
				float pixelMultiplier = 1f;
				SizeTop = pixelMultiplier*value;
			}
		}
		
		public float SizeTop
		{
			set
			{
				offsetMax.y = -value;
				Work();
			}
		}
		#endregion
	}
}
#endif
#endif