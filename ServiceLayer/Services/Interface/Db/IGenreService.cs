using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Genre;

namespace ServiceLayer.Services.Interface.Db;

public interface IGenreService
{
    Task<GetGenreResponse>GetGenreById(int id);
    Task<IEnumerable<GetGenreResponse>> GetAllGenre();
    Task<CreateGenreResponse> CreateGenre(CreateGenreRequest request);
    Task<UpdateGenreResponse> UpdateGenre(int id, UpdateGenreRequest request);
    Task DeleteGenre(int id);
}