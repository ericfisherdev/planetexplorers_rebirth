using UnityEngine;
using System.Collections;


namespace GraphMapping
{
	//[ExecuteInEditMode]
	public class EditMapping : MonoBehaviour
	{
		public Texture2D mBiomeTex;
		public Texture2D mHeightTex;
		public Texture2D mAiSpawnTex;
		public bool mSaveData = false;

		public bool mTest =false;
		public Vector2 testPos = new Vector2(2000,2000);
		public Vector2 testWorldSize = new Vector2(18432,18432);

		bool SaveData()
		{
			PeMappingMgr.Instance.mBiomeMap.LoadTexData(mBiomeTex);
			PeMappingMgr.Instance.mHeightMap.LoadTexData(mHeightTex);
			PeMappingMgr.Instance.mAiSpawnMap.LoadTexData(mAiSpawnTex);
			return PeMappingMgr.Instance.SaveFile("D:/PeGraphMapping/");
		}
		void Awake()
		{
			PeMappingMgr.Instance.Init(testWorldSize);
		}

		void Update()
		{
			if (mSaveData)
			{
				bool ok = SaveData();
				string info = ok ? "ReLoad texture suceess!" : "ReLoad texture failed!";
				Debug.Log (info);
				mSaveData = false;
			}

			else if(mTest)
			{
				int  id  = PeMappingMgr.Instance.mAiSpawnMap.GetAiSpawnMapId(testPos,testWorldSize);
				Debug.Log(id);
				mTest = false ;
			}
		}
	}
}