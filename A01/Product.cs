/*
 * FILE          : Product.cs
 * PROJECT       : RPOG2121-A01
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-09-09
 * DESCRIPTION   : 
 *   This file defines the abstract Product class and its derived classes: Dairy, Produce,
 *   and Cereal. It implements a product hierarchy for a retail management system, including
 *   common properties like SKU, brand, and price, as well as specific attributes and
 *   behaviors for each product type. The class structure demonstrates inheritance,
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A01
{
    /*
    * NAME : Product
    * PURPOSE : The Product class serves as the base class for all product types in the retail
    * management system. It defines common properties such as SKU, brand, name, size,
    * date stocked, shelf life, and price. The class also implements core functionality
    * like discount calculation and product information retrieval. As an abstract class,
    * Product provides a template for derived classes (Dairy, Produce, Cereal) to inherit
    * from and specialize, ensuring a consistent interface across all product types while
    * allowing for type-specific behaviors.
    */
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

        //
        // METHOD : GetProductInformation
        // DESCRIPTION :
        //   This method returns a formatted string containing all the
        //   relevant information about a product, including its SKU,
        //   brand, name, size, date stocked, shelf life, base retail price,
        //   discount, and discounted price.
        // PARAMETERS :
        //   None
        // RETURNS :
        //   string : A formatted string containing all product information
        //
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

        public void GenericMethod()
        {
            Console.WriteLine(this + ": This is a inherited method from the parent");
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

    /*
    * NAME : Dairy
    * PURPOSE : The Dairy class is a specialized product type that inherits from the Product class.
    * It represents dairy products in the retail management system. In addition to the
    * base product properties, it includes a LactoseFree property to indicate whether
    * the product is lactose-free. The Dairy class implements a unique discount calculation
    * based on the product's shelf life and current date. It also provides a specialized
    * method for retrieving product information, including the lactose-free status.
    */
    internal class Dairy : Product
    {
        public bool? LactoseFree { get; set; }
        //
        // PROPERTY : Discount
        // DESCRIPTION :
        //   This property calculates the discount for a dairy product
        //   based on its remaining shelf life. If the product has 5 or
        //   fewer days left before expiration, it returns a 50% discount.
        //   Otherwise, it returns no discount (0%).
        // PARAMETERS :
        //   None
        // RETURNS :
        //   float? : The calculated discount as a percentage (0.5 for 50%, 0.0 for 0%),
        //            or null if the DateStocked is not set
        //
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

        //
        // FUNCTION : UniqueMethod
        // DESCRIPTION :
        //   This function demonstrates a method that is unique to the Dairy class.
        //   It prints a message indicating that it is a unique method for this class.
        // PARAMETERS :
        //   None
        // RETURNS :
        //   void
        //
        public void UniqueMethod()
        {
            Console.WriteLine(this + ": This is a unique method");
        }
    }

    /*
    * NAME : Produce
    * PURPOSE : The Produce class is a specialized product type that inherits from the Product class.
    * It represents fresh produce items in the retail management system. The class includes
    * additional properties such as SaleUnit (package or weight) and ProductCategory (fruit
    * or vegetable). The Produce class implements a unique discount calculation based on
    * the remaining shelf life of the product. It also provides a specialized method for
    * retrieving product information, including the sale unit and product category.
    */
    internal class Produce : Product
    {
        public enum SaleUnit { PACKAGE, WEIGHT }
        public enum Category { FRUIT, VEGETABLE }
        //
        // PROPERTY : Discount
        // DESCRIPTION :
        //   This property calculates the discount for a produce item
        //   based on its remaining shelf life. If the product has 5 or
        //   fewer days left, it returns a 50% discount. If it has between
        //   6 and 10 days left, it returns a 20% discount. Otherwise,
        //   it returns no discount (0%).
        // PARAMETERS :
        //   None
        // RETURNS :
        //   float? : The calculated discount as a percentage (0.5 for 50%, 0.2 for 20%, 0.0 for 0%),
        //            or null if the DateStocked is not set
        //
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

        public SaleUnit? SoldBy { get; set; }
        
        public Produce() { }

        public Produce(uint sku, string brand, string name, uint size, DateTime date, TimeSpan life, float price, SaleUnit soldBy, Category category) :
            base(sku, brand, name, size, date, life, price)
        { 
            ProductCategory = category;
            SoldBy = soldBy;
        }

        public override string GetProductInformation()
        {
            return base.GetProductInformation() +
                String.Format("Sold by: {0}\n", SoldBy) + 
                String.Format("Product Category: {0}\n", ProductCategory);
        }
    }

    /*
    * NAME : Cereal
    * PURPOSE : The Cereal class is a specialized product type that inherits from the Product class.
    * It represents cereal products in the retail management system. In addition to the
    * base product properties, it includes a Sugar property to indicate the sugar content
    * of the cereal. The Cereal class implements a unique discount calculation based on
    * whether the product has expired. It also provides a specialized method for retrieving
    * product information, including the sugar content of the cereal.
    */
    internal class Cereal : Product
    {
        public float? Sugar { get; set; }
        //
        // PROPERTY : Discount
        // DESCRIPTION :
        //   This property calculates the discount for a cereal product
        //   based on whether it has expired. If the product has passed
        //   its shelf life, it returns a 50% discount. Otherwise, it
        //   returns no discount (0%).
        // PARAMETERS :
        //   None
        // RETURNS :
        //   float? : The calculated discount as a percentage (0.5 for 50%, 0.0 for 0%),
        //            or null if the DateStocked is not set
        //
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
            Sugar = suger;
        }

        public override string GetProductInformation()
        {
            return base.GetProductInformation() +
                String.Format("Suger: {0}\n", Sugar);
        }
    }
}
