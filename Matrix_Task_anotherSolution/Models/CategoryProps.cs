using System.ComponentModel.DataAnnotations.Schema;

namespace Matrix_Task_anotherSolution.Models
{
    public class CategoryProps
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public CategoryProps()
        {
            ProductCategoryProps = new HashSet<ProductCategoryProp>();
        }
        public virtual ICollection<ProductCategoryProp> ProductCategoryProps { get; set; }
    }
}
