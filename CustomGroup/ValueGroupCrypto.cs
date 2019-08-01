#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ValueGroupCrypto : ValueGroup
	{
		[Header("Crypto")]
		[SerializeField]private string keyEncrypt = "KeyEncrypt";
		protected string KeyEncrypt
		{
			get
			{
				return nodeSetting.Key+keyEncrypt+KeyExtra;
			}
		}
		
		[SerializeField]private xNode.NodeObject.NodeBase[] nodeEncrypt;
		private string KeyExtra
		{
			get
			{
				string tempString = "";
				for (int i = 0; i < nodeEncrypt.Length; i++)
				{
					tempString += nodeEncrypt[i].ValueToString;
				}
				return tempString;
			}
		}
	}
}
#endif