using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CounseledProgramDTOCollection: BaseDTOCollection<CounseledProgramDTO>
    {
        public CounseledProgramDTO GetCounseledProgram(int counselingProgramId)
        {
            return this.SingleOrDefault(i => i.CounseledProgramId == counselingProgramId);
        }
    }
}
