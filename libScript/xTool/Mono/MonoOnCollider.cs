#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnCollider : BaseMainM
	{
		[Header("Listen")]
		#pragma warning disable
		[SerializeField]private bool trigger = true;
		[SerializeField]private bool collision = true;
		#pragma warning restore
		
		[Header("Filter")]
		[SerializeField]private string[] excludeTag = new string[0];
		[SerializeField]private string[] excludeName = new string[0];
		[SerializeField]private string[] includeTag = new string[0];
		[SerializeField]private string[] includeName = new string[0];
		
		[Header("Event")]
		[SerializeField]private EventBool eventCollider = new EventBool();
		
		
		#region OnTransform
		private void OnTransform(Component trans,bool status)
		{
			if(Comparision(trans)) eventCollider.Invoke(status);
		}
		
		#if ModPhysics
		private void OnTriggerEnter(Collider collider)
		{
			if(!trigger) return;
			OnTransform(collider.transform,true);
		}
		
		private void OnTriggerExit(Collider collider)
		{
			if(!trigger) return;
			OnTransform(collider.transform,false);
		}
		
		private void OnCollisionEnter(Collision collision)
		{
			if(!collision) return;
			OnTransform(collision.collider.transform,true);
		}
		
		private void OnCollisionExit(Collision collision)
		{
			if(!collision) return;
			OnTransform(collision.collider.transform,false);
		}
		#endif
		#endregion
		
		
		#region Comparison
		private bool Comparision(Component trans)
		{
			bool fallback = true;
			if(excludeTag.Length>0)
			{
				for(int i = 0; i < excludeTag.Length; i++)
				{
					if(trans.CompareTag(excludeTag[i])) return false;
				}
			}
			if(excludeName.Length>0)
			{
				for(int i = 0; i < excludeName.Length; i++)
				{
					if(trans.name == excludeName[i]) return false;
				}
			}
			if(includeTag.Length>0)
			{
				fallback = false;
				for(int i = 0; i < includeTag.Length; i++)
				{
					if(trans.CompareTag(includeTag[i])) return true;
				}
			}
			if(includeName.Length>0)
			{
				fallback = false;
				for(int i = 0; i < includeName.Length; i++)
				{
					if(trans.name == includeName[i]) return true;
				}
			}
			return fallback;
		}
		#endregion
	}
}
#endif