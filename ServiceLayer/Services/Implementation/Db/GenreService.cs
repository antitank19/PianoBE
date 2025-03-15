using AutoMapper;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.IRepository;
using ServiceLayer.CustomException;
using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Enums;
using ServiceLayer.ModelViews.Genre;
using ServiceLayer.Services.Interface.Db;

namespace ServiceLayer.Services.Implementation.Db;

public class GenreService : IGenreService
{
    private readonly PianoContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GenreService(PianoContext context,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _context = context;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetGenreResponse> GetGenreById(int id)
    {
        /*
        var genre = await _context.Genres.FirstOrDefaultAsync(p => p.Id == id);
        */
        var genre = await _unitOfWork.GenreRepository.FindAsync(p => p.Id == id);
        if(genre == null) 
            throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,"Genre not found");
        
        var response = _mapper.Map<GetGenreResponse>(genre);
        return response;
    }
    
    public async Task<IEnumerable<GetGenreResponse>> GetAllGenre()
    {
        /*
        var genres = await _context.Genres.ToListAsync();
        */
        var genres = await _unitOfWork.GenreRepository.FindAllAsync(p => p.IsActive == true && p.IsDeleted == false);
        if(genres.IsNullOrEmpty()) 
            throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,"Genre is empty or not found");
        var response = _mapper.Map<IEnumerable<GetGenreResponse>>(genres);
        return response;
    }
    
    public async Task<CreateGenreResponse> CreateGenre(CreateGenreRequest request)
    {
        var genre = _mapper.Map<Genre>(request);
        /*await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();*/
        await _unitOfWork.GenreRepository.InsertAsync(genre);
        await _unitOfWork.SaveAsync();
        var response = _mapper.Map<CreateGenreResponse>(genre);
        return response;
    }

    public async Task<UpdateGenreResponse> UpdateGenre(int id, UpdateGenreRequest request)
    {
        /*
        var genre = await _context.Genres.FirstOrDefaultAsync(p => p.Id == id);
        */
        var genre = await _unitOfWork.GenreRepository.FindAsync(p => p.Id == id && p.IsActive == true && p.IsDeleted == false);
        if(genre == null)           
            throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,"Genre not found");

        _mapper.Map(request, genre);
        /*_context.Genres.Update(genre);
        await _context.SaveChangesAsync();*/
        await _unitOfWork.GenreRepository.UpdateAsync(genre);
        await _unitOfWork.SaveAsync();
        var response = _mapper.Map<UpdateGenreResponse>(request);
        return response;
    }
    public async Task DeleteGenre(int id)
    {
        var genre = await _unitOfWork.GenreRepository.FindAsync(p => p.Id == id && p.IsActive == true && p.IsDeleted == false);
        if (genre == null)
            throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                "Genre not found");
        genre.IsDeleted = true;
        _unitOfWork.GenreRepository.UpdateAsync(genre);
        await _unitOfWork.SaveAsync();
    }
}