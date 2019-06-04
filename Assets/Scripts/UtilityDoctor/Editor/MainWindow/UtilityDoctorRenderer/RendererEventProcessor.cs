using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public partial class UtilityDoctorRenderer
    {
        public void ProcessEvents(Event e)
        {
            drag = Vector2.zero;

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        ProcessLeftClick(e.mousePosition);
                    }

                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                        e.Use();
                    }
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0)
                    {
                        ProcessMouseDrag(e);
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    draggedNode = null;
                    break;
            }
        }
    }
}
