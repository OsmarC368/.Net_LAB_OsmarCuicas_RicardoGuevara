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
    public class UserTypeService : IUserTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserType> GetById(int id)
        {
            return await _unitOfWork.UserTypeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<UserType>> GetAll()
        {
            return await _unitOfWork.UserTypeRepository.GetAllAsync();
        }

        public async Task<UserType> Create(UserType newUserType)
        {
            UserTypeValidator validator = new ();
            var validatorResult = await validator.ValidateAsync(newUserType);

            if (validatorResult.IsValid)
            {
                await _unitOfWork.UserTypeRepository.AddAsync(newUserType);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());

            }
            return newUserType;
        }

        public async Task<UserType> Update(int userTypeUpdatedId, UserType userTypeUpdated)
        {
            UserTypeValidator validator = new ();
            var validatorResult = await validator.ValidateAsync(userTypeUpdated);
            if (!validatorResult.IsValid)
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());

            UserType userType = await _unitOfWork.UserTypeRepository.GetByIdAsync(userTypeUpdatedId);
            if (userType == null)
                throw new ArgumentException("Invalid User Type ID While Updating");

            userType.name = userTypeUpdated.name;
            userType.description = userTypeUpdated.description;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.UserTypeRepository.GetByIdAsync(userTypeUpdatedId);
        }

        public async Task Delete(int userTypeId)
        {
            UserType userTypeFound = await _unitOfWork.UserTypeRepository.GetByIdAsync(userTypeId);
            _unitOfWork.UserTypeRepository.Remove(userTypeFound);
            await _unitOfWork.CommitAsync();
        }
    }
}