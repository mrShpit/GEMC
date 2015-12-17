namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class interpreterContext
    {
        // Constructor
        public interpreterContext(string input)
        {
            this._input = input;
        }

        // Gets or sets input
        public string Input
        {
            get { return this._input; }
            set { this._input = value; }
        }

        // Gets or sets output
        public List<byte> Output
        {
            get { return this._output; }
            set { this._output = value; }
        }

        private string _input;
        private List<byte> _output;
    }
}
