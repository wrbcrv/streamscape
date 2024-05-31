using Api.Repositories;
using AutoMapper;
using Api.DTOs;
using Api.Models;

namespace Api.Services
{
    public class TitleService : ITitleService
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public TitleService(ITitleRepository titleRepository, IEpisodeRepository episodeRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _titleRepository = titleRepository;
            _episodeRepository = episodeRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TitleResponseDTO>> GetAllAsync()
        {
            var titles = await _titleRepository.GetAllAsync();
            return titles.Select(TitleResponseDTO.ValueOf);
        }

        public async Task<TitleResponseDTO> GetByIdAsync(int id)
        {
            var title = await _titleRepository.GetByIdAsync(id);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            return TitleResponseDTO.ValueOf(title);
        }

        public async Task<TitleResponseDTO> AddAsync(TitleDTO titleDTO)
        {
            var title = _mapper.Map<Title>(titleDTO);

            foreach (var genreId in titleDTO.GenreIds)
            {
                var genre = await _genreRepository.GetByIdAsync(genreId);

                if (genre == null)
                {
                    throw new KeyNotFoundException($"Genre with ID {genreId} not found.");
                }

                title.TitleGenres.Add(new TitleGenre { Title = title, Genre = genre });
            }

            title = await _titleRepository.AddAsync(title);
            return TitleResponseDTO.ValueOf(title);
        }

        public async Task<TitleResponseDTO> UpdateAsync(int id, TitleDTO titleDTO)
        {
            var title = await _titleRepository.GetByIdAsync(id);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            title.Name = titleDTO.Name;
            title.Description = titleDTO.Description;
            title.Release = titleDTO.Release;

            title = await _titleRepository.UpdateAsync(title);
            return TitleResponseDTO.ValueOf(title);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var title = await _titleRepository.GetByIdAsync(id);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            return await _titleRepository.DeleteAsync(id);
        }

        public async Task<EpisodeResponseDTO> AddEpisodeAsync(int titleId, EpisodeDTO episodeDTO)
        {
            var title = await _titleRepository.GetByIdAsync(titleId);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            var episode = _mapper.Map<Episode>(episodeDTO);
            episode.TitleId = titleId;

            episode = await _episodeRepository.AddAsync(episode);
            return EpisodeResponseDTO.ValueOf(episode);
        }
    }
}