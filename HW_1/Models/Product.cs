namespace HW_1.Models
{
    public class Product : BaseModel
    {
        public int CategoriId { get; set; }
        public int Price { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Storage>? Stores { get; set; }

    }
}
