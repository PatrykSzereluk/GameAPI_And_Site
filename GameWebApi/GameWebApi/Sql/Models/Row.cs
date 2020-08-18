namespace GameWebApi.Sql.Models
{
    using System.Collections.Generic;

    public class Row
    {
        public List<object> Elements { get; set; }

        public Row()
        {
            Elements = new List<object>();
        }
    }
}
