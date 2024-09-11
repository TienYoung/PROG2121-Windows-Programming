using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A01
{
    internal abstract class Product
    {
        public uint? SKU { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public uint? Size { get; set; }
        public DateTime? DateStocked { get; set; }
        public TimeSpan? ShelfLife { get; set; }
        public float? BaseRetailPrice { get; set; }
        public abstract float? Discount { get; }
        public float? DiscountedPrice { get => BaseRetailPrice * (1.0f - Discount); }

        public virtual string GetProductInformation()
        {
            return String.Format(
                "SKU: {0}\n" +
                "Brand: {1}\n" +
                "Name: {2}\n" +
                "Size: {3}\n" +
                "Date stocked: {4:d}\n" +
                "Shelf life: {5}\n" +
                "Base retail price: {6}\n" +
                "Discount: {7}\n" +
                "Discounted Price: {8}\n", 
                SKU, Brand, Name, Size, DateStocked, ShelfLife?.Days,
                BaseRetailPrice, Discount, DiscountedPrice
                );
        }
        public Product() { }

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
        public bool? LactoseFree { get; set; }
        public override float? Discount
        {
            get 
            {
                if (DateStocked == null)
                {
                    return null;
                }
                else 
                {
                    return (ShelfLife - (DateTime.Now - DateStocked))?.Days <= 5 ? 0.5f : 0.0f;
                }
            }
        }

        public Dairy() { }

        public Dairy(uint sku, string brand, string name, uint size, DateTime date, TimeSpan life, float price, bool lactoseFree):
            base(sku, brand, name, size, date, life, price)
        {
            LactoseFree = lactoseFree;
        }

        public override string GetProductInformation()
        {
            return base.GetProductInformation() +
                String.Format("Lactose Free: {0}\n", LactoseFree);
        }
    }

    internal class Produce : Product
    {
        public enum Category { Fruit, Vegetable }
        public override float? Discount 
        {
            get
            {
                if (DateStocked == null)
                {
                    return null;
                }
                else
                {
                    int? daysUntilExpiration = (ShelfLife - (DateTime.Now - DateStocked))?.Days;
                    if (daysUntilExpiration <= 10)
                        if (daysUntilExpiration <= 5)
                            return 0.5f;
                        else
                            return 0.2f;
                    else
                        return 0.0f;
                }
            }
        }

        public Category? ProductCategory { get; set; }
        
        public Produce() { }

        public Produce(uint sku, string brand, string name, uint size, DateTime date, TimeSpan life, float price, Category category) :
            base(sku, brand, name, size, date, life, price)
        { 
            ProductCategory = category;
        }

        public override string GetProductInformation()
        {
            return base.GetProductInformation() +
                String.Format("Product Category: {0}\n", ProductCategory);
        }
    }

    internal class Cereal : Product
    {
        public float? Suger { get; set; }

        public override float? Discount
        {
            get
            {
                if (DateStocked == null)
                {
                    return null;
                }
                else 
                { 
                    return (ShelfLife - (DateTime.Now - DateStocked))?.Days < 0 ? 0.5f : 0.0f;
                }
            }
        }

        public Cereal() { }

        public Cereal(uint sku, string brand, string name, uint size, DateTime date, TimeSpan life, float price, float suger) :
            base(sku, brand, name, size, date, life, price)
        {
            Suger = suger;
        }

        public override string GetProductInformation()
        {
            return base.GetProductInformation() +
                String.Format("Suger: {0}\n", Suger);
        }
    }
}
