using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Dto
{
    public class ValidatedDataEntry
    {
        public DataCenter Source { get; set; } = DataCenter.Empty;
        public DataCenter Destination { get; set; } = DataCenter.Empty;
        public Access ValidatedAccess { get; set; } = Access.Empty;
    }
}
