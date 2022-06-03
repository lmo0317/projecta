using UnityEngine;
using System;

namespace FIMSpace.Generating
{
    /// <summary>
    /// Class for controlling Field Setup's variables
    /// Probably will be more extensive in future versions
    /// </summary>
    [System.Serializable]
    public partial class FieldVariable
    {
        public string Name = "Variable";
        [SerializeField] private Vector3 v3Val;
        [SerializeField] private string str;
        [SerializeField] private Material mat;
        [SerializeField] private GameObject gameObj;
        internal ModificatorsPack helperPackRef;

        [HideInInspector] public Vector3 helper = Vector3.zero;

        public FieldVariable()
        {
            Name = "None";
            ValueType = EVarType.None;
        }

        public FieldVariable(string name, float value)
        {
            Name = name;
            v3Val.x = value;
            ValueType = EVarType.Float;
        }

        public FieldVariable(FieldVariable toCopy)
        {
            Name = toCopy.Name;
            helper = toCopy.helper;
            SetValue(toCopy);
            v3Val = toCopy.v3Val;
            str = toCopy.str;
            mat = toCopy.mat;
            gameObj = toCopy.gameObj;
        }

        public float Float { get { return v3Val.x; } set { v3Val.x = value; } }

        [NonSerialized] public bool Prepared = false;

        public enum EVarType { None, Float, Bool, Material, GameObject, Vector3/*, Int, Vector2, String*/ }
        [HideInInspector] public EVarType ValueType = EVarType.None;

        public int GetIntValue() { return (int)v3Val.x; }
        public float GetFloatValue() { return v3Val.x; }
        public bool GetBoolValue() { return v3Val.x > 0; }
        public Vector2 GetVector2Value() { return new Vector2(v3Val.x, v3Val.y); }
        public Vector3 GetVector3Value() { return v3Val; }
        public string GetStringValue() { return str; }
        public Material GetMaterialRef() { return mat; }
        public GameObject GetGameObjRef() { return gameObj; }


        //public void SetValue(int value) { v3Val.x = value; ValueType = EVarType.Int; UpdateVariable(); }
        public void SetValue(float value) { v3Val.x = value; ValueType = EVarType.Float; UpdateVariable(); }
        public void SetValue(bool value) { v3Val.x = value ? 1 : 0; ValueType = EVarType.Bool; UpdateVariable(); }
        public void SetValue(Material value) { mat = value; ValueType = EVarType.Material; UpdateVariable(); }
        public void SetValue(GameObject value) { gameObj = value; ValueType = EVarType.GameObject; UpdateVariable(); }
        //public void SetValue(Vector2 value) { v3Val.x = value.x; v3Val.y = value.y; ValueType = EVarType.Vector2; UpdateVariable(); }
        public void SetValue(Vector3 value) { v3Val = value; ValueType = EVarType.Vector3; UpdateVariable(); }
        //public void SetValue(string value) { str = value; ValueType = EVarType.String; UpdateVariable(); }


        public void SetValue(object value)
        {
            if (value is int)
            {
                SetValue(Convert.ToInt32(value));
            }
            else if (value is float)
            {
                SetValue(Convert.ToSingle(value));
            }
            else if (value is bool)
            {
                SetValue(Convert.ToBoolean(value));
            }
            else if (value is Vector2)
            {
                SetValue((Vector2)value);
            }
            else if (value is Vector3)
            {
                SetValue((Vector3)value);
            }
            else if (value is string)
            {
                SetValue((string)value);
            }
            else if (value is Material)
            {
                SetValue((Material)value);
            }
            else if (value is GameObject)
            {
                SetValue((GameObject)value);
            }
            else
            {
                ValueType = EVarType.None;
            }

            UpdateVariable();
        }

        public void SetValue(FieldVariable value)
        {
            if (value == null) return;

            switch (value.ValueType)
            {
                case EVarType.Float: SetValue(value.GetFloatValue()); break;
                //case EVarType.Int: SetValue(value.GetIntValue()); break;
                case EVarType.Bool: SetValue(value.GetBoolValue()); break;
                case EVarType.Material: SetValue(value.mat); break;
                case EVarType.GameObject: SetValue(value.gameObj); break;
                    //case EVarType.Vector2: SetValue(value.GetVector2Value()); break;
                    case EVarType.Vector3: SetValue(value.GetVector3Value()); break;
                    //case EVarType.String: SetValue(value.GetStringValue()); break;
            }

            UpdateVariable();
        }

        public void UpdateVariable() { }

        public FieldVariable Copy()
        {
            FieldVariable f = (FieldVariable)MemberwiseClone();
            f.Name = Name;
            f.str = str;
            f.v3Val = v3Val;
            f.mat = mat;
            return f;
        }
    }
}
