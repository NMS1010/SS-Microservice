using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Model.Paging
{
    public class PagingRequest
    {
        private string _keyword;

        public string Keyword
        {
            get => _keyword;
            set => _keyword = value?.ToLower();
        }

        public long PageIndex { get; set; } = 1;
        public long PageSize { get; set; } = 1000;

        public string ColumnName { get; set; }
        public string TypeSort { get; set; } = "ASC";
        public int SortBy { get; set; }
    }
}