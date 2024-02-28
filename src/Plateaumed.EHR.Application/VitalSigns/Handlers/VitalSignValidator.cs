using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.VitalSigns.Abstraction;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class VitalSignValidator : IVitalSignValidator
{
    private readonly IRepository<MeasurementRange, long> _measurementRangeRepository; 
    
    public VitalSignValidator(IRepository<MeasurementRange, long> measurementRangeRepository)
    {
        _measurementRangeRepository = measurementRangeRepository;
    }
    
    public async Task ValidateRequest(double vitalReading, VitalSign vitalSign, long? measurementRangeId)
    {
        // validate a vital reading is passed
        if (vitalReading <= 0) throw new UserFriendlyException("Vital reading cannot be less than or equal zero.");

        // validate the length of the data
        if (vitalSign.MaxLength > 0)
        {
            // get and validate the measuring range if any
            MeasurementRange measurementRange = null;
            if (measurementRangeId.HasValue)
                measurementRange = await _measurementRangeRepository.GetAsync(measurementRangeId.Value) ??
                                   throw new UserFriendlyException("Measurement Range not found");
            
            // split the vital reading value
            var vitalReadingStr = vitalReading.ToString().Split(".");

            var vitalReadingExceptMssg =
                $"The vital reading for {vitalSign.Sign} is greater than the max length of {vitalSign.MaxLength.ToString()}.";
            
            // validate if the max length was setup in the measurement range table
            if (measurementRange != null && measurementRange.MaxLength != null && vitalReadingStr[0].Length > measurementRange.MaxLength.Value)
                throw new UserFriendlyException(vitalReadingExceptMssg);
            
            if (vitalReadingStr[0].Length > vitalSign.MaxLength)
                throw new UserFriendlyException(vitalReadingExceptMssg);

            // check if there is a decimal value
            if (vitalReadingStr.Length > 1 && vitalSign.DecimalPlaces > 0)
            {
                var vitalReadingDecimalExceptMssg =
                    $"The value after the decimal point for {vitalSign.Sign} is greater than the max length of {vitalSign.DecimalPlaces.ToString()}.";
                
                if (measurementRange != null && measurementRange.DecimalPlaces != null && measurementRange.DecimalPlaces.Value > 0 && vitalReadingStr.Length > 1)
                    throw new UserFriendlyException(vitalReadingDecimalExceptMssg);
                
                // validate if the length of the decimal value is greater than the decimal places
                if (vitalReadingStr[1].Length > vitalSign.DecimalPlaces)
                    throw new UserFriendlyException(vitalReadingDecimalExceptMssg);
            } 
        }
    }
    
}