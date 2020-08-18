namespace GameWebApi.Sql.Models
{
    using System.Collections.Generic;

    public class TableSets
    {
        public List<TableSet> Elements { get; set; }

        public TableSets()
        {
            Elements = new List<TableSet>();
        }
    }
}
