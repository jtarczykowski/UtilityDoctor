using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class MenuBarDrawer 
    {
        public const float menuBarHeight = 20f;

        public static void DrawMenuBar(EditorWindow window)
        {
            var menuBar = new Rect(0, 0, window.position.width, menuBarHeight);

            GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Save"), EditorStyles.toolbarButton, GUILayout.Width(35)))
            {
                Signals.Get<SaveButtonPressed>().Dispatch();
            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Load"), EditorStyles.toolbarButton, GUILayout.Width(35)))
            {
                Signals.Get<LoadButtonPressed>().Dispatch();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}
