#if xLibv3
namespace xLib.ToolObject
{
	public class DontDestroyOnLoad : BaseM
	{
		public bool dontDestroy = true;
		private void Awake()
		{
			if(dontDestroy) DontDestroyOnLoad(gameObject);
		}
	}
}
#endif