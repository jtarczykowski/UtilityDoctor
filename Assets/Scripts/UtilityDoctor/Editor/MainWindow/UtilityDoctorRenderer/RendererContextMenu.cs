using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public partial class UtilityDoctorRenderer
    {
        public void ProcessContextMenu(Vector2 mousePosition)
        {
            var nodeAtPosition = FindNodeAtPosition(mousePosition);
            if(nodeAtPosition != null)
            {
                if(nodeAtPosition is SelectorNode selectorNode)
                {
                    selectorNodeDrawer.ProcessContextMenu(selectorNode);
                    return;
                }
                else if (nodeAtPosition is ActionNode actionNode)
                {
                    actionNodeDrawer.ProcessContextMenu(actionNode);
                }
                return;
            }

            ProcessMainWindowMenu(mousePosition);
        }

        protected void ProcessMainWindowMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add Selector/FirstScoreWins"), false,
                () => Signals.Get<AddSelector>()
                .Dispatch(mousePosition, new FirstScoreWinsSelector())
                );
            genericMenu.AddItem(new GUIContent("Add Selector/HighestScoreWins"), false,
                () => Signals.Get<AddSelector>()
                .Dispatch(mousePosition, new HighestScoreWinsSelector())
                );


            var actionTypes = typeof(ActionBase).Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ActionBase)))
                .ToArray();

            foreach (var t in actionTypes)
            {
                genericMenu.AddItem(new GUIContent($"Add Action/{t.Name}"), false,
                    () => OnClickCreateActionNode(mousePosition, t));
            }

            genericMenu.ShowAsContext();
        }
    }
}
