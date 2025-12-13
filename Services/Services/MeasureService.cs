using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Services.Validators;

namespace Services.Services
{
    public class MeasureService : IMeasureService
    {
         private readonly IUnitOfWork _unitOfWork;
        public MeasureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Measure> GetById(int id)
        {
            return await _unitOfWork.MeasureRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Measure>> GetAll()
        {
            return await _unitOfWork.MeasureRepository.GetAllAsync();
        }

        public async Task<Measure> Create(Measure newMeasure)
        {
            MeasureValidator validator = new ();
            var validatorResult = await validator.ValidateAsync(newMeasure);

            if (validatorResult.IsValid)
            {
                await _unitOfWork.MeasureRepository.AddAsync(newMeasure);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());
            }

            return newMeasure;
        }

        public async Task<Measure> Update(int measureUpdatedId, Measure measureUpdated)
        {
            MeasureValidator validator = new ();
            var validatorResult = await validator.ValidateAsync(measureUpdated);
            if (!validatorResult.IsValid)
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());

            Measure measure = await _unitOfWork.MeasureRepository.GetByIdAsync(measureUpdatedId);
            if (measure == null)
                throw new ArgumentException("Invalid Measure ID While Updating");

            measure.name = measureUpdated.name;
            measure.description = measureUpdated.description;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.MeasureRepository.GetByIdAsync(measureUpdatedId);
        }

        public async Task Delete(int measureId)
        {
            Measure measureFound = await _unitOfWork.MeasureRepository.GetByIdAsync(measureId);
            _unitOfWork.MeasureRepository.Remove(measureFound);
            await _unitOfWork.CommitAsync();
        }
    }
}