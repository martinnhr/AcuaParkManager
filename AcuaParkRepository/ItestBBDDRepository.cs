using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuaParkRepository
{
    public interface ItestBBDDRepository
    {

        Task<string> GetName();

    }
}
