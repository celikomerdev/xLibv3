#if xLibv3
using UnityEngine;

namespace xLib.ToolManager
{
	public abstract class LogGroup : BaseWorkM
	{
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
			for (int i = 0; i < m_arrayIAnalyticObject.Length; i++)
			{
				IAnalyticObject analyticObject = m_arrayIAnalyticObject[i];
				if(analyticObject.AnalyticDirty) Send(analyticObject);
			}
			this.WaitForSeconds(call:ClearAll,delay:0.1f);
		}
		
		private void ClearAll()
		{
			for (int i = 0; i < m_arrayIAnalyticObject.Length; i++)
			{
				m_arrayIAnalyticObject[i].AnalyticDirty = false;
			}
		}
		
		protected abstract void Send(IAnalyticObject analyticObject);
	}
}
#endif