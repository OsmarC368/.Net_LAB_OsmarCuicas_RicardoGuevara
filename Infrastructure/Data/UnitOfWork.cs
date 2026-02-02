using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private UserTypeRepository? _userTypeRepository;
        private MeasureRepository? _measureRepository;
        private IUserRepository? _userRepository;
        private IIngredientRepository? _ingredientRepository;
        private IIngredientsPerRecipeRepository? _ingredientsPerRecipeRepository;
        private IRecipeRepository? _recipeRepository;
        private IStepRepository? _stepRepository;
        private IStepUserRepository? _stepUserRepository;

        public UnitOfWork(AppDbContext context) { this._context = context; }

        public IUserTypeRepository UserTypeRepository => _userTypeRepository ??= new UserTypeRepository(_context);
        public IMeasureRepository MeasureRepository => _measureRepository ??= new MeasureRepository(_context);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IIngredientRepository IngredientRepository => _ingredientRepository ??= new IngredientRepository(_context);
        public IIngredientsPerRecipeRepository IngredientsPerRecipeRepository => _ingredientsPerRecipeRepository ??= new IngredientsPerRecipeRepository(_context);
        public IStepRepository StepRepository => _stepRepository ??= new StepRepository(_context);
        public IRecipeRepository RecipeRepository => _recipeRepository ??= new RecipeRepository(_context);
        public IStepUserRepository StepUserRepository => _stepUserRepository ??= new StepUserRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
