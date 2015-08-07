using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CheeseAndScrewdrivers
{
    public class TestClass
    {
        private ShoppingCart cart;
        public TestClass()
        {
            cart = new ShoppingCart();
        }

        [Fact]
        public void WhenAddCalledQuanitityIncreases()
        {
            cart.addCheese();
            Assert.Equal(1, cart.cheeseQuantity);
        }

        [Fact]
        public void WhenAddCalledTotalIncreases()
        {
            cart.addCheese();
            Assert.Equal(3.50, cart.totalPrice);
        }

        [Fact]
        public void AddCheeseAndScrewdrivers()
        {
            cart.addCheese();
            cart.addScrewdriver();
            Assert.Equal(1, cart.cheeseQuantity);
            Assert.Equal(1, cart.screwdriverQuanitity);
            Assert.Equal(9.50, cart.totalPrice);

        }

        [Fact]
        public void BuyOutOfDateCheese()
        {
            cart.addOutOfDateCheese();
            Assert.Equal(1.75, cart.totalPrice);
        }

        [Fact]
        public void BuyEverything()
        {
            cart.addScrewdriver();
            cart.addCheese();
            cart.addOutOfDateCheese();
            Assert.Equal(2, cart.cheeseQuantity);
            Assert.Equal(11.25, cart.totalPrice);
        }

        [Fact]
        public void BuyBundle()
        {
            cart.addBundle();
            Assert.Equal(10.125, cart.totalPrice);
            Assert.Equal(1, cart.bundleQuantity);
        }
    }

    public class ShoppingCart
    {
        public int screwdriverQuanitity = 0;
        public int bundleQuantity = 0;
        public double totalPrice = 0;
        private double cheesePrice = 3.50;
        private double screwdriverPrice = 6;

        public List<IShoppingCartItem>  items = new List<IShoppingCartItem>();
        public void addCheese()
        {
            totalPrice += cheesePrice;
        }

        public void addScrewdriver()
        {
            totalPrice += screwdriverPrice;
        }

        public void addOutOfDateCheese()
        {
            totalPrice += (cheesePrice/2);
        }

        public void addBundle()
        {
            double discount = 0.1;
            double bundlePrice = cheesePrice + (cheesePrice/2) + screwdriverPrice;
            totalPrice += (1 - discount) * bundlePrice;
            bundleQuantity++;
        }

        public void add(IShoppingCartItem item)
        {
            totalPrice += item.GetPrice();
            items.Add(item);
        }

        public int cheeseQuantity
        {
            get { return items.Count(i => i is Cheese); }
        }
    }

    public class Cheese : IShoppingCartItem
    {
        public double GetPrice()
        {
            return 3.5;
        }
    }

    public interface IShoppingCartItem
    {
        double GetPrice();

        string Name { get; }
    }

}
