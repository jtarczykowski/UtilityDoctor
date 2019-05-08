using System;
using System.Collections.Generic;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class UtilityDoctorEditor : NodeBasedEditor
    {
        private List<SelectorNode> selectorNodes;

        [MenuItem("Window/UtilityDoctorEditor")]
        protected static void OpenUtilityDoctor()
        {
            var window = GetWindow<UtilityDoctorEditor>();
            window.titleContent = new GUIContent("Node Based Editor");
        }

        protected override void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add Selector/FirstScoreWins"), false,
                () => OnClickAddFirstScoreWinsSelector(mousePosition));
            genericMenu.AddItem(new GUIContent("Add Selector/HighestScoreWins"), false,
                () => OnClickAddHighestScoreWinsSelector(mousePosition));
            genericMenu.ShowAsContext();
        }

        private SelectorNode CreateSelectorNode(Vector2 mousePosition,Selector selector)
        {
            var selectorNode =  new SelectorNode(mousePosition,
                defaultNodeDimensions,
                defaultNodeStyle,
                OnClickInPoint,
                OnClickOutPoint,
                OnClickRemoveNode);

            selectorNode.selector = selector;
            return selectorNode;
        }

        private void OnClickAddSelector(Vector2 mousePosition, Selector selector)
        {
            if (nodes == null)
            {
                nodes = new List<Node>();
            }

            if(selectorNodes == null)
            {
                selectorNodes = new List<SelectorNode>();
            }

            var newNode = CreateSelectorNode(mousePosition, selector);
            nodes.Add(newNode);
            selectorNodes.Add(newNode);
        }

        private void OnClickAddHighestScoreWinsSelector(Vector2 mousePosition)
        {
            OnClickAddSelector(mousePosition, new HighestScoreWinsSelector());
        }

        private void OnClickAddFirstScoreWinsSelector(Vector2 mousePosition)
        {
            OnClickAddSelector(mousePosition, new FirstScoreWinsSelector());
        }
    }
}
