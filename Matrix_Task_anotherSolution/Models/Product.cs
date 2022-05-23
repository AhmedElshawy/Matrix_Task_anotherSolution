using System.ComponentModel.DataAnnotations.Schema;

namespace Matrix_Task_anotherSolution.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product()
        {
            Props = new HashSet<ProductProp>();
        }

        public virtual ICollection<ProductProp> Props { get; set; }
    }
}
