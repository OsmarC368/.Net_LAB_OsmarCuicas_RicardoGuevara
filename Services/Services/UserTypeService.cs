using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Services.Validators;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Responses;

namespace Services.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserType>> GetByIdAsync(int id)
        {
            var found = await _unitOfWork.UserTypeRepository.GetByIdAsync(id);

			if (found == null)
			{
				return new Response<UserType> { Ok = false, Mensaje = "Tipo de usuario no encontrado", Datos = found };
			}
			else
			{
				return new Response<UserType> { Ok = true, Mensaje = "Tipo de usuario obtenido", Datos = found };
			}
        }

        public async Task<Response<IEnumerable<UserType>>> GetAllAsync()
        {
			return new Response<IEnumerable<UserType>> { Ok = true, Mensaje = "Tipos de usuarios obtenidos", Datos = await _unitOfWork.UserTypeRepository.GetAllAsync() };
        }

        public async Task<Response<UserType>> Create(UserType newUserType)
        {
            UserTypeValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newUserType);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

			var entityAdded = await _unitOfWork.UserTypeRepository.AddAsync(newUserType);
			await _unitOfWork.CommitAsync();

			return new Response<UserType> { Ok = true, Mensaje = "Tipo de usuario creado con éxito", Datos = entityAdded };
        }

        public async Task<Response<UserType>> Update(int userTypeUpdatedId, UserType userTypeUpdated)
        {
            UserTypeValidator validator = new();

			var resultValidation = await validator.ValidateAsync(userTypeUpdated);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());

			}

			UserType UserTypeToUpdate = await _unitOfWork.UserTypeRepository.GetByIdAsync(userTypeUpdatedId);

			if (UserTypeToUpdate == null)
				throw new ArgumentException("Id del tipo de usuario a actualizar inválido");

			UserTypeToUpdate.name = userTypeUpdated.name;
			UserTypeToUpdate.description = userTypeUpdated.description;

			await _unitOfWork.CommitAsync();

			return new Response<UserType> { Ok = true, Mensaje = "Tipo de usuario actualizado con éxito", Datos = await _unitOfWork.UserTypeRepository.GetByIdAsync(userTypeUpdatedId) };
        }

        public async Task<Response<UserType>> Remove(int userTypeId)
        {
            UserType userTypeFound = await _unitOfWork.UserTypeRepository.GetByIdAsync(userTypeId);
            _unitOfWork.UserTypeRepository.Remove(userTypeFound);
            await _unitOfWork.CommitAsync();
            return new Response<UserType> { Ok = true, Mensaje = "Tipo de usuario eliminado", Datos = null };
        }
    }
}