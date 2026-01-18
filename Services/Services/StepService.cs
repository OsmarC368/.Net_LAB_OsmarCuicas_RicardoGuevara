using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Responses;
using Services.Validators;

namespace Services.Services
{
    public class StepService : IStepService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StepService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Step>> GetByIdAsync(int id)
        {
            var found = await _unitOfWork.StepRepository.GetByIdAsync(id);

			if (found == null)
			{
				return new Response<Step> { Ok = false, Mensaje = "Paso no encontrado", Datos = found };
			}
			else
			{
				return new Response<Step> { Ok = true, Mensaje = "Paso obtenido", Datos = found };
			}
        }

        public async Task<Response<IEnumerable<Step>>> GetAllAsync()
        {
            return new Response<IEnumerable<Step>> { Ok = true, Mensaje = "Pasos obtenidas", Datos = await _unitOfWork.StepRepository.GetAllAsync() };
        }

        public async Task<Response<Step>> Create(Step newStep)
        {
            StepValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newStep);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

			var entityAdded = await _unitOfWork.StepRepository.AddAsync(newStep);
			await _unitOfWork.CommitAsync();

			return new Response<Step> { Ok = true, Mensaje = "Paso creada con éxito", Datos = entityAdded };
        }

        public async Task<Response<Step>> Update(int stepUpdatedId, Step stepUpdated)
        {
            StepValidator validator = new();

			var resultValidation = await validator.ValidateAsync(stepUpdated);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());

			}

			Step stepToUpdate = await _unitOfWork.StepRepository.GetByIdAsync(stepUpdatedId);

			if (stepToUpdate == null)
				throw new ArgumentException("Id del paso a actualizar inválido");

			stepToUpdate.Name = stepUpdated.Name;
			stepToUpdate.Description = stepUpdated.Description;
			stepToUpdate.Duration = stepUpdated.Duration;

			await _unitOfWork.CommitAsync();

			return new Response<Step> { Ok = true, Mensaje = "Paso actualizada con éxito", Datos = await _unitOfWork.StepRepository.GetByIdAsync(stepUpdatedId) };
        }

        public async Task<Response<Step>> Remove(int stepId)
        {
            Step stepFound = await _unitOfWork.StepRepository.GetByIdAsync(stepId);
            _unitOfWork.StepRepository.Remove(stepFound);
            await _unitOfWork.CommitAsync();
            return new Response<Step> { Ok = true, Mensaje = "Paso eliminada", Datos = null };
        }
    }
}