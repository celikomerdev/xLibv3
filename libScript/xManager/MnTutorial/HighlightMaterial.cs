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
		
		protected override bool OnRegister(bool register)
		{
			if(!register)
			{
				target.localScale = Vector3.zero;
			}
			else
			{
				target.localScale = Vector3.one;
			}
			target.localPosition = Vector3.zero;
			Tick(1);
			return base.OnRegister(register);
		}
		
		protected override void Tick(float tickTime)
		{
			Vector2 screenPoint = targetCamera.WorldToScreenPoint(target.position);
			Vector2 position = new Vector4(screenPoint.x / targetCamera.pixelWidth, (screenPoint.y + (targetCamera.pixelWidth - targetCamera.pixelHeight) / 2f) / targetCamera.pixelWidth);
			targetMaterial.SetVector("_Center",position);
			targetMaterial.SetFloat("_Radius",target.lossyScale.z*10f);
		}
	}
}
#endif