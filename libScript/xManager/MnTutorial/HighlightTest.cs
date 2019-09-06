#if xLibv3
using UnityEngine;

namespace xLib
{
	public class HighlightTest : BaseTickNodeM
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
		[SerializeField]private Camera m_camera = null;
		[SerializeField]private Material materialBg = null;
		
		protected override void Tick(float tickTime)
		{
			Vector2 screenPoint = m_camera.WorldToScreenPoint(target.transform.position);
			Vector2 position = new Vector4(screenPoint.x / m_camera.pixelWidth, (screenPoint.y + (m_camera.pixelWidth - m_camera.pixelHeight) / 2f) / m_camera.pixelWidth);
			materialBg.SetVector("_Center",position);
			materialBg.SetFloat("_Radius",target.localScale.z*0.2f);
		}
	}
}
#endif