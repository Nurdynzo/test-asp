using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Json;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class DefaultMedicationProductsCreator
{
     private readonly EHRDbContext _context;
    
    public DefaultMedicationProductsCreator(EHRDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        if(!_context.Products.ToList().Any())
        {
            var products = getProducts();
            foreach (var item in products)
                _context.Products.Add(item);
                
            _context.SaveChanges();
        }  
        
        if(!_context.GenericDrugs.ToList().Any())
        {
            var genericDrugs = getGenericDrugs();
            foreach (var item in genericDrugs)
                _context.GenericDrugs.Add(item);
                
            _context.SaveChanges();
        } 
        
        if(!_context.ProductCategories.ToList().Any())
        {
            var productCategories = getProductCategories();
            foreach (var item in productCategories)
                _context.ProductCategories.Add(item);
                
            _context.SaveChanges();
        } 
        
        if(!_context.ProductCategoryMappings.ToList().Any())
        {
            var productCategoryMappingList = getProductCategoryMappingDto();
            foreach (var item in productCategoryMappingList)
            {
                var product  = _context.Products.FirstOrDefault(v => v.ProductId == item.TempProductId);
                if (product != null)
                {
                    var productCategoryMapping = new ProductCategoryMapping();
                    productCategoryMapping.ProductId = product.Id;
                    productCategoryMapping.CategoryId = Convert.ToInt64(item.CategoryId);
                    _context.ProductCategoryMappings.Add(productCategoryMapping);
                }
            }
                
            _context.SaveChanges();
        } 
    }
 
    
    private List<Product> getProducts()
    { 
        var data = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(ProductJson.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    } 
    
    private List<GenericDrug> getGenericDrugs()
    { 
        var data = System.Text.Json.JsonSerializer.Deserialize<List<GenericDrug>>(GenericDrugJson.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    } 
    
    private List<ProductCategory> getProductCategories()
    { 
        var data = System.Text.Json.JsonSerializer.Deserialize<List<ProductCategory>>(ProductCategoriesJson.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    } 
    
    private List<ProductCategoryMappingDto> getProductCategoryMappingDto()
    { 
        var data = System.Text.Json.JsonSerializer.Deserialize<List<ProductCategoryMappingDto>>(ProductCategoryMappingJson.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    } 
}

public class ProductCategoryMappingDto
{
    public long TempProductId { get; set; }
    public string CategoryId { get; set; }
}