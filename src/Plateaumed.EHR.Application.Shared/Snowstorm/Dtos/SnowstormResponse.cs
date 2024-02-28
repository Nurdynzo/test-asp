using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Plateaumed.EHR.Snowstorm.Dtos;

public class SnowstormResponse
{
    [JsonProperty("buckets")]
    public Buckets Buckets { get; set; }

    [JsonProperty("languageNames")] 
    [CanBeNull] 
    public LanguageNames LanguageNames { get; set; } 
    
    // [JsonProperty("bucketConcepts")]
    // public Dictionary<string, Concept> BucketConcepts { get; set; }

    [JsonProperty("totalPages")]
    public long TotalPages { get; set; }

    [JsonProperty("totalElements")]
    public long TotalElements { get; set; }

    [JsonProperty("last")]
    public bool Last { get; set; }

    [JsonProperty("number")]
    public long Number { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("numberOfElements")]
    public long NumberOfElements { get; set; }

    [JsonProperty("first")]
    public bool First { get; set; }

    [JsonProperty("items")]
    public List<SnowItem> Items { get; set; }
}

public partial class Buckets
{
    [JsonProperty("module")]
    public Dictionary<string, long> Module { get; set; }

    [JsonProperty("semanticTags")] 
    [CanBeNull] 
    public SemanticTags SemanticTags { get; set; }

    [JsonProperty("language")] 
    [CanBeNull] 
    public Language Language { get; set; }

    [JsonProperty("membership")]
    public Dictionary<string, long> Membership { get; set; }
}

public partial class Language
{
    [JsonProperty("en")]
    public long En { get; set; }
}

public partial class SemanticTags
{
    [JsonProperty("disorder")]
    public long Disorder { get; set; }

    [JsonProperty("finding")]
    public long Finding { get; set; }
}

public partial class LanguageNames
{
    [JsonProperty("en")]
    public string En { get; set; }
}

public partial class SnowItem
{
    [JsonProperty("term")]
    public string Term { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }

    [JsonProperty("languageCode")]
    public string LanguageCode { get; set; }

    [JsonProperty("module")]
    public string Module { get; set; }

    [JsonProperty("concept")]
    public Concept Concept { get; set; }
}

public partial class Concept
{
    [JsonProperty("conceptId")]
    public string ConceptId { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }

    [JsonProperty("definitionStatus")]
    public string DefinitionStatus { get; set; }

    [JsonProperty("moduleId")]
    public string ModuleId { get; set; }

    [JsonProperty("fsn")] 
    [CanBeNull] 
    public Fsn Fsn { get; set; }

    [JsonProperty("pt")] 
    [CanBeNull] 
    public Fsn Pt { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}

public partial class Fsn
{
    [JsonProperty("term")]
    public string Term { get; set; }

    [JsonProperty("lang")]
    public string Lang { get; set; }
}