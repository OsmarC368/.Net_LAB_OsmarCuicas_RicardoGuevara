using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Responses;
using Services.Validators;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<User>> GetByIdAsync(int id)
		{
			var found = await _unitOfWork.UserRepository.GetByIdAsync(id);

			if (found == null)
			{
				return new Response<User> { Ok = false, Mensaje = "Usuario no encontrado", Datos = found };
			}
			else
			{
				return new Response<User> { Ok = true, Mensaje = "Usuario obtenido", Datos = found };
			}
		}

        public async Task<Response<IEnumerable<User>>> GetAllAsync()
		{
			return new Response<IEnumerable<User>> { Ok = true, Mensaje = "Usuarios obtenidos", Datos = await _unitOfWork.UserRepository.GetAllAsync() };
		}

        public async Task<Response<User>> Create(User newEntity)
		{
			UserValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newEntity);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

			var entityAdded = await _unitOfWork.UserRepository.AddAsync(newEntity);
			await _unitOfWork.CommitAsync();

			return new Response<User> { Ok = true, Mensaje = "Usuario creado con éxito", Datos = entityAdded };

		}

        public async Task<Response<User>> Update(int entityToUpdateId, User newValuesEntity)
		{
			UserValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newValuesEntity);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());

			}

			User UserToUpdate = await _unitOfWork.UserRepository.GetByIdAsync(entityToUpdateId);

			if (UserToUpdate == null)
				throw new ArgumentException("Id del usuario a actualizar inválido");

			UserToUpdate.Name = newValuesEntity.Name;
			UserToUpdate.Lastname = newValuesEntity.Lastname;
			UserToUpdate.Email = newValuesEntity.Email;
            UserToUpdate.Password = newValuesEntity.Password;

			await _unitOfWork.CommitAsync();

			return new Response<User> { Ok = true, Mensaje = "Usuario actualizado con éxito", Datos = await _unitOfWork.UserRepository.GetByIdAsync(entityToUpdateId) };

		}

        public async Task<Response<User>> Remove(int entityId)
		{
			User user = await _unitOfWork.UserRepository.GetByIdAsync(entityId);
			_unitOfWork.UserRepository.Remove(user);
			await _unitOfWork.CommitAsync();
			return new Response<User> { Ok = true, Mensaje = "Usuario eliminado", Datos = null };
		}

        public async Task<Response<ResponseLogin>> Login(string email, string password)
		{
			User user = await _unitOfWork.UserRepository.GetByEmail(email);

			if (user == null)
			{
				return new Response<ResponseLogin>
				{
					Ok = false,
					Mensaje = "Email y/o contraseña incorrecta.",
					Datos = null
				};
			}

			bool passwordOk = BCrypt.Net.BCrypt.Verify(password, user.Password);

			if (!passwordOk)
			{
				return new Response<ResponseLogin>
				{
					Ok = false,
					Mensaje = "Email y/o contraseña incorrecta.",
					Datos = null
				};
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("superclaveultrasegura_12345678900000!");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Email, user.Email),
					new Claim("id", user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddMinutes(7),
				Issuer = "MiApiBackend",
    			Audience = "MiApiClientes",
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			string userToken = tokenHandler.WriteToken(token);



			return new Response<ResponseLogin> { Ok = true, Mensaje = "Inicio de sesión correcto.", Datos = new ResponseLogin {jwt = userToken, idusersession = user.Id } };
		}

        public async Task<Response<RegisterResponse>> Register(RegisterResponse registerResponse)
        {
            if (string.IsNullOrEmpty(registerResponse.Name) ||
				string.IsNullOrEmpty(registerResponse.Lastname) ||
				string.IsNullOrEmpty(registerResponse.Email) ||
				string.IsNullOrEmpty(registerResponse.Password))
			{
				return new Response<RegisterResponse>
				{
					Ok = false,
					Mensaje = "Todos los campos son obligatorios",
					Datos = null
				};
			}

			var existingUser = await _unitOfWork.UserRepository.GetByEmail(registerResponse.Email);
			if (existingUser != null)
			{
				return new Response<RegisterResponse>
				{
					Ok = false,
					Mensaje = "El email ya está registrado",
					Datos = null
				};
			}

			string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerResponse.Password);

			var user = new User
			{
				Name = registerResponse.Name,
				Lastname = registerResponse.Lastname,
				Email = registerResponse.Email,
				Password = hashedPassword,
				UserTypeID = registerResponse.UserTypeID,
			};

			await _unitOfWork.UserRepository.AddAsync(user);
			await _unitOfWork.CommitAsync();

			var response = new RegisterResponse
			{
				Name = user.Name,
				Lastname = user.Lastname,
				Email = user.Email,
				UserTypeID = user.UserTypeID
			};

			return new Response<RegisterResponse>
			{
				Ok = true,
				Mensaje = "Usuario registrado correctamente",
				Datos = response
			};
        }
    }
}
