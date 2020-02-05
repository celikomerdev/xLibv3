#if xLibv3
namespace xLib
{
	public abstract class BaseRegisterM : BaseActiveM,BaseRegisterI
	{
		#region Object
		[System.Serializable]
		public class BaseRegisterInfo
		{
			public int order;
			public bool onRegister;
			internal bool isRegister;
		}
		public BaseRegisterInfo baseRegister = new BaseRegisterInfo();
		
		public int Order
		{
			get
			{
				return baseRegister.order;
			}
			set
			{
				baseRegister.order = value;
			}
		}
		
		public bool OnRegister
		{
			get
			{
				return baseRegister.onRegister;
			}
			set
			{
				baseRegister.onRegister = value;
			}
		}
		#endregion
		
		
		#region Custom
		protected override void OnActive(bool value)
		{
			if(CanDebug) xLogger.LogFormat(this,this.name+":OnActive:{0}",value);
			IsRegister = value;
		}
		
		protected abstract bool TryRegister(bool register);
		public bool IsRegister
		{
			get
			{
				return baseRegister.isRegister;
			}
			set
			{
				if(baseRegister.isRegister == value) return;
				
				FindView();
				string tempViewId = ViewCore.CurrentId;
				ViewCore.CurrentId = ViewId;
				
				baseRegister.isRegister = TryRegister(value);
				if(CanDebug) xLogger.LogFormat(this,this.name+":IsRegister:{0}",baseRegister.isRegister);
				
				ViewCore.CurrentId = tempViewId;
			}
		}
		#endregion
		
		
		#region Public
		public void ForceRegister()
		{
			IsRegister = false;
			IsRegister = true;
		}
		
		public void ForceExecute()
		{
			IsRegister = !baseRegister.isRegister;
			IsRegister = !baseRegister.isRegister;
		}
		#endregion
		
		protected override void OnDestroyed()
		{
			IsRegister = false;
		}
	}
}
#endif