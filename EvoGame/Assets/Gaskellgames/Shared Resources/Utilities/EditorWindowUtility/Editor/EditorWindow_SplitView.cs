using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    public class EditorWindow_SplitView : EditorWindow
    {
        #region Variables

        private Color32 seperatorColor = new Color32(035, 035, 035, 255);
        private Color32 shadowColor = new Color32(039, 039, 039, 255);
        private Color32 pageColor = new Color32(042, 042, 042, 255);
        private Color32 menuColor = new Color32(048, 048, 048, 255);
        private Color32 buttonColor = new Color32(150, 150, 150, 255);
        private Color32 headerColor = new Color32(223, 223, 223, 255);
        private GUIStyle headerStyle = new GUIStyle();
        private Rect cursorChangeRect;
        private float windowMinWidthPercent = 0.20f;
        private float menuHeight = 21.0f;
        private float shadowBuffer = 2.0f;
        private bool resize;
        
        protected MenuTree menuTree = new MenuTree();
        protected float resizeBarPosition;
        protected Vector2 menuScrollPos { get; private set; }
        protected Vector2 scrollPos { get; private set; }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Menu Item [Implement in base]
        
        /*
        [MenuItem("Gaskellgames/Examples/Split View: Base", false, 1)]
        public static void ShowWindow()
        {
            EditorWindow_SplitView window = GetWindow<EditorWindow_SplitView>();
            window.titleContent = new GUIContent("Split View: Base");
        }
        */
        
        #endregion

        //----------------------------------------------------------------------------------------------------

        #region MenuTree

        protected virtual void MenuTree()
        {
            menuTree.header = "Split View: Base";
            menuTree.pages = new List<string>()
            {
                "Page 01",
                "Page 02"
            };
        }

        protected virtual void SelectedPage()
        {
            EditorGUILayout.Space();
            switch (menuTree.selectionIndex)
            {
                case 0:
                    PlaceHolderPageText("'Page 01'");
                    break;
                case 1:
                    PlaceHolderPageText("'Page 02'");
                    break;
            }
        }
        
        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Editor Loop

        protected void OnEnable()
        {
            InitialiseSplitView();
        }

        private void OnFocus()
        {
            ValidateResizeBar();
            cursorChangeRect = new Rect(resizeBarPosition, menuHeight, shadowBuffer * 1.5f, Screen.height * 1.2f);
        }

        private void OnGUI()
        {
            // calculate menu tree and pages
            MenuTree();
            
            // add buffer to top of screen
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            // draw menu, resize bar and the selected page ...
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal(GUILayout.Width(resizeBarPosition));
            GUILayout.BeginVertical();
            
            // ... draw menu background & menu bar
            EditorGUI.DrawRect(new Rect(0, menuHeight, resizeBarPosition, Screen.height - menuHeight), menuColor);
            EditorGUI.DrawRect(new Rect(0, menuHeight - 1, Screen.width, 1), seperatorColor);
            float scrollOffset = Screen.height - menuHeight;
            menuScrollPos = EditorGUILayout.BeginScrollView(menuScrollPos, GUILayout.ExpandWidth(true), GUILayout.Height(scrollOffset));
            MenuBar();
            EditorGUILayout.EndScrollView();
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            
            // ... draw page background & resize bar
            EditorGUI.DrawRect(new Rect(resizeBarPosition, menuHeight, Screen.width - resizeBarPosition, Screen.height - menuHeight), pageColor);
            EditorGUI.DrawRect(new Rect(resizeBarPosition, menuHeight, Screen.width - resizeBarPosition, shadowBuffer), shadowColor);
            ResizeBar();
            
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            
            // ... draw page
            scrollOffset = Screen.height - menuHeight;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true), GUILayout.Height(scrollOffset));
            SelectedPage();
            EditorGUILayout.EndScrollView();
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Header, Menu & Resize Bar

        private void InitialiseSplitView()
        {
            ValidateResizeBar();
            cursorChangeRect = new Rect(resizeBarPosition, menuHeight, shadowBuffer * 1.5f, Screen.height * 1.2f);

            // set header style
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.alignment = TextAnchor.MiddleLeft;
            headerStyle.normal.textColor = headerColor;
        }

        private void ValidateResizeBar()
        {
            float minWidth = this.position.width * windowMinWidthPercent;
            if (resizeBarPosition <= minWidth)
            {
                resizeBarPosition = minWidth;
                cursorChangeRect.Set(resizeBarPosition, menuHeight, cursorChangeRect.width, cursorChangeRect.height);
            }
            else if (Screen.width - minWidth <= resizeBarPosition)
            {
                resizeBarPosition = Screen.width - minWidth;
                cursorChangeRect.Set(resizeBarPosition, menuHeight, cursorChangeRect.width, cursorChangeRect.height);
            }
        }
        
        private void MenuBar()
        {
            // calculate variables
            Color32 defaultColor = GUI.backgroundColor;
            float LeftWindowWidth = resizeBarPosition - 5;
            
            if (menuTree.header != null && menuTree.header != "")
            {
                // header
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField(menuTree.header, headerStyle, GUILayout.Width(LeftWindowWidth));
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();

                float buffer = 15.0f;
                float lineThickness = 1.0f;
                float windowPosition = 27.5f;
                Rect rect = new Rect(buffer, windowPosition, resizeBarPosition - (2f * buffer), lineThickness);
                EditorGUI.DrawRect(rect, menuTree.underlineColor);
            }
            else
            {
                // no header
                EditorGUILayout.Space();
            }

            if (0 < menuTree.pages?.Count)
            {
                // buttons
                GUI.backgroundColor = buttonColor;
                for (int i = 0; i < menuTree.pages.Count; i++)
                {
                    if (GUILayout.Button(menuTree.pages[i], GUILayout.Height(30), GUILayout.Width(LeftWindowWidth)))
                    {
                        menuTree.selectionIndex = i;
                    }
                }

                // reset background color
                GUI.backgroundColor = defaultColor;
            }
        }
        
        private void ResizeBar()
        {
            EditorGUI.DrawRect(cursorChangeRect, shadowColor);
            EditorGUIUtility.AddCursorRect(cursorChangeRect, MouseCursor.ResizeHorizontal);

            // enable resize
            if (Event.current.type == EventType.MouseDown && cursorChangeRect.Contains(Event.current.mousePosition))
            {
                resize = true;
            }

            if (resize)
            {
                // get resize bar position
                float minWidth = this.position.width * windowMinWidthPercent;
                float mouseX = Mathf.Max(0, Event.current.mousePosition.x);
                if (mouseX < 0 + minWidth)
                {
                    resizeBarPosition = minWidth;
                }
                else if (Screen.width - minWidth < mouseX)
                {
                    resizeBarPosition = Screen.width - minWidth;
                }
                else
                {
                    resizeBarPosition = mouseX;
                }

                // set resize bar position
                cursorChangeRect.Set(resizeBarPosition, menuHeight, cursorChangeRect.width, cursorChangeRect.height);
                
                // Update visuals
                Repaint();
            }

            // disable resize
            if (Event.current.type == EventType.MouseUp)
            {
                resize = false;
            }
        }

        protected void PlaceHolderPageText(string pageName)
        {
            // set style
            GUIStyle myStyle = new GUIStyle();
            myStyle.alignment = TextAnchor.MiddleCenter;
            myStyle.normal.textColor = Color.grey;
            
            // draw placeholder window
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(pageName + " is empty", myStyle);
        }

        #endregion

    } // class end
}