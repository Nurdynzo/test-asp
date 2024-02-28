

using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Payload;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class DefaultOccupationsCreator
    {

        private readonly EHRDbContext _context;

        public DefaultOccupationsCreator(EHRDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateOccupations();
        }

        private void CreateOccupations()
        {
            if (!_context.Occupations.ToList().Any())
            {

                _context.Occupations.AddRange(GetOccupations());
                _context.SaveChanges();
            }
        }

        private static List<Occupation> GetOccupations()
        {

            var JsonData = OccupationsHelper.GetDeserializedOccupationsData();
            List<Occupation> Occupations = new();

            foreach (var occupation in JsonData)
            {
                var newCategory = new Occupation
                {
                    Id = occupation.id,
                    Name = occupation.name,
                    OccupationCategoryId = occupation.categoryId, 
                };

                Occupations.Add(newCategory);
            }

            return Occupations;
        }
    }
}
