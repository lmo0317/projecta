using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Generating.Checker
{
    /// <summary>
    /// Just vector 2 int for graph fast usage
    /// </summary>
    [System.Serializable]
    public class CheckerPos
    {
        public int x;
        public int y;
        public bool approved;

        public CheckerPos()
        {
            x = 0; y = 0; approved = false;
        }

        public CheckerPos(int x, int y)
        {
            this.x = x; this.y = y; approved = true;
        }

        public Vector2Int ToV2()
        {
            return new Vector2Int(x, y);
        }  
        
        public Vector3 ToV3(float yLevel = 0)
        {
            return new Vector3(x, yLevel, y);
        }
    }
}