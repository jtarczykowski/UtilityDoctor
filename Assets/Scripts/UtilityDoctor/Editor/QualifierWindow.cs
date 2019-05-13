﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class QualifierWindow : EditorWindow
    {
        public Qualifier qualifier;

        private List<bool> toggles = new List<bool>();

        private void OnGUI()
        {
            qualifier.name = GUILayout.TextField(qualifier.name);
            var scorers = qualifier.scorers;


            if (toggles.Count != scorers.Count)
            {
                toggles.Clear();
                foreach (var scorer in scorers)
                {
                    toggles.Add(false);
                }
            }

            for(int i = 0; i < scorers.Count; ++i)
            {
                var scorer = scorers[i];

                GUILayout.BeginHorizontal();
                GUILayout.Box(scorer.GetType().Name);
                toggles[i] = GUILayout.Toggle(toggles[i],"select");
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add new scorer"))
            {
                AddNewScorer();
            }
            if (GUILayout.Button("Remove selected"))
            {
                for (int i = toggles.Count - 1; i >= 0; --i)
                {
                    if(toggles[i])
                    {
                        qualifier.scorers.RemoveAt(i);
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        private void AddNewScorer()
        {
            var window = GetWindow<AddScorerWindow>();
            window.Init(qualifier);
        }
    }
}