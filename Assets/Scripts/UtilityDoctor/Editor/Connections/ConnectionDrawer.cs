using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class ConnectionDrawer
    {
        private static Vector2 bezierOffset = Vector2.left * 50f;
        private const float defaultHandleWidth = 2f;

        public void DrawConnections(List<Connection> connections)
        {
            foreach(var connection in connections)
            {
                var inCenter = connection.input.rect.center;
                var outCenter = connection.output.rect.center;

                Handles.DrawBezier(inCenter, outCenter,
                    inCenter + bezierOffset, outCenter - bezierOffset,
                    Color.white, null, defaultHandleWidth);

                bool removeClicked = Handles.Button((inCenter + outCenter) * 0.5f,
                    Quaternion.identity, 4, 8, Handles.RectangleHandleCap);

                if (removeClicked)
                {
                    Signals.Get<RemoveConnectionClicked>().Dispatch(connection);
                }
            }
        }
    }
}
