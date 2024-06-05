using Api.Repositories;
using AutoMapper;
using Api.DTOs;
using Api.Models;

namespace Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(UserResponseDTO.ValueOf);
        }

        public async Task<UserResponseDTO> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return UserResponseDTO.ValueOf(user);
        }

        public async Task<UserResponseDTO> AddAsync(UserDTO userDTO)
        {
            if (userDTO.Password != userDTO.Repeat)
            {
                throw new ArgumentException("As senhas não coincidem.");
            }

            var existing = await _userRepository.GetByUsernameAsync(userDTO.Username);

            if (existing != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            var user = _mapper.Map<User>(userDTO);
            user.Password = _passwordHasher.Hash(userDTO.Password);
            user.CreatedAt = DateTime.UtcNow;
            user = await _userRepository.AddAsync(user);
            return UserResponseDTO.ValueOf(user);
        }

        public async Task<UserResponseDTO> UpdateAsync(int id, UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.Username = userDTO.Username;
            user.Password = userDTO.Password;

            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                user.Password = _passwordHasher.Hash(userDTO.Password);
            }

            user = await _userRepository.UpdateAsync(user);
            return UserResponseDTO.ValueOf(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserResponseDTO> GetByUsernameOrEmailAndPassword(string usernameOrEmail, string password)
        {
            var user = await _userRepository.GetByUsernameOrEmailAsync(usernameOrEmail);

            if (user == null || !_passwordHasher.Verify(user.Password, password))
            {
                return null;
            }

            return UserResponseDTO.ValueOf(user);
        }
    }
}