namespace Matrix_Task_anotherSolution.Models
{
    public class ProductCategoryProp
    {
        public int ProductPropId { get; set; }
        public ProductProp ProductProp { get; set; }

        public int CategoryPropId { get; set; }
        public CategoryProps CategoryProps { get; set; }
    }
}
