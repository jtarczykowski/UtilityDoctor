using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class XMLWrapperSelector
    {
        private Selector selector;

        public XMLWrapperSelector() { }

        public XMLWrapperSelector(Selector selector)
        {
            this.selector = selector;
        }

        public static implicit operator Selector(XMLWrapperSelector wrapper)
        {
            return wrapper != null ? wrapper.selector : null;
        }

        public static implicit operator XMLWrapperSelector(Selector selector)
        {
            return selector != null ? new XMLWrapperSelector(selector) : null;
        }

        public string id
        {
            get { return selector.id; }
            set { selector.id = value; }
        }
    }
}
