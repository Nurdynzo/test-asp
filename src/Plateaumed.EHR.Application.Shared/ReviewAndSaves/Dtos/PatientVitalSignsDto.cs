using Plateaumed.EHR.VitalSigns.Dto;
using System;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class PatientVitalSignsDto
    {
        public long Id { get; set; }
        public GetSimpleVitalSignsResponse VitalSign { get; set; }
        public MeasurementSiteDto MeasurementSite { get; set; }
        public MeasurementRangeDto MeasurementRange { get; set; }
        public string PatientVitalType { get; set; }
        public string Position { get; set; }
        public double VitalReading { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
