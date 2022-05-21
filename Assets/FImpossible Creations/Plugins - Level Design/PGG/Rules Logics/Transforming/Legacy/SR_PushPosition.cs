#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace FIMSpace.Generating.Rules.Transforming.Legacy
{
    public class SR_PushPosition : SpawnRuleBase, ISpawnProcedureType
    {
        public override string TitleName() { return "Push Position"; }
        public override string Tooltip() { return "Offsetting spawn position like DirectOffset but in additive way"; }
        public EProcedureType Type { get { return EProcedureType.Event; } }

        public Vector3 Offset = Vector3.zero;
        public Vector3 RandomOffset = Vector3.zero;


        public override void CellInfluence(FieldSetup preset, FieldModification mod, FieldCell cell, ref SpawnData spawn, FGenGraph<FieldCell, FGenPoint> grid)
        {
            Quaternion rot = spawn.GetRotationOffset();
            Vector3 offset = Offset;

            if (RandomOffset != Vector3.zero)
            {
                Vector3 rOffset = new Vector3
                (
                    FGenerators.GetRandom(-RandomOffset.x, RandomOffset.x),
                    FGenerators.GetRandom(-RandomOffset.y, RandomOffset.y),
                    FGenerators.GetRandom(-RandomOffset.z, RandomOffset.z)
                );

                offset += rOffset;
            }

            spawn.Offset += rot * offset;
            spawn.TempPositionOffset += offset;
        }
    }
}