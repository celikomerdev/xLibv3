#if xLibv3
using UnityEngine;

namespace xLib
{
	public class HighlightMaterial : BaseTickNodeM
	{
		[SerializeField]private Transform target = null;
		public Transform Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
				this.enabled = (value!=null);
			}
		}
		
		[SerializeField]private Camera targetCamera = null;
		[SerializeField]private Material targetMaterial = null;
		private static readonly int Center = Shader.PropertyToID("_Center");
		private static readonly int Radius = Shader.PropertyToID("_Radius");
		
		protected override void Tick(float tickTime)
		{
			Vector2 screenPoint = targetCamera.WorldToScreenPoint(target.position);
			
			Vector2 position = new Vector2();
			int pixelWidth = targetCamera.pixelWidth;
			position.x = screenPoint.x/pixelWidth;
			position.y = ((pixelWidth-targetCamera.pixelHeight)*0.5f+screenPoint.y)/pixelWidth;
			
			targetMaterial.SetVector(Center,position);
			targetMaterial.SetFloat(Radius,target.lossyScale.z);
		}
	}
}
#endif