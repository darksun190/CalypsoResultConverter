using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalypsoResultConverter
{
    public class CalypsoResultEventArgs
    {
        public OperationResult op_result;

        public CalypsoResultEventArgs(OperationResult oR)
        {
            op_result = oR;
        }
    }
}
