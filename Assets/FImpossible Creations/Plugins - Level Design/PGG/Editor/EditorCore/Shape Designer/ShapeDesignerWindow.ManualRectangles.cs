using UnityEngine;
using UnityEditor;
using FIMSpace.FEditor;
using System.Collections.Generic;

namespace FIMSpace.Generating
{
    public partial class ShapeDesignerWindow : EditorWindow
    {


        private Vector2 scroller = Vector2.zero;
        static int selectedManualShape = 0;
        protected static int selectedCell = 0;
        static List<GenerationShape.ShapeCells> shapeLists = new List<GenerationShape.ShapeCells>();
        static FieldSpawner spawnerOwner;
        static GUIStyle boxStyle = null;
        static GUIStyle boxStyleSel = null;
        static int depthLevel = 0;
        protected enum ESpaceView { XZ_TopDown, XY_Side, ZY_Front }
        protected static ESpaceView spaceView = ESpaceView.XZ_TopDown;

        static int drawSize = 30;


        void DrawManualRectanglesGUI()
        {
            GUILayout.Space(-7);
            EditorGUILayout.BeginVertical(GUILayout.Height(280));

            Color bc = GUI.backgroundColor;

            if (boxStyle == null)
            {
                boxStyle = new GUIStyle(FGUI_Resources.BGInBoxStyle);
                boxStyle.alignment = TextAnchor.MiddleCenter;
                boxStyle.fontStyle = FontStyle.Bold;
                boxStyle.fontSize = Mathf.RoundToInt(boxStyle.fontSize * 1.2f);

                boxStyleSel = new GUIStyle(boxStyle);
                boxStyleSel.normal.background = FGUI_Resources.HeaderBoxStyleH.normal.background;
            }

            drawSize = EditorGUILayout.IntSlider("Cells Draw Size", drawSize, 12, 40);
            GUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            //EditorGUIUtility.labelWidth = 50;
            //EditorGUIUtility.fieldWidth = 18;
            //depthLevel = EditorGUILayout.IntField(new GUIContent("YLevel", "If you want to check cells placement above or below main cell"), depthLevel);
            //EditorGUIUtility.fieldWidth = 0;
            //EditorGUIUtility.labelWidth = 0;
            //if (GUILayout.Button("▲", FGUI_Resources.ButtonStyle, GUILayout.Width(22), GUILayout.Height(18))) depthLevel++;
            //if (GUILayout.Button("▼", FGUI_Resources.ButtonStyle, GUILayout.Width(22), GUILayout.Height(18))) depthLevel--;

            //GUILayout.Space(6);

            //EditorGUIUtility.labelWidth = 50;
            //EditorGUIUtility.fieldWidth = 24;

            if (selectedPreset.Setup != null)
            {
                EditorGUI.BeginChangeCheck();

                int manualShapes = 1;
                shapeLists = selectedPreset.Setup.CellSets;
                if (shapeLists.Count == 0) shapeLists.Add(new GenerationShape.ShapeCells());
                manualShapes = shapeLists.Count;

                EditorGUIUtility.labelWidth = 68;
                EditorGUILayout.LabelField(new GUIContent("Shape " + (selectedManualShape + 1) + "/" + manualShapes, "You can define multiple shapes to be randomly choosed with shape preset"));
                //selectedManualShape = EditorGUILayout.IntField(new GUIContent("Shape " + (selectedManualShape + 1) + "/" + manualShapes, "You can define multiple shapes to be randomly choosed with shape preset"), selectedManualShape);
                EditorGUIUtility.fieldWidth = 0;
                EditorGUIUtility.labelWidth = 0;
                if (GUILayout.Button("◄", FGUI_Resources.ButtonStyle, GUILayout.Width(22), GUILayout.Height(18))) selectedManualShape -= 1;
                if (GUILayout.Button("►", FGUI_Resources.ButtonStyle, GUILayout.Width(22), GUILayout.Height(18))) selectedManualShape += 1;
                if (selectedManualShape >= manualShapes) selectedManualShape = 0;
                if (selectedManualShape < 0) selectedManualShape = manualShapes - 1;
                if (GUILayout.Button("+", FGUI_Resources.ButtonStyle, GUILayout.Width(22), GUILayout.Height(18))) { shapeLists.Add(new GenerationShape.ShapeCells()); selectedManualShape = shapeLists.Count - 1; }
                if (manualShapes > 1) if (GUILayout.Button(new GUIContent(FGUI_Resources.Tex_Remove), FGUI_Resources.ButtonStyle, GUILayout.Width(22), GUILayout.Height(18))) { shapeLists.RemoveAt(selectedManualShape); selectedManualShape -= 1; if (selectedManualShape < 0) selectedManualShape = 0; }
                GenerationShape.ShapeCells drawing = shapeLists[selectedManualShape];

                GUILayout.Space(6);

                EditorGUILayout.EndHorizontal();

                FGUI_Inspector.DrawUILine(0.25f, 0.4f, 2, 6);

                scroller = EditorGUILayout.BeginScrollView(scroller);

                float centerX = position.size.x / 2f;
                float centerY = position.size.y / 2f - (drawSize + 12);

                Rect refRect = new Rect(0, 0, drawSize, drawSize);
                float offset = refRect.width / 4f;

                int gridDrawW = Mathf.RoundToInt(position.width / drawSize);
                int gridDrawH = Mathf.RoundToInt(300 / drawSize);

                int centerCellOnGridX = gridDrawW / 2;
                if (gridDrawW % 2 == 0) centerCellOnGridX -= 1;

                int centerCellOnGridY = gridDrawH / 2 - 1;
                if (gridDrawH % 2 == 0) centerCellOnGridY -= 1;

                for (int x = 0; x < gridDrawW - 1; x++)
                {
                    for (int y = 0; y < gridDrawH - 3; y++)
                    {
                        Rect rDraw = new Rect(refRect); rDraw.x = x * drawSize + offset; rDraw.y = y * drawSize + offset;

                        int posX = x - centerCellOnGridX;
                        int posZ = centerCellOnGridY - y;
                        Vector3Int pos = new Vector3Int(posX, depthLevel, posZ);

                        bool contained = false;
                        if (drawing.ContainsPosition(pos)) contained = true;
                        GUIStyle styl = contained ? boxStyleSel : boxStyle;

                        if (centerCellOnGridX == x && centerCellOnGridY == y && depthLevel == 0)
                        {
                            Color preC = GUI.color;
                            Color prebC = GUI.backgroundColor;

                            if (contained)
                                GUI.backgroundColor = new Color(0.7f, 0.7f, 1f, 1f);
                            else
                                GUI.backgroundColor = new Color(0.85f, 0.85f, 0.85f, 1f);

                            if (GUI.Button(rDraw, new GUIContent("C", "Center cell - means cell from which checking starts!\nPosition Offset = " + pos), boxStyleSel))
                            {
                                drawing.SwitchOnPosition(pos);
                            }

                            GUI.color = preC;
                            GUI.backgroundColor = prebC;
                        }
                        else
                        {
                            if (contained)
                                GUI.backgroundColor = new Color(0.7f, 0.7f, 1f, 1f);
                            else
                                GUI.backgroundColor = new Color(1f, 1f, 1f, 0.4f);

                            if (GUI.Button(rDraw, new GUIContent(" ", "Position Offset = " + pos), styl))
                            {
                                drawing.SwitchOnPosition(pos);
                            }
                        }
                    }
                }

                if (EditorGUI.EndChangeCheck()) { repaint = true; }

                EditorGUILayout.EndScrollView();
            }

            EditorGUILayout.EndVertical();
        }



    }
}