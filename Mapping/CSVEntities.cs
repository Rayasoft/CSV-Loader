using CoreCSVImport.Lib.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCSVImport.Models
{
    public enum CSVCategoryEnum {
        Conference = 1,
        Conference_Session = 2
    }
    public interface ICVSEntities
    {
        void SetObjectsToLoad(int category);
    }
    public class CSVEntities : ICVSEntities
    {
        public Dictionary<int, Type> objectsToLoad = new Dictionary<int, Type>();
        public void SetObjectsToLoad(int category)
        {
            switch (category)
            {
                case (int)CSVCategoryEnum.Conference:
                    objectsToLoad.Add(1, typeof(Conference));
                    break;
                case (int)CSVCategoryEnum.Conference_Session:
                    objectsToLoad.Add(1, typeof(Conference));
                    objectsToLoad.Add(2, typeof(Session));
                    break;
            }
        }
        public Object getit()
        {
            return new Mapper<Conference>();
        }
    }
}
