using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FIMSpace.Generating
{
    public enum EPlanGuideDirecion { None, Left, Right, Forward, Back, Up, Down }
    public enum EFlatDirecion { Left, Right, Forward, Back }
    public enum EHelperGuideType { Doors, ClearWall, Spawn }
    public enum EAlignDir { Up, Down, Left, Right }

    public enum ESP_OffsetMode 
    { 
        [Tooltip("Override previous offsets with this value")]
        OverrideOffset,
        [Tooltip("Add offsets to already applied offset in this spawn")]
        AdditiveOffset
    }

    public enum ESP_OffsetSpace 
    { 
        [Tooltip("Offset without ingerention of parent or current rotation")]
        WorldSpace, 
        [Tooltip("Offset with ingerention of parent or current rotation")]
        LocalSpace
    }

    public enum ESP_GetMode
    {
        [Tooltip("Get local/world offset separately")]
        Separate,
        [Tooltip("Sum local/world offset into one offset value")]
        Sum
    }

    public static class PGGEnums
    {
        public static Vector3 GetDirection(this EPlanGuideDirecion dir)
        {
            switch (dir)
            {
                case EPlanGuideDirecion.Left: return Vector3.left;
                case EPlanGuideDirecion.Right: return Vector3.right;
                case EPlanGuideDirecion.Forward: return Vector3.forward;
                case EPlanGuideDirecion.Back: return Vector3.back;
                case EPlanGuideDirecion.Up: return Vector3.up;
                case EPlanGuideDirecion.Down: return Vector3.down;
            }

            return Vector3.zero;
        }

        public static Vector2Int GetDirection2D(this EPlanGuideDirecion dir)
        {
            switch (dir)
            {
                case EPlanGuideDirecion.Left: return Vector2Int.left;
                case EPlanGuideDirecion.Right: return Vector2Int.right;
                case EPlanGuideDirecion.Back: return Vector2Int.down;
                case EPlanGuideDirecion.Forward: return Vector2Int.up;
            }

            return Vector2Int.zero;
        }

        public static Vector2Int GetDirection2D(this EFlatDirecion dir)
        {
            switch (dir)
            {
                case EFlatDirecion.Left: return Vector2Int.left;
                case EFlatDirecion.Right: return Vector2Int.right;
                case EFlatDirecion.Back: return Vector2Int.down;
                case EFlatDirecion.Forward: return Vector2Int.up;
            }

            return Vector2Int.zero;
        }
    }

}
