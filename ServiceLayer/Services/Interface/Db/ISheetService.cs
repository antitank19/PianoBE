using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface.Db
{
    public interface ISheetService
    {
        public IQueryable<T> GetSheetList<T>();
        public IQueryable<T> GetSheetListBySongId<T>(int songId);
        public Task<T> GetSheetByIdAsync<T>(int sheetId);
        public Task<SheetGetDto> CreateSheetAsync(SheetCreateDto input);
        public Task<SheetGetDto> CreateSheetAsync(SheetSymbolCreateDto input);
        public Task<SheetGetDto> CreateSheetAsync(SheetMidiCreateDto input);
        public Task<SheetGetDto> CreateSheetAsync(SheetXmlCreateDto input);
        public Task<SheetGetDto> UpdateSheetAsync(SheetUpdateDto input);
        public Task<bool> IsExistAsync(int sheetId);
        Task DeleteSheet(int id);
    }
}
