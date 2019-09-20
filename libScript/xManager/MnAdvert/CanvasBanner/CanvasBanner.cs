#if xLibv3
using UnityEngine;

namespace xLib.ToolUI
{
	public class CanvasBanner : BaseWorkM
	{
		[SerializeField]private RectTransform rectCanvas = null;
		[SerializeField]private RectTransform rectTransform = null;
		private Vector2 offsetMin = Vector2.zero;
		private Vector2 offsetMax = Vector2.zero;
		
		
		#region Work
		private void Work()
		{
			if(!CanWork) return;
			//TODO FixReference
			SetOffset();
			DebugWork();
		}
		
		private void SetOffset()
		{
			rectTransform.offsetMin = offsetMin;
			rectTransform.offsetMax = offsetMax;
		}
		
		private void DebugWork()
		{
			if(!CanDebug) return;
			Debug.LogFormat(this,this.name+":sizeDelta:{0}",rectCanvas.sizeDelta.ToString());
			Debug.LogFormat(this,this.name+":offsetMin:{0}",offsetMin.ToString());
			Debug.LogFormat(this,this.name+":offsetMax:{0}",offsetMax.ToString());
		}
		#endregion
		
		#region Left
		public float PixelLeft
		{
			set
			{
				float pixelMultiplier = (rectCanvas.sizeDelta.x/Screen.width);
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
				float pixelMultiplier = (rectCanvas.sizeDelta.x/Screen.width);
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
				float pixelMultiplier = (rectCanvas.sizeDelta.y/Screen.height);
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
				float pixelMultiplier = (rectCanvas.sizeDelta.y/Screen.height);
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