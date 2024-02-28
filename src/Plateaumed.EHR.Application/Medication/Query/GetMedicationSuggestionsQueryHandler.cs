using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication.Query;

public class GetMedicationSuggestionsQueryHandler : IGetMedicationSuggestionsQueryHandler
{ 
    private readonly IRepository<Product, long> _productRepository;
    private readonly IRepository<GenericDrug, long> _genericDrugRepository; 
    private readonly IRepository<ProductCategory, long> _productCategoryRepository; 
    private readonly IRepository<ProductCategoryMapping, long> _productCategoryMappingRepository;
    private readonly IRepository<AllInputs.Medication, long> _medicationRepository;

    public GetMedicationSuggestionsQueryHandler(IRepository<Product, long> productRepository, IRepository<GenericDrug, long> genericDrugRepository, IRepository<ProductCategory, long> productCategoryRepository, IRepository<ProductCategoryMapping, long> productCategoryMappingRepository, IRepository<AllInputs.Medication, long> medicationRepository)
    {
        _productRepository = productRepository;
        _genericDrugRepository = genericDrugRepository;
        _productCategoryRepository = productCategoryRepository;
        _productCategoryMappingRepository = productCategoryMappingRepository;
        _medicationRepository = medicationRepository;
    }

    public async Task<List<SearchMedicationForReturnDto>> Handle()
    {
        var productIds = await (from med in _medicationRepository.GetAll()
            group med by med.ProductId into productGroup
            orderby productGroup.Count() descending, productGroup.Key
            select productGroup.Key).Take(20).ToListAsync();
        
        var suggestionList = await (from genericDrug in _genericDrugRepository.GetAll()
            join product in _productRepository.GetAll() on genericDrug.Id equals product.GenericsSctId
            join categoryMapping in _productCategoryMappingRepository.GetAll() on product.Id equals categoryMapping.ProductId
            join category in _productCategoryRepository.GetAll() on categoryMapping.CategoryId equals category.Id
            
            where productIds.Contains(product.Id)
            select new SearchMedicationForReturnDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                GenericName = genericDrug.GenericSctName,
                ActiveIngredient = product.ActiveIngredients,
                BrandName = product.BrandName,
                CategoryName = category.CategoryName,
                DoseForm = product.DoseFormName.ToLower(),
                DoseStrength = product.DoseStrengthName,
                Source = "Suggestion"
            }).ToListAsync(); 

        return suggestionList; 
    }
}