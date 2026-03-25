using System.Collections.Generic;
using UnityEngine;

namespace Papar.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "PetDefinition",
        menuName = "Papar/ScriptableObjects/Pet Definition")]
    public class PetDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string petTypeId = "pet_default";
        public string displayName = "Default Pet";
        [TextArea]
        public string description;

        [Header("Preview")]
        public Sprite previewSprite;

        [Header("Facing Roots")]
        public GameObject frontFacingPrefab;
        public GameObject backFacingPrefab;
        public PetFacingType defaultFacing = PetFacingType.Front;

        [Header("Animation")]
        public RuntimeAnimatorController animatorController;
        public bool supportsFrontFacing = true;
        public bool supportsBackFacing = true;

        [Header("Skin Options")]
        public List<PetSkinOption> skinOptions = new List<PetSkinOption>();
    }
}
