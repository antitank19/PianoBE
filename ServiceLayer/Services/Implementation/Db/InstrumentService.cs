using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.IRepository;
using ServiceLayer.CustomException;
using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Enums;
using ServiceLayer.ModelViews.Instruments;

namespace ServiceLayer.Services.Implementation.Db
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InstrumentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<T> GetList<T>()
        {
            return _unitOfWork.InstrumentRepository.GetList<T>();
            /*
            return context.Instruments.ProjectTo<T>(mapper.ConfigurationProvider);
        */
        }
        public async Task<bool> IsExistAsync(int id)
        {
            return await _unitOfWork.InstrumentRepository.IsExistAsync(id);
            /*return await context.Instruments.AnyAsync(x => x.Id == id);*/
        }
        
        public async Task<ResponseInstrumentsModel> GetInstrumentById(int id)
        {
            /*
            var instrument = await _context.Instruments.Where(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);
            */
            var instrument = await _unitOfWork.InstrumentRepository.FindAsync(p => p.Id == id && p.IsActive == true && p.IsDeleted == false);
            if (instrument == null)
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                    "Instruments not found");
            var response = _mapper.Map<ResponseInstrumentsModel>(instrument);
            return response;
        }

        public async Task<IEnumerable<ResponseInstrumentsModel>> GetAllInstrumente()
        {
            /*
            var instrument = await _context.Instruments.Where(x => x.IsActive == true && x.IsDeleted == false).ToListAsync();
            */
            var instrument =
                await _unitOfWork.InstrumentRepository.FindAllAsync(p => p.IsActive == true && p.IsDeleted == false);
            if (instrument.IsNullOrEmpty())
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                    "Instruments is empty or not found");
            var response = _mapper.Map<IEnumerable<ResponseInstrumentsModel>>(instrument);
            return response;
        }

        public async Task<CreateInstrumentsModel> CreateInstrument(CreateInstrumentsModel request)
        {
            var instrument = _mapper.Map<Instrument>(request);
            /*await _context.Instruments.AddAsync(instrument);
            await _context.SaveChangesAsync();*/
            await _unitOfWork.InstrumentRepository.InsertAsync(instrument);
            await _unitOfWork.SaveAsync();
            var response = _mapper.Map<CreateInstrumentsModel>(instrument);
            return response;
        }

        public async Task<UpdateInstrumentsModel> UpdateInstrument(int id, UpdateInstrumentsModel request)
        {
            /*
            var instrument = await _context.Instruments.Where(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);
            */
            var instrument = await _unitOfWork.InstrumentRepository.FindAsync(p => p.IsActive == true && p.IsDeleted == false && p.Id == id);
            if (instrument == null) 
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                    "Instruments not found");
            _mapper.Map(request, instrument);
            /*_context.Instruments.Update(instrument);
            await _context.SaveChangesAsync();*/
            await _unitOfWork.InstrumentRepository.UpdateAsync(instrument);
            await _unitOfWork.SaveAsync();
            var response = _mapper.Map<UpdateInstrumentsModel>(instrument);
            return response;
        }

        public async Task DeleteInstrument(int id)
        {
            /*
            var instrument = _context.Instruments.Where(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            */
            var instrument = await _unitOfWork.InstrumentRepository.FindAsync(p => p.IsActive == true && p.IsDeleted == false && p.Id == id);
            if (instrument == null) 
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                    "Instruments not found");
            instrument.IsDeleted = true;
            /*_context.Instruments.Update(instrument);
            _context.SaveChanges();*/
            await _unitOfWork.InstrumentRepository.UpdateAsync(instrument);
            await _unitOfWork.SaveAsync();
        }
    }
}
