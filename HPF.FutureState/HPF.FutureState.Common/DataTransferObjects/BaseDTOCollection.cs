using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BaseDTOCollection<T> : Collection<T>
    {
        public void Add(Collection<T> collection)
        {
            if (collection == null) return;
            foreach (var col in collection)
            {
                Add(col);
            }
        }
    }
}
