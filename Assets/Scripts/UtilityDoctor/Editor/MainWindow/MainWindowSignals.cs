using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class SaveButtonPressed : ASignal { }
    public class LoadButtonPressed : ASignal { }

    public class AddSelector : ASignal<Vector2, Selector> { }
    public class AddQualifier : ASignal<SelectorNode, System.Type> { }
}
