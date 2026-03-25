using System.Collections.Generic;
using UnityEngine;

namespace Papar.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "BoxStyleDefinition",
        menuName = "Papar/ScriptableObjects/Box Style Definition")]
    public class BoxStyleDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string styleId = "box_style_default";
        public string displayName = "Default Box Style";
        [TextArea]
        public string description;

        [Header("Visual")]
        public Sprite previewSprite;
        public Color baseTint = Color.white;
        public bool useTransparency;
        [Range(0f, 1f)]
        public float alpha = 1f;

        [Header("Surfaces")]
        public Sprite topSprite;
        public Sprite outerSideSprite;
        public Sprite innerSideSprite;
        public Sprite bottomSprite;

        [Header("Color Variants")]
        public List<BoxColorOption> colorOptions = new List<BoxColorOption>();
    }
}
