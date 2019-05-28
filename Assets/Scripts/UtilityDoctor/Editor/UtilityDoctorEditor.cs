using System;
using System.Collections.Generic;
using System.Linq;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class UtilityDoctorEditor : NodeBasedEditor
    {
        private List<SelectorNode> selectorNodes;
        private List<ActionNode> actionNodes;

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


            var qualifierTypes = typeof(ActionBase).Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ActionBase)))
                .ToArray();

            foreach (var t in qualifierTypes)
            {
                genericMenu.AddItem(new GUIContent($"Add Action/{t.Name}"), false,
                    () => OnClickCreateActionNode(mousePosition,t));
            }

            genericMenu.ShowAsContext();
        }

        private SelectorNode CreateSelectorNode(Vector2 mousePosition,Selector selector)
        {
            var selectorNode =  new SelectorNode(selector,
                mousePosition,
                defaultNodeDimensions,
                defaultNodeStyle,
                OnClickInPoint,
                OnClickOutPoint,
                OnClickRemoveNode);
            return selectorNode;
        }

        private ActionNode CreateActionNode(Vector2 mousePosition, ActionBase action)
        {
            var actionNode = new ActionNode(mousePosition,
                defaultNodeDimensions,
                defaultNodeStyle,
                OnClickInPoint,
                OnClickOutPoint,
                OnClickRemoveNode);

            actionNode.action = action;
            return actionNode;
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

        private void OnClickCreateActionNode(Vector2 mousePosition, Type t)
        {
            if (nodes == null)
            {
                nodes = new List<Node>();
            }

            if (actionNodes == null)
            {
                actionNodes = new List<ActionNode>();
            }

            var action = Activator.CreateInstance(t) as ActionBase;
            var actionNode = CreateActionNode(mousePosition, action);
            actionNodes.Add(actionNode);
            nodes.Add(actionNode);
        }
    }
}
