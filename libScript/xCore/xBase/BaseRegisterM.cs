#if xLibv3
namespace xLib
{
	public abstract class BaseRegisterM : BaseActiveM
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
		#endregion
		
		
		#region Custom
		protected override void OnActive(bool value)
		{
			if(CanDebug) xDebug.LogTempFormat(this,this.name+":OnActive:{0}",value);
			IsRegister = value;
		}
		
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
				string tempId = ViewCore.CurrentId;
				ViewCore.CurrentId = ViewId;
				
				baseRegister.isRegister = OnRegister(value);
				if(CanDebug) xDebug.LogTempFormat(this,this.name+":IsRegister:{0}",baseRegister.isRegister);
				
				ViewCore.CurrentId = tempId;
			}
		}
		
		protected virtual bool OnRegister(bool value)
		{
			return value;
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