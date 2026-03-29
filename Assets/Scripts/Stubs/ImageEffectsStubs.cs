// Stub types for legacy Unity Standard Assets Image Effects.
// These types no longer ship with Unity 6. The stubs provide
// compile-time compatibility so that existing gameplay and editor
// scripts can reference the types without error.
//
// None of these stubs perform real rendering. They exist solely to
// allow the project to compile during the migration.

using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    // ---------------------------------------------------------------
    // BlurOptimized  (used by ViewCameraControler, Output)
    // ---------------------------------------------------------------
    public class BlurOptimized : MonoBehaviour
    {
        public int blurIterations;
        public float blurSize;
    }

    // ---------------------------------------------------------------
    // Grayscale  (used by PlayerDeathEffect, PeCameraImageEffect)
    // ---------------------------------------------------------------
    public class Grayscale : MonoBehaviour
    {
        public float rampOffset;
        public float saturate;
    }

    // ---------------------------------------------------------------
    // BloomAndFlares  (used by PlayerDeathEffect, BloomAndFlaresEditor)
    // ---------------------------------------------------------------
    public class BloomAndFlares : MonoBehaviour
    {
        public float bloomThreshold;
    }

    // ---------------------------------------------------------------
    // MotionBlur  (used by PlayerShakeEffect, PeCameraImageEffect)
    // ---------------------------------------------------------------
    public class MotionBlur : MonoBehaviour
    {
        public float blurAmount;
    }

    // ---------------------------------------------------------------
    // Antialiasing  (used by UIOption, PECameraMan, AntialiasingEditor)
    // ---------------------------------------------------------------
    public class Antialiasing : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // DepthOfField  (used by PECameraMan, DepthOfFieldEditor)
    // ---------------------------------------------------------------
    public class DepthOfField : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // BloomOptimized  (used by PeCameraImageEffect)
    // ---------------------------------------------------------------
    public class BloomOptimized : MonoBehaviour
    {
        public float intensity;
    }

    // ---------------------------------------------------------------
    // GlobalFog  (used by RandomMapConfig, Output)
    // ---------------------------------------------------------------
    public class GlobalFog : MonoBehaviour
    {
        public float height;
    }

    // ---------------------------------------------------------------
    // SunShafts  (used by Output, SunShaftsEditor)
    // ---------------------------------------------------------------
    public class SunShafts : MonoBehaviour
    {
        public Transform sunTransform;
        public Color sunColor;
        public float sunShaftIntensity;
    }

    // ---------------------------------------------------------------
    // Bloom  (used by Output, BloomEditor)
    // ---------------------------------------------------------------
    public class Bloom : MonoBehaviour
    {
        public float bloomIntensity;
        public float bloomThreshold;
    }

    // ---------------------------------------------------------------
    // CameraMotionBlur  (used by CameraMotionBlurEditor)
    // ---------------------------------------------------------------
    public class CameraMotionBlur : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // ColorCorrectionCurves  (used by ColorCorrectionCurvesEditor)
    // ---------------------------------------------------------------
    public class ColorCorrectionCurves : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // ColorCorrectionLookup  (used by ColorCorrectionLookupEditor)
    // ---------------------------------------------------------------
    public class ColorCorrectionLookup : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // DepthOfFieldDeprecated  (used by DepthOfFieldDeprecatedEditor)
    // ---------------------------------------------------------------
    public class DepthOfFieldDeprecated : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // EdgeDetection  (used by EdgeDetectionEditor)
    // ---------------------------------------------------------------
    public class EdgeDetection : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // NoiseAndGrain  (used by NoiseAndGrainEditor)
    // ---------------------------------------------------------------
    public class NoiseAndGrain : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // Tonemapping  (used by TonemappingEditor)
    // ---------------------------------------------------------------
    public class Tonemapping : MonoBehaviour
    {
    }

    // ---------------------------------------------------------------
    // VignetteAndChromaticAberration  (used by VignetteAndChromaticAberrationEditor)
    // ---------------------------------------------------------------
    public class VignetteAndChromaticAberration : MonoBehaviour
    {
    }
}
