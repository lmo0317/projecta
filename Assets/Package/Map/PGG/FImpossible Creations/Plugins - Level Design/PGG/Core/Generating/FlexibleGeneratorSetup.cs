using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Generating
{
    [System.Serializable]
    public class FlexibleGeneratorSetup
    {
        /// <summary> Target FieldSetup for generating (project file) </summary>
        public FieldSetup FieldPreset;
        /// <summary> Instante of field setup to avoid changes in project file </summary>
        public FieldSetup RuntimeFieldSetup { get { return Preparation.RuntimeFieldSetup; } }
        /// <summary> Initial preparation settings for the FieldSetup to be generated </summary>
        public GeneratingPreparation Preparation;
        /// <summary> Controlling grid cells updating with FieldSetup's rules </summary>
        public CellsController CellsController;
        /// <summary> Instantiating manager for controlled generating objects on the scene </summary>
        public InstantiatedFieldInfo InstantiatedInfo;

        internal void Initialize(MonoBehaviour g)
        {
            if (Preparation == null)
            {
                Preparation = new GeneratingPreparation();
                if (CellsController == null) CellsController = new CellsController();
                if (InstantiatedInfo == null) InstantiatedInfo = new InstantiatedFieldInfo();

                Preparation.Initialize(this);
                CellsController.Initialize(this);
                InstantiatedInfo.Initialize(this);
                InstantiatedInfo.SetupContainer(g.transform);
            }

            RefreshReferences(g);
        }

        public void RefreshReferences(MonoBehaviour g)
        {
            Preparation.RefreshReferences(this);
            CellsController.RefreshReferences(this);
            InstantiatedInfo.RefreshReferences(this);
            InstantiatedInfo.SetupContainer(g.transform);
            RefreshRuntimeFieldSetup();
        }

        public void RefreshRuntimeFieldSetup()
        {
            if (FieldPreset != null) Preparation.SyncGeneringWith(FieldPreset);
        }

    }
}