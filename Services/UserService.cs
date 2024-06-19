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
        private readonly ITitleRepository _titleRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, ITitleRepository titleRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _titleRepository = titleRepository;
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

            var existingByUsername = await _userRepository.GetByUsernameAsync(userDTO.Username);
            if (existingByUsername != null)
            {
                throw new InvalidOperationException("Este nome de usuário já está em uso.");
            }

            var existingByEmail = await _userRepository.GetByEmailAsync(userDTO.Email);
            if (existingByEmail != null)
            {
                throw new InvalidOperationException("Este email já está em uso.");
            }

            var user = _mapper.Map<User>(userDTO);
            user.Password = _passwordHasher.Hash(userDTO.Password);
            user.CreatedAt = DateTime.UtcNow;
            user = await _userRepository.AddAsync(user);
            return UserResponseDTO.ValueOf(user);
        }

        // UserService.cs

        public async Task<UserResponseDTO> UpdateAsync(int id, UserUpdateDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException($"Usuário com o id '{id}' não encontrado.");
            }


            user.Email = userDTO.Email;
            user.Username = userDTO.Username;

            if (!string.IsNullOrWhiteSpace(userDTO.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(userDTO.CurrentPassword))
                {
                    throw new ArgumentException("A senha atual é necessária para alterar a senha.");
                }

                if (!_passwordHasher.Verify(user.Password, userDTO.CurrentPassword))
                {
                    throw new ArgumentException("A senha atual está incorreta.");
                }

                if (userDTO.NewPassword != userDTO.RepeatPassword)
                {
                    throw new ArgumentException("A nova senha e a repetição da senha não coincidem.");
                }

                user.Password = _passwordHasher.Hash(userDTO.NewPassword);
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

        public async Task<(UserResponseDTO user, string message)> AddToMyList(int uid, int tid)
        {
            var user = await _userRepository.GetByIdAsync(uid);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var title = await _titleRepository.GetByIdAsync(tid);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            string message;

            if (user.MyList.Contains(title))
            {
                user.MyList.Remove(title);
                message = "Removido da minha lista";
            }
            else
            {
                user.MyList.Add(title);
                message = "Adicionado à minha lista";
            }

            await _userRepository.UpdateAsync(user);

            return (UserResponseDTO.ValueOf(user), message);
        }

        public async Task<(UserResponseDTO user, string message)> RemoveFromMyList(int uid, int tid)
        {
            var user = await _userRepository.GetByIdAsync(uid);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var title = await _titleRepository.GetByIdAsync(tid);

            if (title == null)
            {
                throw new KeyNotFoundException("Title not found.");
            }

            if (user.MyList.Contains(title))
            {
                user.MyList.Remove(title);
                await _userRepository.UpdateAsync(user);
                return (UserResponseDTO.ValueOf(user), "Removido da minha lista");
            }
            else
            {
                return (UserResponseDTO.ValueOf(user), "Título não encontrado na lista");
            }
        }
    }
}