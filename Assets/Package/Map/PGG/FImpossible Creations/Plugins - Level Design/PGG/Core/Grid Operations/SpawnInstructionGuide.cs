using UnityEngine;

namespace FIMSpace.Generating
{
    [System.Serializable]
    public class SpawnInstructionGuide
    {
        public Vector3Int pos;
        public Quaternion rot;
        public int Id;
        public InstructionDefinition CustomDefinition;
        public bool UseDirection = false;

        internal SpawnInstruction GenerateGuide(FieldSetup preset, SpawnRestrictionsGroup group)
        {
            SpawnInstruction i = new SpawnInstruction();

            if (CustomDefinition == null)
            {
                i.definition = group.Restriction.GetSpawnInstructionDefinition(preset);
            }
            else
            {
                i.definition = CustomDefinition;
            }

            i.gridPosition = pos;
            i.desiredDirection = PGGUtils.V3toV3Int(rot * Vector3.forward);
            i.useDirection = UseDirection;

            return i;
        }
    }
}