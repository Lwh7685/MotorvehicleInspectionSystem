using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Tools
{
    /// <summary>
    /// 查询机动车列表时，按照流水号合并两个datatable
    /// </summary>
    public class CustomComparerR002 : IEqualityComparer<DataRow>
    {
        #region IEqualityComparer<DataRow> Members

        public bool Equals(DataRow x, DataRow y)
        {
            return ((string)x["Lsh"]).Equals((string)y["Lsh"]);
        }

        public int GetHashCode(DataRow obj)
        {
            return ((string)obj["Lsh"]).GetHashCode();
        }

        #endregion
    }
}
