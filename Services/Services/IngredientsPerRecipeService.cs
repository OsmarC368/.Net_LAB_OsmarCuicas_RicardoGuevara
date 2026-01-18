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
    public class IngredientsPerRecipeService :  IIngredientsPerRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IngredientsPerRecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IngredientsPerRecipe>> GetByIdAsync(int id)
        {
            var found = await _unitOfWork.IngredientsPerRecipeRepository.GetByIdAsync(id);

			if (found == null)
			{
				return new Response<IngredientsPerRecipe> { Ok = false, Mensaje = "Ingredientes por receta no encontrado", Datos = found };
			}
			else
			{
				return new Response<IngredientsPerRecipe> { Ok = true, Mensaje = "Ingredientes por receta obtenido", Datos = found };
			}
        }

        public async Task<Response<IEnumerable<IngredientsPerRecipe>>> GetAllAsync()
        {
            return new Response<IEnumerable<IngredientsPerRecipe>> { Ok = true, Mensaje = "Ingredientes por receta obtenidos", Datos = await _unitOfWork.IngredientsPerRecipeRepository.GetAllAsync() };
        }

        public async Task<Response<IngredientsPerRecipe>> Create(IngredientsPerRecipe newIngredientsPerRecipe)
        {
            IngredientsPerRecipeValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newIngredientsPerRecipe);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

			var entityAdded = await _unitOfWork.IngredientsPerRecipeRepository.AddAsync(newIngredientsPerRecipe);
			await _unitOfWork.CommitAsync();

			return new Response<IngredientsPerRecipe> { Ok = true, Mensaje = "Ingredientes por receta creada con éxito", Datos = entityAdded };
        }

        public async Task<Response<IngredientsPerRecipe>> Update(int ingredientsPerRecipeUpdatedId, IngredientsPerRecipe ingredientsPerRecipeUpdated)
        {
            IngredientsPerRecipeValidator validator = new();

			var resultValidation = await validator.ValidateAsync(ingredientsPerRecipeUpdated);
			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());

			}

			IngredientsPerRecipe ingredientsPerRecipeToUpdate = await _unitOfWork.IngredientsPerRecipeRepository.GetByIdAsync(ingredientsPerRecipeUpdatedId);

			if (ingredientsPerRecipeToUpdate == null)
				throw new ArgumentException("Id del ingrediente por receta a actualizar inválido");

			ingredientsPerRecipeToUpdate.amount = ingredientsPerRecipeUpdated.amount;

			await _unitOfWork.CommitAsync();

			return new Response<IngredientsPerRecipe> { Ok = true, Mensaje = "Ingredientes por receta actualizada con éxito", Datos = await _unitOfWork.IngredientsPerRecipeRepository.GetByIdAsync(ingredientsPerRecipeUpdatedId) };
        }

        public async Task<Response<IngredientsPerRecipe>> Remove(int ingredientsPerRecipeId)
        {
            IngredientsPerRecipe ingredientsPerRecipeFound = await _unitOfWork.IngredientsPerRecipeRepository.GetByIdAsync(ingredientsPerRecipeId);
            _unitOfWork.IngredientsPerRecipeRepository.Remove(ingredientsPerRecipeFound);
            await _unitOfWork.CommitAsync();
            return new Response<IngredientsPerRecipe> { Ok = true, Mensaje = "Ingredientes por receta eliminada", Datos = null };
        }
    }
}