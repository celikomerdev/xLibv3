﻿#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtTexture2D
	{
		public static Texture2D Uncompress(this Texture2D value)
		{
			Texture2D texture2D = new Texture2D(value.width,value.height);
			if(value==null)
			{
				Debug.LogException(new UnityException($"ExtTexture2D.Uncompress:null"));
				return texture2D;
			}
			
			RenderTexture renderTexture = RenderTexture.GetTemporary
			(
				value.width,
				value.height,
				0,
				RenderTextureFormat.Default,
				RenderTextureReadWrite.Linear
			);
			Graphics.Blit(value,renderTexture);
			
			RenderTexture activeLast = RenderTexture.active;
			RenderTexture.active = renderTexture;
			
			texture2D.ReadPixels(new Rect(0,0,renderTexture.width,renderTexture.height),0,0);
			texture2D.Apply();
			
			RenderTexture.active = activeLast; //TODO FATAL
			RenderTexture.ReleaseTemporary(renderTexture);
			RenderTexture.active = activeLast;
			
			return texture2D;
		}
		
		public static byte[] xEncodeToPNG(this Texture2D value)
		{
			byte[] bytes = new byte[0];
			if(value==null)
			{
				Debug.LogException(new UnityException($"ExtTexture2D.xEncodeToPNG:null"));
				return bytes;
			}
			
			Texture2D texture2D = value.Uncompress();
			
			#if ModImageConversion
			bytes = texture2D.EncodeToPNG();
			#else
			xDebug.LogTempFormat("!ModImageConversion");
			// bytes = texture2D.GetRawTextureData();
			#endif
			
			texture2D.Compress(true);
			texture2D.Apply(true);
			return bytes;
		}
		
		public static void Load(this Texture2D value,byte[] data)
		{
			if(data==null) return;
			if(data.Length<0) return;
			
			#if ModImageConversion
			value.LoadImage(data);
			#else
			xDebug.LogTempFormat("!ModImageConversion");
			// value.LoadRawTextureData(data);
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