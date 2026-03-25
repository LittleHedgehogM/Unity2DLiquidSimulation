using System;
using UnityEngine;

namespace Papar.ScriptableObjects
{
    public enum PaparBoxShapeType
    {
        StandardLongBox = 0,
        EggCone = 1,
        SweetRing = 2,
        Custom = 99
    }

    public enum PetFacingType
    {
        Front = 0,
        Back = 1
    }

    [Serializable]
    public class BoxColorOption
    {
        public string colorId = "default";
        public string displayName = "Default";
        public Color color = Color.white;
    }

    [Serializable]
    public class PetSkinOption
    {
        public string skinId = "default";
        public string displayName = "Default";
        public Color tintColor = Color.white;
        public Sprite previewSprite;
    }
}
