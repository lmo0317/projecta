using System.Collections.Generic;
using UnityEngine;
// NO EXPORT

namespace FIMSpace.Generating
{
    public class FGenCell 
    {
        /// <summary> Grid space position (it's not world position) </summary>
        public Vector3Int Pos;
        public bool InTargetGridArea = false;
        public int Scaler = 1;
       
        [System.NonSerialized] public Vector3 HelperVector = Vector3.zero;
        public Vector3Int HelperDirection { get { return HelperVector.V3toV3Int(); } }


        public Vector3 WorldPos(float cellSize = 2f, float ySize = 1f) { Vector3 pos = (Vector3)Pos * cellSize * Scaler; pos.y *= ySize; return pos; }
        public Vector3 WorldPos(float xSize, float ySize, float zSize) { return new Vector3(Pos.x * xSize * Scaler, Pos.y * ySize * Scaler, Pos.z * zSize * Scaler); }
        public Vector3 WorldPos(Vector3 cellSize) { return new Vector3(Pos.x * cellSize.x * Scaler, Pos.y * cellSize.y * Scaler, Pos.z * cellSize.z * Scaler); }
        public Vector3 WorldPos(FieldSetup preset) { Vector3 cellSize = preset.GetCellUnitSize(); return new Vector3(Pos.x * cellSize.x * Scaler, Pos.y * cellSize.y * Scaler, Pos.z * cellSize.z * Scaler); }


        #region Cells Hierarchy handling

        private List<FGenCell> biggerCells;
        private List<FGenCell> subCells;

        public bool HaveScaleParentCells()
        {
            if (biggerCells == null) return false;
            if (biggerCells.Count == 0) return false;
            return true;
        }

        public List<FGenCell> GetScaleParentCells()
        {
            return biggerCells;
        }

        public void AddScaleParentCell(FGenCell cellParent)
        {
            if (biggerCells == null) biggerCells = new List<FGenCell>();
            if (biggerCells.Contains(cellParent) == false) biggerCells.Add(cellParent);
        }

        public bool HaveScaleChildCells()
        {
            if (subCells == null) return false;
            if (subCells.Count == 0) return false;
            return true;
        }

        public List<FGenCell> GetScaleChildCells()
        {
            return subCells;
        }

        public void AddScaleChildCell(FGenCell childCell)
        {
            if (subCells == null) subCells = new List<FGenCell>();
            if (subCells.Contains(childCell) == false) subCells.Add(childCell);
        }

        public void ResetCellsHierarchy()
        {
            if (biggerCells != null) biggerCells.Clear();
            biggerCells = null;
            if (subCells != null) subCells.Clear();
            subCells = null;
        }

        #endregion

    }
}
