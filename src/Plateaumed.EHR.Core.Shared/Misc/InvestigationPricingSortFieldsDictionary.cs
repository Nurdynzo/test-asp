using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Misc
{
    public class InvestigationPricingSortFieldsDictionary
	{
        public static string GetInvestigationSortFieldValue(string key)
        {
            var values = new Dictionary<string, string>()
            {
                {"TestNameAsc","Test name: A-Z"},
                {"TestNameDesc","Test name: Z-A"},
                {"PriceHighest","Price: Highest"},
                {"PriceLowest","Price: Lowest"},
                {"DateMostRecent","Date: Most Recent"}
            };
            return values.FirstOrDefault(x => x.Key.Equals(key)).Value;
        }
    }
}

