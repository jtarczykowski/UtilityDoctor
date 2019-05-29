using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class Connection
    {
        public InputConnectionPin input;
        public OutputConnectionPin output;

        public Connection() { }

        public Connection(InputConnectionPin input, OutputConnectionPin output)
        {
            this.input = input;
            this.output = output;
        }
    }
}
