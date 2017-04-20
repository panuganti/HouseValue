using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseValueLibrary
{
    public class Utils
    {
        public static IEnumerable<string> HousingFeatures(HousingData data)
        {
            var features = new List<string>();
            features.Add(data.id.ToString());
            features.Add(data.pincode);
            features.Add(data.floor_count > 2 ? "0" : "1");
            #region floorCategory
            string floorCategory = "";
            if (data.floor_count > 2)
            {
                if (data.floor_number == 0)
                {
                    floorCategory = "0";
                }
                else if (data.floor_number == data.floor_count)
                {
                    floorCategory = "1";
                }
                else
                {
                    floorCategory = "2";
                }
            }
            else
            {
                floorCategory = "3";
            }
            #endregion floorCategory
            features.Add(floorCategory);
            features.Add(data.built_up_area.ToString());
            features.Add(data.bedroom_count.ToString());
            features.Add(data.bathroom_count.ToString());
            features.Add(data.under_construction ? "0" : "1");
            features.Add(data.age_of_property.ToString());
            features.Add(data.price_per_sqft.ToString());
            return features;
        }

        
        public static IEnumerable<House> GetComparables(House property,  IEnumerable<House> data)
        {
            int comparables_count = 6;
            var comparables = data.Where(h => String.Equals(h.city_name, property.city_name, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            House[] new_comparables = comparables;
            if (new_comparables.Count() > comparables_count)
            {
                comparables = new_comparables.ToArray();
                new_comparables = comparables.Where(h => Math.Abs(h.year - property.year) < 10).ToArray();
                if (new_comparables.Length > comparables_count)
                {
                    comparables = new_comparables;
                    new_comparables = comparables.Where(h => h.built_up_area*2 > property.built_up_area/2 && property.built_up_area*2 > h.built_up_area).ToArray();
                    if (new_comparables.Length > comparables_count)
                    {
                        comparables = new_comparables;
                        new_comparables = comparables.Where(h => (DateTime.Now - new DateTime(h.date_priced)).TotalDays < 10*365).ToArray();
                        if (new_comparables.Length > comparables_count)
                        {
                            comparables = new_comparables;
                            new_comparables = comparables.Where(h => String.Equals(h.locality, property.locality, StringComparison.InvariantCultureIgnoreCase)).ToArray();
                            if (new_comparables.Length > comparables_count)
                            {
                                comparables = new_comparables;
                            }
                        }
                    }
                }
            }
            return comparables.OrderBy(h => Math.Abs(property.built_up_area - h.built_up_area)).Take(6);
        }
        

    }
}
