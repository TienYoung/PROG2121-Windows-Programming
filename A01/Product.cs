using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A01
{
    internal abstract class Product
    {
        public uint SKU { get; set; } = 0;
        public string Brand { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public uint Size { get; set; } = 0;
        public DateTime DateStocked { get; set; } = DateTime.MinValue;
        public TimeSpan ShelfLife { get; set; } = TimeSpan.Zero;
        public float BaseRetailPrice { get; set; } = 0;
        public abstract float Discount { get; }
        public float DiscountedPrice { get => BaseRetailPrice * (1.0f - Discount); }

        public string GetProductInformation()
        {
            return string.Empty;
        }
        public Product()
        { 
        }

        public Product(uint sku, string brand, string name, uint size, DateTime date, TimeSpan life, float price)
        {
            SKU = sku;
            Brand = brand;
            Name = name;
            Size = size;
            DateStocked = date;
            ShelfLife = life;
            BaseRetailPrice = price;
        }
    }

    internal class Dairy : Product
    {
        public bool LactoseFree { get; set; }
        public override float Discount => (ShelfLife - (DateTime.Now - DateStocked)).Days <= 5 ? 0.5f : 0.0f;

        public Dairy(bool lactoseFree)
        {
            LactoseFree = lactoseFree;
        }
    }

    internal class Produce : Product
    {
        public enum Category { Fruit, Vegetable }
        public override float Discount 
        {
            get
            {
                int daysUntilExpiration = (ShelfLife - (DateTime.Now - DateStocked)).Days;
                if (daysUntilExpiration <= 10)
                    if (daysUntilExpiration <= 5)
                        return 0.5f;
                    else
                        return 0.2f;
                else
                    return 0.0f;
            }
        }

        public Category ProductCategory { get; set; }
        
        public Produce(Category category)
        { 
            ProductCategory = category;
        }
            
    }

    internal class Cereal : Product
    {
        public float Suger { get; set; }

        public override float Discount => (ShelfLife - (DateTime.Now - DateStocked)).Days < 0 ? 0.5f : 0.0f;

        public Cereal(float suger)
        {
            Suger = suger;
        }
    }
}
