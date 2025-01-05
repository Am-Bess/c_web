namespace AppProduct.Models
{
    public class Category : BModel
    {
        public virtual List<Product>? Products { get; set; }
    }
}
