using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpManchester
{
    public interface IOutput
    {
        public Team GetResults(string selectedName);
    }
}
