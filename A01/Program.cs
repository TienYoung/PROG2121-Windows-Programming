﻿/*
 * FILE          : Program.cs
 * PROJECT       : RPOG2121-A01
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-09-09
 * DESCRIPTION   : 
 *   This file contains the main entry point for the A01 project. It demonstrates the usage
 *   of various Product subclasses (Dairy, Produce, and Cereal) by creating instances,
 *   setting their properties, and calling their methods. The program showcases inheritance,
 *   polymorphism, and the specific behaviors of each product type.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A01
{
    /*
    * NAME : Program
    * PURPOSE : The Program class serves as the entry point for the A01 project. It demonstrates
    * the usage and functionality of various Product subclasses (Dairy, Produce, and
    * Cereal). The class instantiates objects of each subclass, sets their properties,
    * and invokes their methods to showcase inheritance, polymorphism, and the specific
    * behaviors of each product type. This class is crucial for testing and demonstrating
    * the capabilities of the product hierarchy implemented in the project.
    */
    internal class Program
    {
        static void Main(string[] args)
        {

            // Instantiate each of the three subclasses
            Dairy milk = new Dairy();
            Console.WriteLine(milk.GetProductInformation());
            Produce peach = new Produce();
            Console.WriteLine(peach.GetProductInformation());
            Cereal rice = new Cereal();
            Console.WriteLine(rice.GetProductInformation());

            // Demonstrate accessing the inherited properties (both write and read)
            milk.SKU = 1;
            milk.Brand = "AAA";
            milk.Name = "Milk";
            milk.Size = 250;
            milk.DateStocked = new DateTime(2024, 9, 10);
            milk.ShelfLife = new TimeSpan(days: 15, 0, 0, 0);
            milk.BaseRetailPrice = 3.0f;
            milk.LactoseFree = false;
            Console.WriteLine(milk.GetProductInformation());

            milk.DateStocked = new DateTime(2024, 1, 1);
            Console.WriteLine("Milk date stocked: " + milk.DateStocked);
            Console.WriteLine("Milk discount: " + milk.Discount);
            Console.WriteLine("Milk discounted price: " + milk.DiscountedPrice);
            Console.WriteLine();

            peach.SKU = 38;
            peach.Brand = "BBB";
            peach.Name = "Peach";
            peach.Size = 2500;
            peach.DateStocked = new DateTime(2024, 9, 9);
            peach.ShelfLife = new TimeSpan(days: 3, 0, 0, 0);
            peach.SoldBy = Produce.SaleUnit.PACKAGE;
            peach.BaseRetailPrice = 10.0f;
            peach.ProductCategory = Produce.Category.FRUIT;
            Console.WriteLine(peach.GetProductInformation());

            rice.SKU = 150;
            rice.Brand = "CCC";
            rice.Name = "Rice";
            rice.Size = 7000;
            rice.DateStocked = new DateTime(2023, 10, 25);
            rice.ShelfLife = new TimeSpan(days: 365, 0, 0, 0);
            rice.BaseRetailPrice = 2.4f;
            rice.Sugar = .3f;
            Console.WriteLine(rice.GetProductInformation());

            // Demonstrate accessing the unique property(s) of the subclasses
            rice.Sugar = 0.001f;
            Console.WriteLine("Rice sugar: " + rice.Sugar);

            // Demonstrate the method(s) inherited from the parent
            milk.GenericMethod();
            peach.GenericMethod();
            rice.GenericMethod();

            // Demonstrate the overridden methods
            Console.WriteLine(rice.GetProductInformation());

            // Demonstrate the overloaded methods
            Cereal wheat = new Cereal();
            wheat = new Cereal(151, "CCC", "Wheat", 10000, new DateTime(2024, 8, 26), new TimeSpan(days: 300, 0, 0, 0), 5.0f, 0.1f);

            // Demonstrate the unique methods
            milk.UniqueMethod();

            Console.ReadKey();
        }
    }
}
