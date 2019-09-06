#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtRenderTexture
	{
		public static Texture2D ToTexture2D(this RenderTexture renderTexture)
		{
			if(renderTexture==null)
			{
				xDebug.LogExceptionFormat("ExtRenderTexture:ToTexture2D:null");
				return null;
			}
			
			RenderTexture activeLast = RenderTexture.active;
			RenderTexture.active = renderTexture;
			
			Texture2D texture2D = new Texture2D(renderTexture.width,renderTexture.height,TextureFormat.ARGB32,true);
			texture2D.ReadPixels(new Rect(0,0,renderTexture.width,renderTexture.height),0,0);
			texture2D.Compress(true);
			texture2D.Apply(true);
			
			RenderTexture.active = activeLast;
			
			return texture2D;
		}
	}
}
#endif