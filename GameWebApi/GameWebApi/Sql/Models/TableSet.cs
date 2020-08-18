namespace GameWebApi.Sql.Models
{
    using System.Collections.Generic;

    public class TableSet
    {
        public List<Row> Rows { get; set; }

        public List<TableHeader> TableHeaders;

        public TableSet()
        {
            Rows = new List<Row>();
        }

        public void SetTableHeader(Dictionary<string, string> header)
        {
            List<TableHeader> tmpHeaders = new List<TableHeader>();

            foreach (var item in header)
            {
                tmpHeaders.Add(
                    new TableHeader()
                    {
                        Name = item.Key,
                        Type = item.Value
                    }
                    );
            }

            TableHeaders = tmpHeaders;
        }

    }
}
