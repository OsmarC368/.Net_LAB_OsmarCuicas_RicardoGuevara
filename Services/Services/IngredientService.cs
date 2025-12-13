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
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Ingredient>> Add(Ingredient newEntity)
		{
			IngredientValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newEntity);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

			var entityadded = await _unitOfWork.IngredientRepository.AddAsync(newEntity);
			await _unitOfWork.CommitAsync();

			return new Response<Ingredient> { Ok = true, Mensaje = "Ingrediente creado con éxito", Datos = entityadded };

		}

        public async Task<Response<Ingredient>> GetByIdAsync(int id)
		{
			var found = await _unitOfWork.IngredientRepository.GetByIdAsync(id);

			if (found == null)
			{
				return new Response<Ingredient> { Ok = false, Mensaje = "Ingrediente no encontrado", Datos = found };
			}
			else
			{
				return new Response<Ingredient> { Ok = true, Mensaje = "Ingrediente obtenido", Datos = found };
			}
		}

        public async Task<Response<IEnumerable<Ingredient>>> GetAllAsync()
		{
			return new Response<IEnumerable<Ingredient>> { Ok = true, Mensaje = "Ingredientes obtenidos", Datos = await _unitOfWork.IngredientRepository.GetAllAsync() };
		}

        public async Task<Response<Ingredient>> Create(Ingredient newEntity)
		{
			IngredientValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newEntity);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

			var entityAdded = await _unitOfWork.IngredientRepository.AddAsync(newEntity);
			await _unitOfWork.CommitAsync();

			return new Response<Ingredient> { Ok = true, Mensaje = "Ingrediente creado con éxito", Datos = entityAdded };

		}

        public async Task<Response<Ingredient>> Update(int entityToUpdateId, Ingredient newValuesEntity)
		{
			IngredientValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newValuesEntity);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());

			}

			Ingredient IngredientToUpdate = await _unitOfWork.IngredientRepository.GetByIdAsync(entityToUpdateId);

			if (IngredientToUpdate == null)
				throw new ArgumentException("Id del Ingrediente a actualizar inválido");

			IngredientToUpdate.Name = newValuesEntity.Name;
			IngredientToUpdate.Type = newValuesEntity.Type;

			await _unitOfWork.CommitAsync();

			return new Response<Ingredient> { Ok = true, Mensaje = "Ingrediente actualizado con éxito", Datos = await _unitOfWork.IngredientRepository.GetByIdAsync(entityToUpdateId) };

		}

        public async Task<Response<Ingredient>> Remove(int entityId)
		{
			Ingredient ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(entityId);
			_unitOfWork.IngredientRepository.Remove(ingredient);
			await _unitOfWork.CommitAsync();
			return new Response<Ingredient> { Ok = true, Mensaje = "Ingrediente eliminado", Datos = null };
		}
    }
}
