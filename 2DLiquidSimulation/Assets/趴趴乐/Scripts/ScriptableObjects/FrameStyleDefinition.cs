using UnityEngine;

namespace Papar.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "FrameStyleDefinition",
        menuName = "Papar/ScriptableObjects/Frame Style Definition")]
    public class FrameStyleDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string styleId = "frame_style_default";
        public string displayName = "Default Frame Style";
        [TextArea]
        public string description;

        [Header("Visual")]
        public Sprite previewSprite;
        public Sprite frameFrontSprite;
        public Sprite frameBackSprite;
        public Color frameTint = Color.white;

        [Header("Shape Tuning")]
        [Min(0.01f)]
        public float frameThickness = 0.1f;
        [Min(0f)]
        public float cornerRadius;
        public bool useBackFrame = true;
    }
}
