namespace Plateaumed.EHR.Misc.Json.VitalSigns
{
    public class VitalSignsJson
    {
        public static readonly string jsonData = /*lang=json*/ @"[
    {
        ""Sign"": ""Pain"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": true,
        ""MaxLength"": ""10"",
        ""Sites"": null
    },
    {
        ""Sign"": ""Resp rate"",
        ""LeftRight"": false,
        ""Sites"": null,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": true,
        ""MaxLength"": ""3"",
        ""Ranges"": [
            {
                ""Unit"": ""cpm"",
                ""LowerRange"": ""12"",
                ""UpperRange"": ""25""
            }
        ]
    },
    {
        ""Sign"": ""Heart Rate"",
        ""LeftRight"": true,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": true,
        ""MaxLength"": ""3"",
        ""Sites"": [
            { ""Site"": ""Brachial"" },
            {
                ""Site"": ""Radial"",
                ""Default"": true
            },
            { ""Site"": ""Heart"" },
            { ""Site"": ""Axillary"" },
            { ""Site"": ""Carotid"" },
            { ""Site"": ""Femoral"" },
            { ""Site"": ""Popliteal"" },
            { ""Site"": ""Post tibial"" },
            { ""Site"": ""Dorsal pedal"" }
        ],
        ""Ranges"": [
            {
                ""Unit"": ""bpm"",
                ""LowerRange"": ""60"",
                ""UpperRange"": ""100""
            }
        ]
    },
    {
        ""Sign"": ""Height"",
        ""LeftRight"": true,
        ""DecimalPlaces"": ""1"",
        ""IsPreset"": false,
        ""MaxLength"": ""3"",
        ""Sites"": [
            {
                ""Site"": ""Standing"",
                ""Default"": true
            },
            { ""Site"": ""Lying"" }
        ],
        ""Ranges"": [
            {
                ""Unit"": ""cm"",
                ""LowerRange"": """",
                ""UpperRange"": """"
            }
        ]
    },
    {
        ""Sign"": ""Weight"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""2"",
        ""IsPreset"": false,
        ""MaxLength"": ""3"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": ""kg"",
                ""LowerRange"": """",
                ""UpperRange"": """"
            }
        ]
    },
    {
        ""Sign"": ""BP sys"",
        ""LeftRight"": true,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": false,
        ""MaxLength"": ""3"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": ""mmHg"",
                ""LowerRange"": ""90"",
                ""UpperRange"": ""140""
            }
        ]
    },
    {
        ""Sign"": ""BP dias"",
        ""LeftRight"": true,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": false,
        ""MaxLength"": ""3"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": ""mmHg"",
                ""LowerRange"": ""60"",
                ""UpperRange"": ""90""
            }
        ]
    },
    {
        ""Sign"": ""Temperature"",
        ""LeftRight"": true,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": false,
        ""MaxLength"": ""0"",
        ""Sites"": [
            {
                ""Site"": ""Axillary"",
                ""Default"": true
            },
            { ""Site"": ""Oral"" },
            { ""Site"": ""Rectal"" },
            { ""Site"": ""Tympanic"" },
            { ""Site"": ""Skin of forehead"" },
            { ""Site"": ""Vaginal"" }
        ],
        ""Ranges"": [
            {
                ""Unit"": ""⁰C"",
                ""LowerRange"": ""35.0"",
                ""UpperRange"": ""38.0"",
                ""DecimalPlaces"": ""1"",
                ""MaxLength"": ""3""
            },
            {
                ""Unit"": ""⁰F"",
                ""LowerRange"": ""97.0"",
                ""UpperRange"": ""99.0"",
                ""DecimalPlaces"": ""1"",
                ""MaxLength"": ""4""
            }
        ]
    },
    {
        ""Sign"": ""SPO₂"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""1"",
        ""IsPreset"": false,
        ""MaxLength"": ""4"",
        ""Sites"": [
            {
                ""Site"": ""Room air"",
                ""Default"": true
            },
            { ""Site"": ""O2 via nasal cannula"" },
            { ""Site"": ""O2 via face mask"" },
            { ""Site"": ""ETT in situ"" }
        ],
        ""Ranges"": [
            {
                ""Unit"": ""%"",
                ""LowerRange"": ""95"",
                ""UpperRange"": ""N/A""
            }
        ]
    },
    {
        ""Sign"": ""BMI"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""1"",
        ""IsPreset"": false,
        ""MaxLength"": ""3"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": ""kg/m²"",
                ""LowerRange"": """",
                ""UpperRange"": """"
            }
        ]
    },
    {
        ""Sign"": ""GCS"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": false,
        ""MaxLength"": ""0"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": """",
                ""LowerRange"": ""7"",
                ""UpperRange"": ""N/A""
            }
        ]
    },
    {
        ""Sign"": ""Occipitofrontal circumference"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""2"",
        ""IsPreset"": false,
        ""MaxLength"": ""0"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": ""cm"",
                ""LowerRange"": """",
                ""UpperRange"": """"
            }
        ]
    },
    {
        ""Sign"": ""Mid-upper arm circumference"",
        ""LeftRight"": true,
        ""DecimalPlaces"": ""2"",
        ""IsPreset"": false,
        ""MaxLength"": ""0"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": ""cm"",
                ""LowerRange"": """",
                ""UpperRange"": """"
            }
        ]
    },
    {
        ""Sign"": ""BSA"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""2"",
        ""IsPreset"": false,
        ""MaxLength"": ""3"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": ""m²"",
                ""LowerRange"": """",
                ""UpperRange"": """"
            }
        ]
    },
    {
        ""Sign"": ""Fetal Heart Rate"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": false,
        ""MaxLength"": ""3"",
        ""Sites"": [ { ""Site"": ""Linked to Abdominal sites"" } ],
        ""Ranges"": [
            {
                ""Unit"": ""bpm"",
                ""LowerRange"": ""110"",
                ""UpperRange"": ""160""
            }
        ]
    },
    {
        ""Sign"": ""Fundal Height"",
        ""LeftRight"": false,
        ""DecimalPlaces"": ""0"",
        ""IsPreset"": false,
        ""MaxLength"": ""3"",
        ""Sites"": null,
        ""Ranges"": [
            {
                ""Unit"": ""cm"",
                ""LowerRange"": """",
                ""UpperRange"": """"
            }
        ]
    }
]
";
    }
}