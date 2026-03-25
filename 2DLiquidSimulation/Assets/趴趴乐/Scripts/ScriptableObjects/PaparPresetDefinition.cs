using UnityEngine;

namespace Papar.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "PaparPresetDefinition",
        menuName = "Papar/ScriptableObjects/Papar Preset Definition")]
    public class PaparPresetDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string presetId = "preset_default";
        public string displayName = "Default Preset";
        [TextArea]
        public string description;
        public Sprite previewSprite;

        [Header("Preset References")]
        public BoxShapeDefinition boxShapeDefinition;
        public BoxStyleDefinition boxStyleDefinition;
        public FrameStyleDefinition frameStyleDefinition;
        public PetDefinition petDefinition;

        [Header("Default Selections")]
        public string defaultBoxColorId = "default";
        public string defaultPetSkinId = "default";
        public PetFacingType defaultFacing = PetFacingType.Front;
    }
}
