using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.DatabaseContext.DatabaseContext;

namespace Error404.Repository.Repository
{
   public class CategoryRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();

        public bool Add(Category category)
        {
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Category category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
            }
            else
            {
                return false;
            }

            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(Category category)
        {
            Category aCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (aCategory != null)
            {
                aCategory.Code = category.Code;
                aCategory.Name = category.Name;
            }
            else
            {
                return false;
            }
            return _dbContext.SaveChanges() > 0;
        }

        public List<Category> GetAll()
        {
            var categories = _dbContext.Categories.ToList();
            //foreach (var category in categories)
            //{
            //    _dbContext.Entry(category)
            //        .Collection(c => c.Products)
            //        .Load();
            //}
            return categories;
        }

        public Category GetById(int id)
        {
            Category category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            //_dbContext.Entry(category)
            //    .Collection(c => c.Products)
            //    .Load();
            return category;
        }

        public bool ExistCategoryCode(Category category)
        {
            bool isExistCategoryCode = false;
            Category cate = _dbContext.Categories.FirstOrDefault(c => c.Code == category.Code);
            if (cate != null)
            {
                isExistCategoryCode = true;
            }
            return isExistCategoryCode;
        }

        public bool ExistCategoryName(Category category)
        {
            bool isExistCategoryName = false;
            Category cate = _dbContext.Categories.FirstOrDefault(c => c.Name == category.Name);
            if (cate != null)
            {
                isExistCategoryName = true;
            }
            return isExistCategoryName;
        }

    }
}
