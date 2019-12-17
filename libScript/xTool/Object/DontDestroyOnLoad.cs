#if xLibv3
namespace xLib.ToolObject
{
	public class DontDestroyOnLoad : BaseMainM
	{
		public bool dontDestroy = true;
		private void Awake()
		{
			if(dontDestroy) DontDestroyOnLoad(gameObject);
		}
	}
}
#endif