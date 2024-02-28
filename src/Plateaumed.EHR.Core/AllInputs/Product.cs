using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.AllInputs;

public class Product : FullAuditedEntity<long>
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string NigeriaRegNo { get; set; }
    public long BrandId { get; set; }
    public string BrandName { get; set; }
    public string Manufacturer { get; set; }
    public long? GenericsSctId { get; set; }
    public string SnowmedId { get; set; }
    public long? DoseFormId { get; set; }
    public string DoseFormName { get; set; }
    public string ActiveIngredients { get; set; } 
    public long? DoseStrengthId { get; set; }
    public string DoseStrengthName { get; set; } 
    public string CountryOfManufacture { get; set; }
    public string Synonyms { get; set; }
    public string Synonyms2 { get; set; }
    public string Synonyms3 { get; set; }
    public string Notes { get; set; }
    public string SnowmedCategoryId1 { get; set; }
    public string SnowmedCategoryName1 { get; set; }
    public string SnowmedCategoryId2 { get; set; }
    public string SnowmedCategoryName2 { get; set; }
    public string SnowmedCategoryId3 { get; set; }
    public string SnowmedCategoryName3 { get; set; }
    public string SnowmedCategoryId4 { get; set; }
    public string SnowmedCategoryName4 { get; set; }
    public string SnowmedCategoryId5 { get; set; }
    public string SnowmedCategoryName5 { get; set; } 
}