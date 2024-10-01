using Azure;
using Azure.Data.Tables;

namespace ABC_Retail.Models
{
    public class ProductEntity : ITableEntity
    {

        public string PartitionKey { get; set; } = "Customer";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int ProductID { get; set; }
        public int Stock { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

    }
}
