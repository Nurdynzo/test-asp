namespace Plateaumed.EHR.Misc.Json.VitalSigns
{
    public class ApgarScoringJson
    {
        public static readonly string jsonData = /*lang=json*/ @"[
        {
            ""Name"": ""Appearance"",
            ""Ranges"": [
                {
                    ""Score"": 0,
                    ""Result"": ""Blue, pale""
                },
                {
                    ""Score"": 1,
                    ""Result"": ""Pink body, blue extremities""
                },
                {
                    ""Score"": 2,
                    ""Result"": ""Pink""
                }
            ]
        },
        {
            ""Name"": ""Pulse"",
            ""Ranges"": [
                {
                    ""Score"": 0,
                    ""Result"": ""Absent""
                },
                {
                    ""Score"": 1,
                    ""Result"": ""Below 100 bpm""
                },
                {
                    ""Score"": 2,
                    ""Result"": ""Over 100 bpm""
                }
            ]
        },
        {
            ""Name"": ""Grimace"",
            ""Ranges"": [
                {
                    ""Score"": 0,
                    ""Result"": ""Floppy""
                },
                {
                    ""Score"": 1,
                    ""Result"": ""Minimal response to stimulation""
                },
                {
                    ""Score"": 2,
                    ""Result"": ""Prompt response to stimulation""
                }
            ]
        },
        {
            ""Name"": ""Activity"",
            ""Ranges"": [
                {
                    ""Score"": 0,
                    ""Result"": ""Absent""
                },
                {
                    ""Score"": 1,
                    ""Result"": ""Flexed arms & legs""
                },
                {
                    ""Score"": 2,
                    ""Result"": ""Active""
                }
            ]
        },
        {
            ""Name"": ""Respiratory effort"",
            ""Ranges"": [
                {
                    ""Score"": 0,
                    ""Result"": ""Absent""
                },
                {
                    ""Score"": 1,
                    ""Result"": ""Slow and irregular""
                },
                {
                    ""Score"": 2,
                    ""Result"": ""Vigorous cry""
                }
            ]
        }]";
    }
}