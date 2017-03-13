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

    }
}
