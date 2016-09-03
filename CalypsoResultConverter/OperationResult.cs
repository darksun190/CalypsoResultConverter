using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalypsoResultConverter
{
    public class OperationResult
    {
        public HeaderTable HeaderData
        {
            get;
            set;
        }
        public FeatureTable FeatureData
        {
            get;
            set;
        }
        public CharacteristicTable CharData
        {
            get;
            set;
        }

    }
}
