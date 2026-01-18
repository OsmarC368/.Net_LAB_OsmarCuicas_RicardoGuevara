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
    public class MeasureService : IMeasureService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeasureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Measure>> GetByIdAsync(int id)
        {
            var found = await _unitOfWork.MeasureRepository.GetByIdAsync(id);

			if (found == null)
			{
				return new Response<Measure> { Ok = false, Mensaje = "Medida no encontrada", Datos = found };
			}
			else
			{
				return new Response<Measure> { Ok = true, Mensaje = "Medida obtenida", Datos = found };
			}
        }

        public async Task<Response<IEnumerable<Measure>>> GetAllAsync()
        {
            return new Response<IEnumerable<Measure>> { Ok = true, Mensaje = "Medidas obtenidas", Datos = await _unitOfWork.MeasureRepository.GetAllAsync() };
        }

        public async Task<Response<Measure>> Create(Measure newMeasure)
        {
            MeasureValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newMeasure);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

            var exists = await _unitOfWork.MeasureRepository.GetByName(newMeasure.name);
            if (exists != null)
                throw new ArgumentException("La medida ya existe");

			var entityAdded = await _unitOfWork.MeasureRepository.AddAsync(newMeasure);
			await _unitOfWork.CommitAsync();

			return new Response<Measure> { Ok = true, Mensaje = "Medida creada con éxito", Datos = entityAdded };
        }

        public async Task<Response<Measure>> Update(int measureUpdatedId, Measure measureUpdated)
        {
            MeasureValidator validator = new();

			var resultValidation = await validator.ValidateAsync(measureUpdated);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());

			}

            var existingMeasure = await _unitOfWork.MeasureRepository
                .GetByName(measureUpdated.name);

            if (existingMeasure != null && existingMeasure.id != measureUpdatedId)
            {
                throw new ArgumentException("Ya existe otra medida con ese nombre");
            }

			Measure measureToUpdate = await _unitOfWork.MeasureRepository.GetByIdAsync(measureUpdatedId);

			if (measureToUpdate == null)
				throw new ArgumentException("Id de la medida a actualizar inválido");

			measureToUpdate.name = measureUpdated.name;
			measureToUpdate.symbol = measureUpdated.symbol;

			await _unitOfWork.CommitAsync();

			return new Response<Measure> { Ok = true, Mensaje = "Medida actualizada con éxito", Datos = await _unitOfWork.MeasureRepository.GetByIdAsync(measureUpdatedId) };
        }

        public async Task<Response<Measure>> Remove(int measureId)
        {
            Measure measureFound = await _unitOfWork.MeasureRepository.GetByIdAsync(measureId);
            _unitOfWork.MeasureRepository.Remove(measureFound);
            await _unitOfWork.CommitAsync();
            return new Response<Measure> { Ok = true, Mensaje = "Medida eliminada", Datos = null };
        }
    }
}