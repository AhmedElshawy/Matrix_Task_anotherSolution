namespace Matrix_Task_anotherSolution.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category()
        {
            CategoryProps = new HashSet<CategoryProps>();
            Products = new HashSet<Product>();
        }

        public virtual ICollection<CategoryProps> CategoryProps { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
