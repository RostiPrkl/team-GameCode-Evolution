using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames.AudioController
{
    [System.Serializable]
    public class SoundSettings
    {
        [Range(0, 5)] public float dopplerLevel = 1;
        [Range(0, 360)] public float spread = 0;
        public AudioRolloffMode volumeRolloff = AudioRolloffMode.Custom;
        public float minDistance = 1;
        public float maxDistance = 500;
        
        [Space, CustomCurve(223, 179, 000, 255)]
        public AnimationCurve customRolloff = new AnimationCurve(
            new Keyframe(0, 1),
            new Keyframe(2, 0.500f),
            new Keyframe(4, 0.250f),
            new Keyframe(8, 0.125f),
            new Keyframe(16, 0.063f),
            new Keyframe(32, 0.031f),
            new Keyframe(64, 0.016f),
            new Keyframe(128, 0.008f),
            new Keyframe(256, 0.004f),
            new Keyframe(512, 0.002f)
        );
        
    } // class end
}
