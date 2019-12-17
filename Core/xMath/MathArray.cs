#if xLibv2
namespace xLib.Mathx
{
	public static class MathArray
	{
		/// <summary>
		/// Aynı indexteki elemanları toplar, uzunluğu ayarlar.
		/// </summary>
		public static void SumArray(ref byte[] target,byte[] source)
		{
			ExtArray.IncreaseLenght(ref target,source.Length-1);
			
			for (byte i = 0; i < source.Length; i++)
			{
				target[i] += source[i];
			}
		}
		
		/// <summary>
		/// Arraydeki tüm elemanlara bir sayı ekler, çıkarma sonucu negatif ise 0 kalmasını sağlar.
		/// Çıkarma işlemi 65000 değeri için yapılmaz(İptal Edildi)
		/// </summary>
		public static void AddToAll(this ushort[] target,int value,bool stayPositive)
		{
			if(target==null) return;
			
			for(byte i = 0; i < target.Length; i++)
			{
				//if(target[i]==65000) continue;
				
				int result = target[i]+value;
				if(stayPositive && result < 0)
				{
					result = 0;
				}
				target[i] = (ushort)result;
			}
		}
		
		/// <summary>
		/// Arraydeki tüm elemanlara bir sayı ekler, çıkarma sonucu negatif ise 0 kalmasını sağlar.
		/// </summary>
		public static void AddToAll(this byte[] target,int value,bool stayPositive)
		{
			if(target==null) return;
			
			for(byte i = 0; i < target.Length; i++)
			{
				int result = target[i]+value;
				if(stayPositive && result < 0)
				{
					result = 0;
				}
				target[i] = (byte)result;
			}
		}
	}
}
#endif