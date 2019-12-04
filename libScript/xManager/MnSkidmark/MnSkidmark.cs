#if xLibv2
using xLib;
using UnityEngine;

namespace xLib
{
	public class MnSkidmark : SingletonM<MnSkidmark>
	{
		public MeshFilter meshFilter;
		private Mesh mesh;
		public MeshRenderer meshRenderer;
		
		public int maxSection = 1024;
		public float width = 0.35f;
		public float offsetY = 0.02f;
		public float distanceMinSqr = 0.01f;
		
		private int indexSection;
		private Section[] section;
		[System.Serializable]private class Section
		{
			public Vector3 pos = Vector3.zero;
			public Vector3 normal = Vector3.zero;
			public Vector4 tangent = Vector4.zero;
			public Vector3 posL = Vector3.zero;
			public Vector3 posR = Vector3.zero;
			public byte intensity;
			public int lastIndex;
		}
		
		private MeshClass meshClass = new MeshClass();
		[System.Serializable]private class MeshClass
		{
			public Vector4[] tangents;
			public Vector3[] normals;
			public Vector3[] vertices;
			public Color32[] colors32;
			public Vector2[] uv;
			public int[] triangles;
		}
		
		private bool updated;
		private bool haveSetBounds;
		
		protected override void Started()
		{
			Init();
		}
		
		public override void Init()
		{
			base.Init();
			section = new Section[maxSection];
			for (int i = 0; i<maxSection; i++)
			{
				section[i] = new Section();
			}
			
			mesh = new Mesh();
			mesh.MarkDynamic();
			meshFilter.sharedMesh = mesh;
			
			meshClass.vertices = new Vector3[maxSection*4];
			meshClass.normals = new Vector3[maxSection*4];
			meshClass.tangents = new Vector4[maxSection*4];
			meshClass.colors32 = new Color32[maxSection*4];
			for (int i = 0; i < meshClass.colors32.Length; i++)
			{
				meshClass.colors32[i] = Color.white;
			}
			meshClass.uv = new Vector2[maxSection*4];
			meshClass.triangles = new int[maxSection*6];
		}
		
		private void LateUpdate()
		{
			if (!updated) return;
			updated = false;
			
			mesh.Clear();
			mesh.vertices	= meshClass.vertices;
			mesh.normals	= meshClass.normals;
			mesh.tangents	= meshClass.tangents;
			mesh.triangles	= meshClass.triangles;
			mesh.colors32	= meshClass.colors32;
			mesh.uv			= meshClass.uv;
			
			// if (!haveSetBounds)
			// {
			// 	mesh.bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(10000, 10000, 10000));
			// 	haveSetBounds = true;
			// }
			
			meshFilter.sharedMesh = mesh;
		}
		
		public int AddSkidMark(Vector3 pos, Vector3 normal, float intensity, int indexLast)
		{
			if (intensity>1) intensity = 1.0f;
			else if (intensity<=0) return -1;
			
			if (indexLast>0)
			{
				float sqrDistance = (pos-section[indexLast].pos).sqrMagnitude;
				if (sqrDistance<distanceMinSqr) return indexLast;
			}
			
			Section sectionCurrent = section[indexSection];
			
			sectionCurrent.pos = pos + normal*offsetY;
			sectionCurrent.normal = normal;
			sectionCurrent.intensity = (byte)(intensity*255f);
			sectionCurrent.lastIndex = indexLast;
			
			if (indexLast != -1)
			{
				Section sectionPrevios = section[indexLast];
				Vector3 direction = (sectionCurrent.pos - sectionPrevios.pos);
				Vector3 directionCross = Vector3.Cross(direction,normal).normalized;
				
				sectionCurrent.posL = sectionCurrent.pos + directionCross*width*0.5f;
				sectionCurrent.posR = sectionCurrent.pos - directionCross*width*0.5f;
				sectionCurrent.tangent = new Vector4(directionCross.x,directionCross.y,directionCross.z,1);
				
				if (sectionPrevios.lastIndex == -1)
				{
					sectionPrevios.tangent = sectionCurrent.tangent;
					sectionPrevios.posL = sectionCurrent.pos + directionCross*width*0.5f;
					sectionPrevios.posR = sectionCurrent.pos - directionCross*width*0.5f;
				}
			}
			
			UpdateSkidmarksMesh();
			
			int indexCurrent = indexSection;
			indexSection = ++indexSection % maxSection;
			
			return indexCurrent;
		}
		
		private void UpdateSkidmarksMesh()
		{
			Section sectionCurrent = section[indexSection];
			if (sectionCurrent.lastIndex == -1) return;
			Section sectionPrevios = section[sectionCurrent.lastIndex];
			
			meshClass.vertices[indexSection*4 + 0] = sectionPrevios.posL;
			meshClass.vertices[indexSection*4 + 1] = sectionPrevios.posR;
			meshClass.vertices[indexSection*4 + 2] = sectionCurrent.posL;
			meshClass.vertices[indexSection*4 + 3] = sectionCurrent.posR;
			
			meshClass.normals[indexSection*4 + 0] = sectionPrevios.normal;
			meshClass.normals[indexSection*4 + 1] = sectionPrevios.normal;
			meshClass.normals[indexSection*4 + 2] = sectionCurrent.normal;
			meshClass.normals[indexSection*4 + 3] = sectionCurrent.normal;
			
			meshClass.tangents[indexSection*4 + 0] = sectionPrevios.tangent;
			meshClass.tangents[indexSection*4 + 1] = sectionPrevios.tangent;
			meshClass.tangents[indexSection*4 + 2] = sectionCurrent.tangent;
			meshClass.tangents[indexSection*4 + 3] = sectionCurrent.tangent;
			
			meshClass.colors32[indexSection*4 + 0].a = sectionPrevios.intensity;
			meshClass.colors32[indexSection*4 + 1].a = sectionPrevios.intensity;
			meshClass.colors32[indexSection*4 + 2].a = sectionCurrent.intensity;
			meshClass.colors32[indexSection*4 + 3].a = sectionCurrent.intensity;
			
			meshClass.uv[indexSection*4 + 0] = new Vector2(0,0);
			meshClass.uv[indexSection*4 + 1] = new Vector2(1,0);
			meshClass.uv[indexSection*4 + 2] = new Vector2(0,1);
			meshClass.uv[indexSection*4 + 3] = new Vector2(1,1);
			
			meshClass.triangles[indexSection*6 + 0] = indexSection*4 + 0;
			meshClass.triangles[indexSection*6 + 2] = indexSection*4 + 1;
			meshClass.triangles[indexSection*6 + 1] = indexSection*4 + 2;
			
			meshClass.triangles[indexSection*6 + 3] = indexSection*4 + 2;
			meshClass.triangles[indexSection*6 + 5] = indexSection*4 + 1;
			meshClass.triangles[indexSection*6 + 4] = indexSection*4 + 3;
			
			updated = true;
		}
	}
}
#endif