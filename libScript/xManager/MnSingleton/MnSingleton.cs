#if xLibv3
namespace xLib
{
	public class MnSingleton : SingletonM<MnSingleton>
	{
		protected override void Awaked()
		{
			// System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
		}
	}
}
#endif