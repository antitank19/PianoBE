using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Instruments;

namespace ServiceLayer.Services.Interface.Db
{
    public interface IInstrumentService
    {
        public IQueryable<T> GetList<T>();
        public Task<bool> IsExistAsync(int id);
        Task<ResponseInstrumentsModel> GetInstrumentById(int id);
        Task<IEnumerable<ResponseInstrumentsModel>> GetAllInstrumente();
        Task<CreateInstrumentsModel> CreateInstrument(CreateInstrumentsModel request);
        Task<UpdateInstrumentsModel> UpdateInstrument(int id, UpdateInstrumentsModel request);
        Task DeleteInstrument(int id);
        
    }
}
