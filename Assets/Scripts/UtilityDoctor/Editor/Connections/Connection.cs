using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class Connection
    {
        [XmlIgnore]
        public InputConnectionPin input;
        [XmlIgnore]
        public OutputConnectionPin output;

        public string inputId;
        public string outputId;

        public Connection() { }

        public Connection(InputConnectionPin input, OutputConnectionPin output)
        {
            this.input = input;
            this.output = output;
            inputId = input.id;
            outputId = output.id;
        }
    }
}
