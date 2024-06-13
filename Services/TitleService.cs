using Api.Repositories;
using AutoMapper;
using Api.DTOs;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services
{
    public class TitleService : ITitleService
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public TitleService(ITitleRepository titleRepository, IEpisodeRepository episodeRepository, IGenreRepository genreRepository, IFileService fileService, IMapper mapper)
        {
            _titleRepository = titleRepository;
            _episodeRepository = episodeRepository;
            _genreRepository = genreRepository;
            _fileService = fileService;
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

            if (titleDTO.File != null)
            {
                var thumbnailSource = await _fileService.UploadAsync(titleDTO.File);
                title.Thumbnail = thumbnailSource;
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

        public async Task<EpisodeResponseDTO> AddEpisodeAsync(int titleId, UploadDTO episodeDTO)
        {
            var title = await _titleRepository.GetByIdAsync(titleId);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            var last = await _episodeRepository.GetLastEpisodeByTitleIdAsync(titleId);
            int next = 1;

            if (last != null)
            {
                next = last.Number + 1;
            }

            var episode = new Episode
            {
                Number = next,
                Name = episodeDTO.Name,
                Description = episodeDTO.Description,
                TitleId = titleId,
            };

            if (episodeDTO.File != null)
            {
                var source = await _fileService.UploadAsync(episodeDTO.File);
                episode.Source = source;
            }

            episode = await _episodeRepository.AddAsync(episode);
            return EpisodeResponseDTO.ValueOf(episode);
        }

        public async Task<FileStreamResult> DownloadEpisodeAsync(int titleId, int episodeId)
        {
            var title = await _titleRepository.GetByIdAsync(titleId);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            var episode = await _episodeRepository.GetByIdAsync(episodeId);

            if (episode == null || episode.TitleId != titleId)
            {
                throw new KeyNotFoundException("Episode not found or does not belong to the specified title.");
            }

            var fileStreamResult = await _fileService.DownloadAsync(episode.Source);

            return fileStreamResult;
        }

        public async Task<FileStreamResult> DownloadThumbnailAsync(int titleId)
        {
            var title = await _titleRepository.GetByIdAsync(titleId);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            if (string.IsNullOrEmpty(title.Thumbnail))
            {
                throw new FileNotFoundException("Thumbnail not found.");
            }

            var fileStreamResult = await _fileService.DownloadAsync(title.Thumbnail);

            return fileStreamResult;
        }
        public async Task<IEnumerable<TitleResponseDTO>> SearchAsync(string query)
        {
            var titles = await _titleRepository.SearchAsync(query);
            return titles.Select(TitleResponseDTO.ValueOf);
        }
    }
}