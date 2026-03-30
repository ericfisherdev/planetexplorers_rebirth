using UnityEngine;
using System.Collections;
using MotionBlur = UnityStandardAssets.ImageEffects.MotionBlur;

public class PlayerShakeEffect : MonoBehaviour 
{
	public static bool bShaking = false;
//	float fOriginalFOV = 45;
//	float fShakeTime = 0;
	public float ShakeEffectLife = 0.25f;
//	float ShakeRange = 1.0f;
//	float ShakePos = 0;
//	float LastHP = -500;
	
//	PlayerDeathEffect GrayEffect = null;
	
	// Use this for initialization
	void Start () 
	{
		bShaking = false;
//		fOriginalFOV = 0;
//		fShakeTime = 0;
	}
	
	void Shake()
	{
//		if ( !bShaking )
//			fOriginalFOV = GetComponent<Camera>().fieldOfView;
		bShaking = true;
//		fShakeTime = 0;
		MotionBlur mb = gameObject.GetComponent<MotionBlur>();
		if ( mb == null )
			return;
		mb.enabled = true;
	}
}
