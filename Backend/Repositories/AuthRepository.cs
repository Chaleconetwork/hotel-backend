using AutoMapper;
using Backend.Data;
using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using static Library.Utils;

namespace Backend.Repositories
{
    public class AuthRepository : IAuthInterface
    {
        private readonly Context _context;
        private readonly IJwtTokenGeneratorInterface _jwtTokenGeneratorInterface;
        private readonly IMapper _map;

        public AuthRepository(Context context, IJwtTokenGeneratorInterface jwtTokenGeneratorInterface, IMapper map)
        {
            _context = context;
            _jwtTokenGeneratorInterface = jwtTokenGeneratorInterface;
            _map = map;
        }

        public async Task<UserDTO>? RegisterUser(UserDTO dto)
        {
            var users = await _context.Users.OrderBy(i => i.UserId).AsNoTracking().ToListAsync();
            return null;
        }

        public async Task<UserDTO>? AuthUser(UserDTO dto)
        {
            if (String.IsNullOrEmpty(dto.Username) || String.IsNullOrEmpty(dto.Password))
            {
                UserDTO error = new() { IsError = true, MessageError = "El usuario o contraseña no son validos" };
                return error;
            }

            //Verificar si usuario existe
            var checkUser = await _context.Users.Where(i => i.Username == dto.Username).AsNoTracking().FirstOrDefaultAsync();

            if (checkUser == null)
            {
                UserDTO error = new() { IsError = true, MessageError = "Este usuario no existe" };
                return error;
            }

            //Verificar contraseña
            if (Deencrypt(checkUser?.Password) != dto.Password)
            {
                UserDTO error = new() { IsError = true, MessageError = "Contraseña incorrecta" };
                return error;
            }

            UserDTO userDto = _map.Map<UserDTO>(checkUser);
            userDto.Token = _jwtTokenGeneratorInterface.GenerateJwtToken(userDto);
            userDto.Password = null;

            return userDto;
        }
    }
}
