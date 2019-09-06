#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

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
		
		[SerializeField]private NodeBase[] nodeEncrypt = new NodeBase[0];
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