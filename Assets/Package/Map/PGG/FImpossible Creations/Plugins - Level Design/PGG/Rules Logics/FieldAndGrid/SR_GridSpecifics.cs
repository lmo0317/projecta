#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace FIMSpace.Generating.Rules.FieldAndGrid
{
    public class SR_GridSpecifics : SpawnRuleBase, ISpawnProcedureType
    {
        public override string TitleName() { return "Grid Specifics"; }
        public override string Tooltip() { return "Allow or not allow to spawn when some grid specific value is correct"; }
        public EProcedureType Type { get { return EProcedureType.Rule; } }

        public enum EGridSpec
        {
            GridSizeIsEven
        }

        public EGridSpec Condition = EGridSpec.GridSizeIsEven;

        public enum EGridAxis
        {
            X, Y, Z, XYZ, XZ
        }

        public EGridAxis ConditionMetAxis = EGridAxis.Z;


        #region Editor Stuff

#if UNITY_EDITOR
        public override void NodeBody(SerializedObject so)
        {
            base.NodeBody(so);
        }
#endif

        #endregion


        public override void CheckRuleOn(FieldModification mod, ref SpawnData spawn, FieldSetup preset, FieldCell cell, FGenGraph<FieldCell, FGenPoint> grid, Vector3? restrictDirection = null)
        {
            base.CheckRuleOn(mod, ref spawn, preset, cell, grid, restrictDirection);
            CellAllow = true;

            if ( Condition == EGridSpec.GridSizeIsEven)
            {
                if ( ConditionMetAxis == EGridAxis.X) CellAllow = (grid.Width % 2 == 0);
                else if( ConditionMetAxis == EGridAxis.Y) CellAllow = (grid.Height % 2 == 0);
                else if( ConditionMetAxis == EGridAxis.Z) CellAllow = (grid.Depth % 2 == 0);
                else if( ConditionMetAxis == EGridAxis.XZ) CellAllow = (grid.Depth % 2 == 0) && (grid.Width % 2 == 0);
                else if( ConditionMetAxis == EGridAxis.XYZ) CellAllow = (grid.Depth % 2 == 0) && (grid.Height % 2 == 0) && (grid.Width % 2 == 0);
            }
        }
    }
}