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
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Recipe>> GetByIdAsync(int id)
        {
            var found = await _unitOfWork.RecipeRepository.GetByIdAsync(id);

			if (found == null)
			{
				return new Response<Recipe> { Ok = false, Mensaje = "Receta no encontrada", Datos = found };
			}
			else
			{
				return new Response<Recipe> { Ok = true, Mensaje = "Receta obtenida", Datos = found };
			}
        }

        public async Task<Response<IEnumerable<Recipe>>> GetAllAsync()
        {
            return new Response<IEnumerable<Recipe>> { Ok = true, Mensaje = "Recetas obtenidas", Datos = await _unitOfWork.RecipeRepository.GetAllAsync() };
        }

        public async Task<Response<Recipe>> Create(Recipe newRecipe)
        {
            RecipeValidator validator = new();

			var resultValidation = await validator.ValidateAsync(newRecipe);

			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());
			}

			var entityAdded = await _unitOfWork.RecipeRepository.AddAsync(newRecipe);
			await _unitOfWork.CommitAsync();

			return new Response<Recipe> { Ok = true, Mensaje = "Receta creada con éxito", Datos = entityAdded };
        }

        public async Task<Response<Recipe>> Update(int recipeUpdatedId, Recipe recipeUpdated)
        {
            RecipeValidator validator = new();

			var resultValidation = await validator.ValidateAsync(recipeUpdated);
			if (!resultValidation.IsValid)
			{
				throw new ArgumentException(resultValidation.Errors[0].ErrorMessage.ToString());

			}

			Recipe recipeToUpdate = await _unitOfWork.RecipeRepository.GetByIdAsync(recipeUpdatedId);

			if (recipeToUpdate == null)
				throw new ArgumentException("Id de la receta a actualizar inválido");

			recipeToUpdate.Name = recipeUpdated.Name;
			recipeToUpdate.Description = recipeUpdated.Description;
			recipeToUpdate.DifficultyLevel = recipeUpdated.DifficultyLevel;
			recipeToUpdate.Visibility = recipeUpdated.Visibility;
			recipeToUpdate.Type = recipeUpdated.Type;

			await _unitOfWork.CommitAsync();

			return new Response<Recipe> { Ok = true, Mensaje = "Receta actualizada con éxito", Datos = await _unitOfWork.RecipeRepository.GetByIdAsync(recipeUpdatedId) };
        }

        public async Task<Response<Recipe>> Remove(int recipeId)
        {
            Recipe recipeFound = await _unitOfWork.RecipeRepository.GetByIdAsync(recipeId);
            _unitOfWork.RecipeRepository.Remove(recipeFound);
            await _unitOfWork.CommitAsync();
            return new Response<Recipe> { Ok = true, Mensaje = "Receta eliminada", Datos = null };
        }
    }
}