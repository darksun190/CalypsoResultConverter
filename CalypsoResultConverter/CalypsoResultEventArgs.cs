using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalypsoResultConverter
{
    /// <summary>
    /// deliver OperationResult in an event
    /// </summary>
    public class CalypsoResultEventArgs
    {
        public OperationResult op_result;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oR">one operation result</param>
        public CalypsoResultEventArgs(OperationResult oR)
        {
            op_result = oR;
        }
    }
}
