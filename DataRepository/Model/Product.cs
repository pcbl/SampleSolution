using Dapper.Contrib.Extensions;

namespace DataRepository.Model
{
    [Table("Product")]
    public class Product
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

    }
}