#if xLibv3
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public class MnSpace : SingletonM<MnSpace>
	{
		public static Transform trans = null;
		[SerializeField]private Transform m_trans = null;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void CreateStaticPivot()
		{
			if(trans) return;
			Debug.LogFormat("MnSpace:CreateStaticPivot");
			trans = new GameObject().transform;
			trans.name = "MnSpace.Pivot";
			DontDestroyOnLoad(trans);
		}
		
		protected override void Awaked()
		{
			Destroy(trans);
			trans = m_trans;
		}
		
		protected override void OnDestroyed()
		{
			CreateStaticPivot();
		}
		
		private static UnityAction<Vector3> listener = delegate(Vector3 arg){};
		public static void Listener(UnityAction<Vector3> call,bool addition)
		{
			if(addition)
			{
				listener += call;
			}
			else
			{
				listener -= call;
			}
		}
		
		public static void Translate(Vector3 value)
		{
			if(xDebug.CanDebug) Debug.Log("Translate: "+value.ToString());
			listener.Invoke(value);
			trans.Translate(value);
		}
	}
}
#endif