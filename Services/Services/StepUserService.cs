using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Entities;
using Core.Interfaces.Services;
using Core.Responses;
using Services.Validators;

namespace Services.Services
{
    public class StepUserService : IStepUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StepUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<StepUser>> GetByIdAsync(int id)
        {
            var found = await _unitOfWork.StepUserRepository.GetByIdAsync(id);

			if (found == null)
			{
				return new Response<StepUser> { Ok = false, Mensaje = "Paso de Usuario no encontrado", Datos = found };
			}
			else
			{
				return new Response<StepUser> { Ok = true, Mensaje = "Paso de  Usuario obtenido", Datos = found };
			}
        }

        public async Task<Response<IEnumerable<StepUser>>> GetAllAsync()
        {
            return new Response<IEnumerable<StepUser>> { Ok = true, Mensaje = "Pasos de usuario obtenidas", Datos = await _unitOfWork.StepUserRepository.GetAllAsync() };
        }

        public async Task<Response<StepUser>> Create(StepUser newStepUser)
        {
            StepUserValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newStepUser);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

			var entityAdded = await _unitOfWork.StepUserRepository.AddAsync(newStepUser);
			await _unitOfWork.CommitAsync();

			return new Response<StepUser> { Ok = true, Mensaje = "Paso de Usuario creada con éxito", Datos = entityAdded };
        }

        public async Task<Response<StepUser>> Update(int stepUserUpdatedId, StepUser stepUserUpdated)
        {
            StepUserValidator validator = new();

			var resultValidation = await validator.ValidateAsync(stepUserUpdated);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());

			}

			StepUser stepUserToUpdate = await _unitOfWork.StepUserRepository.GetByIdAsync(stepUserUpdatedId);

			if (stepUserToUpdate == null)
				throw new ArgumentException("Id del paso de usuario a actualizar inválido");

			stepUserToUpdate.comment = stepUserUpdated.comment;
			stepUserToUpdate.completed = stepUserUpdated.completed;

			await _unitOfWork.CommitAsync();

			return new Response<StepUser> { Ok = true, Mensaje = "Paso de Usuario actualizada con éxito", Datos = await _unitOfWork.StepUserRepository.GetByIdAsync(stepUserUpdatedId) };
        }

        public async Task<Response<StepUser>> Remove(int stepUserId)
        {
            StepUser stepUserFound = await _unitOfWork.StepUserRepository.GetByIdAsync(stepUserId);
            _unitOfWork.StepUserRepository.Remove(stepUserFound);
            await _unitOfWork.CommitAsync();
            return new Response<StepUser> { Ok = true, Mensaje = "Paso de Usuario eliminada", Datos = null };
        }
        
    }
}