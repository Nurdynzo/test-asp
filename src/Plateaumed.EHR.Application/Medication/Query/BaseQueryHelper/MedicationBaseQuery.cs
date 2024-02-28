using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication.Query.BaseQueryHelper;

public class MedicationBaseQuery : IBaseQuery
{
    private readonly IRepository<Product, long> _productRepository;
    private readonly IRepository<GenericDrug, long> _genericDrugRepository; 
    private readonly IRepository<ProductCategory, long> _productCategoryRepository; 
    private readonly IRepository<ProductCategoryMapping, long> _productCategoryMappingRepository;
    private readonly IRepository<AllInputs.Medication, long> _medicationRepository;

    public MedicationBaseQuery(IRepository<Product, long> productRepository, IRepository<GenericDrug, long> genericDrugRepository, IRepository<ProductCategory, long> productCategoryRepository, IRepository<ProductCategoryMapping, long> productCategoryMappingRepository, IRepository<AllInputs.Medication, long> medicationRepository)
    {
        _productRepository = productRepository;
        _genericDrugRepository = genericDrugRepository;
        _productCategoryRepository = productCategoryRepository;
        _productCategoryMappingRepository = productCategoryMappingRepository;
        _medicationRepository = medicationRepository;
    }
    
    public IQueryable<SearchMedicationForReturnDto> SearchProductBaseQuery(string searchTerm)
    {
        var query = (from genericDrug in _genericDrugRepository.GetAll()
            join product in _productRepository.GetAll() on genericDrug.Id equals product.GenericsSctId
            join categoryMapping in _productCategoryMappingRepository.GetAll() on product.Id equals categoryMapping.ProductId
            join category in _productCategoryRepository.GetAll() on categoryMapping.CategoryId equals category.Id
            
            where product.ProductName.ToLower().Contains(searchTerm)
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
                Source = "Product"
            }); 

        return query;
    }

    public IQueryable<SearchMedicationForReturnDto> SearchActiveIngredientBaseQuery(string searchTerm)
    {
        var query = (from genericDrug in _genericDrugRepository.GetAll()
            join product in _productRepository.GetAll() on genericDrug.Id equals product.GenericsSctId
            join categoryMapping in _productCategoryMappingRepository.GetAll() on product.Id equals categoryMapping.ProductId
            join category in _productCategoryRepository.GetAll() on categoryMapping.CategoryId equals category.Id
            
            where product.ActiveIngredients.Contains(searchTerm) // || genericDrug.ActiveIngredients.Contains(searchTerm)
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
                Source = "ActiveIngrediens"
            });

        return query;
    }

    public IQueryable<SearchMedicationForReturnDto> SearchCategoryBaseQuery(string searchTerm)
    {
        var query = (from genericDrug in _genericDrugRepository.GetAll()
            join product in _productRepository.GetAll() on genericDrug.Id equals product.GenericsSctId
            join categoryMapping in _productCategoryMappingRepository.GetAll() on product.Id equals categoryMapping.ProductId
            join category in _productCategoryRepository.GetAll() on categoryMapping.CategoryId equals category.Id
            
            where category.CategoryName.Contains(searchTerm)
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
                Source = "Category"
            });

        return query;
    }

    public IQueryable<SearchMedicationForReturnDto> SearchDoseFormBaseQuery(string searchTerm)
    {
        var query = (from genericDrug in _genericDrugRepository.GetAll()
            join product in _productRepository.GetAll() on genericDrug.Id equals product.GenericsSctId
            join categoryMapping in _productCategoryMappingRepository.GetAll() on product.Id equals categoryMapping.ProductId
            join category in _productCategoryRepository.GetAll() on categoryMapping.CategoryId equals category.Id
            
            where product.DoseFormName.Contains(searchTerm) || genericDrug.DoseForm.Contains(searchTerm)
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
                Source = "DoseForm"
            });

        return query;
    }

    public IQueryable<AllInputs.Medication> GetPatientMedications(int patientId, long? procedureId = null)
    {
        var query = _medicationRepository.GetAll()
            .IgnoreQueryFilters()
            .Where(hcv => hcv.PatientId == patientId)
            .WhereIf(procedureId.HasValue, v => v.ProcedureId == procedureId.Value);
        
        return query;
    }
}
