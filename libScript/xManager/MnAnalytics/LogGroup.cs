#if xLibv3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xLib.ToolManager
{
	public abstract class LogGroup : BaseWorkM
	{
		[SerializeField]private float interval = 0.1f;
		[SerializeField]private Object[] arrayIAnalyticObject = new Object[0];
		[SerializeField]private IAnalyticObject[] m_arrayIAnalyticObject = new IAnalyticObject[0];
		
		private void Awake()
		{
			if(CanDebug) Debug.Log($"{this.name}:Awake",this);
			m_arrayIAnalyticObject = arrayIAnalyticObject.GetGenericsArray<IAnalyticObject>();
		}
		
		public void SendAll()
		{
			if(CanDebug) Debug.Log($"{this.name}:Call",this);
			List<IAnalyticObject> listDirty = new List<IAnalyticObject>();
			for (int i = 0; i < m_arrayIAnalyticObject.Length; i++)
			{
				IAnalyticObject analyticObject = m_arrayIAnalyticObject[i];
				if(analyticObject.AnalyticDirty) listDirty.Add(analyticObject);
			}
			this.NewCoroutine(enumerator:SendDirty(listDirty));
		}
		
		private void ClearAll()
		{
			if(CanDebug) Debug.Log($"{this.name}:ClearAll",this);
			for (int i = 0; i < m_arrayIAnalyticObject.Length; i++)
			{
				m_arrayIAnalyticObject[i].AnalyticDirty = false;
			}
		}
		
		private IEnumerator SendDirty(List<IAnalyticObject> list)
		{
			yield return new WaitForSecondsRealtime(0.1f);
			ClearAll();
			
			for (int i = 0; i < list.Count; i++)
			{
				yield return new WaitForSecondsRealtime(interval);
				Send(list[i]);
			}
		}
		
		protected abstract void Send(IAnalyticObject analyticObject);
	}
}
#endif