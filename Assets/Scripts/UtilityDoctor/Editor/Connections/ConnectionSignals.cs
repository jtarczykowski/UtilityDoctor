using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class ConnectionPinClicked : ASignal<ConnectionPin> { }
    public class RemoveConnectionClicked : ASignal<Connection> { }
}
