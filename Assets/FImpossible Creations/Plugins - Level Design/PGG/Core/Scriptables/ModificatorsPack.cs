using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Generating
{
    /// <summary>
    /// It's never sub-asset except being FieldSetup root modification package
    /// Most of the code is inside Editor class of ModificatorsPack
    /// </summary>
    [CreateAssetMenu(fileName = "ModPack_", menuName = "FImpossible Creations/Procedural Generation/Grid Field Modifications Pack", order = 201)]
    public partial class ModificatorsPack : ScriptableObject
    {
        [HideInInspector] public List<FieldModification> FieldModificators = new List<FieldModification>();
        [HideInInspector] public FieldSetup ParentPreset;
        public static bool _Editor_LockBrowser = false;
        public bool DisableWholePackage = false;
        public ESeedMode SeedMode = ESeedMode.None;

        public enum ESeedMode { None, Reset, Custom }
        public int CustomSeed = 0;

        public enum EModPackType { Base, ShallowCopy, VariantCopy }
        public EModPackType ModPackType = EModPackType.Base;

        [Tooltip("WARNING: You need to know what you're doing!\nAdding tag for all spawners executed by package")]
        public string TagForAllSpawners = "";


        [HideInInspector] public List<FieldVariable> Variables = new List<FieldVariable>();

        /// <summary> Required container for 'CallOnAllSpawners' Spawner </summary>
        [HideInInspector] public FieldModification CallOnAllMod;
        [HideInInspector] public FieldSpawner CallOnAllSpawners;
        [HideInInspector] public bool _EditorDisplayCallOnAll = false;

        internal FieldVariable GetVariable(string name)
        {
            for (int i = 0; i < Variables.Count; i++)
                if (Variables[i].Name == name) return Variables[i];

            return null;
        }

        internal void PrepareSeed()
        {
            if (SeedMode != ModificatorsPack.ESeedMode.None)
            {
                if (SeedMode == ModificatorsPack.ESeedMode.Reset)
                {
                    FGenerators.SetSeed(FGenerators.LatestSeed);
                }
                else if (SeedMode == ModificatorsPack.ESeedMode.Custom)
                {
                    FGenerators.SetSeed(CustomSeed);
                }
            }
        }

        internal List<FieldModification> GetModListToRun(GeneratingPreparation preparation)
        {
            List<FieldModification> mods = new List<FieldModification>();

            for (int r = 0; r < FieldModificators.Count; r++)
            {
                if (FieldModificators[r] == null) continue;
                if (FieldModificators[r].Enabled == false) continue;
                if (preparation.IgnoredModificationsForGenerating.Contains(FieldModificators[r])) continue;
                mods.Add(FieldModificators[r]);
            }

            return mods;
        }
    }
}