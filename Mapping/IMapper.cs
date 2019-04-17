using System.Collections.Generic;
using System.Data;

namespace CoreCSVImport.Lib.Mapping
{
    public interface IMapper<T>
    {
        List<T> Map(DataTable table);
    }
}
