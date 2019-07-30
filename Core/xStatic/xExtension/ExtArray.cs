#if xLibv3
using System;

namespace xLib
{
	public static class ExtArray
	{
		public static T Get<T>(this T[] source,int index,T defaultValue=default(T))
		{
			if(source==null) return defaultValue;
			if(source.Length>index) return source[index];
			return defaultValue;
		}
		
		public static void Set<T>(ref T[] target,int index,T value,T defaultValue=default(T),bool initialize=true)
		{
			if(index<0) return;
			IncreaseLenght(ref target,index,defaultValue,initialize);
			target[index] = value;
		}
		
		public static T GetSet<T>(ref T[] source,int index,T defaultValue=default(T),bool setNull=true,bool initilize=true)
		{
			if(setNull) Set(ref source,index,defaultValue,defaultValue,initilize);
			return source.Get(index,defaultValue);
		}
		
		public static void IncreaseLenght<T>(ref T[] target,int index,T defaultValue=default(T),bool initialize=true)
		{
			if(index<0) return;
			if(target==null) target=new T[index+1];
			
			if(index >= target.Length)
			{
				Array.Resize(ref target,index+1);
				// T[] temp = new T[index+1];
				// MergeArray(ref temp,target);
				// target = temp;
			}
			
			if(initialize) Initalize(ref target,defaultValue);
		}
		
		public static void Initalize<T>(ref T[] target,T defaultValue=default(T))
		{
			if(target==null) target=new T[0];
			
			for(int i=0; i<target.Length; i++)
			{
				if(target[i]==null) target[i]=defaultValue;
			}
		}
		
		public static void MergeArray<T>(ref T[] target,T[] source)
		{
			if(target==null) target=new T[0];
			if(source==null) return;
			
			if(target.Length >= source.Length) source.CopyTo(target,0);
			else target = (T[])source.Clone();
		}
		
		public static int FindIndex<T>(this T[] items,Func<T,bool> predicate)
		{
			if(items==null) return -1;
			if(predicate==null) return -1;
			
			if(items.Length>0)
			{
				for(int i=0; i<items.Length; i++)
				{
					if(predicate(items[i])) return i;
				}
			}
			return -1;
		}
	}
}
#endif