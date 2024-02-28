using NUglify.Helpers;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Payload;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    internal class DefaultOccupationCategoriesCreator
    {


        private readonly EHRDbContext _context;

        public DefaultOccupationCategoriesCreator(EHRDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateOccupationCategories();
        }

        private void CreateOccupationCategories()
        {
            if (!_context.OccupationCategories.ToList().Any())
            {

                _context.OccupationCategories.AddRange(GetOccupationCatergories());
                _context.SaveChanges();
            }
        }

        private static List<OccupationCategory> GetOccupationCatergories()
        {

            var JsonData = OccupationCategoriesHelper.GetDeserializedOccupationCategoriesData();
            List<OccupationCategory> OccupationCategories = new();

            foreach (var category in JsonData)
            {
                var newCategory = new OccupationCategory
                {
                    Id = category.id,
                    Name = category.name,
                };

                OccupationCategories.Add(newCategory);
            }

            return OccupationCategories;
        }
    }
}
