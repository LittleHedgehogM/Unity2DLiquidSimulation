using UnityEngine;

namespace Papar.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "BoxShapeDefinition",
        menuName = "Papar/ScriptableObjects/Box Shape Definition")]
    public class BoxShapeDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string shapeId = "shape_standard";
        public string displayName = "Standard Long Box";
        [TextArea]
        public string description;

        [Header("Shape")]
        public PaparBoxShapeType shapeType = PaparBoxShapeType.StandardLongBox;
        public Vector2Int footprint = new Vector2Int(1, 1);
        [Min(0.1f)]
        public float depth = 2f;
        [Min(0.1f)]
        public float frameOpeningScale = 1f;

        [Header("Behavior")]
        public bool supportsFrontFacingPet = true;
        public bool supportsBackFacingPet = true;
        public bool supportsStacking = true;

        [Header("Preview")]
        public Sprite previewSprite;
    }
}
