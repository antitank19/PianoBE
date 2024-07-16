using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface
{
    public interface IServiceWrapper
    {
        public ISongService Songs { get; }
        public ISheetService Sheets { get; }
        public IInstrumentService Instruments { get; }
        public ISystemService System { get; }
    }
}
