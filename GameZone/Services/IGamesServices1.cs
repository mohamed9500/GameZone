using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGamesServices1
    {
        Task Create(CreateGameFormViewModel game);
    }
}