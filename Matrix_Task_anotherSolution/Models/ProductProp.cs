using System.ComponentModel.DataAnnotations.Schema;

namespace Matrix_Task_anotherSolution.Models
{
    public class ProductProp
    {
        public int Id { get; set; }
        public string Value { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ProductProp()
        {
            ProductCategoryProps = new HashSet<ProductCategoryProp>();
        }
        public ICollection<ProductCategoryProp> ProductCategoryProps { get; set; }
    }
}
