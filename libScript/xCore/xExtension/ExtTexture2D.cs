#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtTexture2D
	{
		public static Texture2D Decompress(this Texture2D value)
		{
			Texture2D texture2D = new Texture2D(value.width,value.height);
			if(value==null)
			{
				Debug.LogException(new UnityException("ExtTexture2D.Decompress:null"));
				return texture2D;
			}
			
			if(value.format == texture2D.format)
			{
				if(xLogger.CanDebug) Debug.Log($"Decompress:Already:{value.name}",value);
				return value;
			}
			
			//Please note that due to API limitations, this function is not supported on DX9 or Mac+OpenGL.
			// Graphics.ConvertTexture(texture2D,value);
			
			texture2D.SetPixels32(value.GetPixels32());
			texture2D.Apply();
			
			// last option
			// RenderTexture renderTexture = RenderTexture.GetTemporary(value.width,value.height,0,RenderTextureFormat.ARGB32);
			// Graphics.Blit(value,renderTexture);
			// texture2D = renderTexture.ToTexture2D(compress:false);
			// RenderTexture.ReleaseTemporary(renderTexture);
			
			return texture2D;
		}
		
		public static byte[] xEncodeToPNG(this Texture2D value)
		{
			byte[] bytes = new byte[0];
			if(value==null)
			{
				Debug.LogException(new UnityException("ExtTexture2D.xEncodeToPNG:null"));
				return bytes;
			}
			
			#if ModImageConversion
			Texture2D valueRaw = value.Decompress();
			bytes = valueRaw.EncodeToPNG();
			valueRaw = null;
			#else
			Debug.LogWarning($"!ModImageConversion");
			// bytes = value.GetRawTextureData(); //TODO
			#endif
			
			return bytes;
		}
		
		public static void Load(this Texture2D value,byte[] data)
		{
			if(data==null) return;
			if(data.Length<0) return;
			
			#if ModImageConversion
			value.LoadImage(data);
			#else
			Debug.LogWarning($"!ModImageConversion");
			// value.LoadRawTextureData(data); //TODO
			#endif
			
			if(value==null) return;
			value.Compress(true);
			value.Apply(true);
		}
		
		public static Sprite ToSprite(this Texture2D value)
		{
			return Sprite.Create(value,new Rect(0.0f,0.0f,value.width,value.height),new Vector2(0.5f,0.5f));
		}
	}
}
#endif