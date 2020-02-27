#if xLibv3
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace xLib
{
	[CreateAssetMenu(menuName = "xLib/Helper/Keystore",order = 99)]
	public class HelperKeystore : BaseWorkS
	{
		[SerializeField]private string keystorePass = "keystorePass";
		[SerializeField]private string keyaliasPass = "keyaliasPass";
		
		protected override void OnValidated()
		{
			EditKeystore();
		}
		
		private void EditKeystore()
		{
			EditorPrefs.SetString("keystorePass",keystorePass);
			EditorPrefs.SetString("keyaliasPass",keyaliasPass);
			FillKeystore();
		}
		
		[InitializeOnLoadMethod]
		private static void FillKeystore()
		{
			Debug.Log($"HelperKeystore:FillKeystore");
			PlayerSettings.Android.keystorePass = EditorPrefs.GetString("keystorePass","");
			PlayerSettings.Android.keyaliasPass = EditorPrefs.GetString("keyaliasPass","");
		}
	}
}
#endif
#endif