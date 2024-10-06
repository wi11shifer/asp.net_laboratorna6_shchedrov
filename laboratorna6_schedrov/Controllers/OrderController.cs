using Microsoft.AspNetCore.Mvc;
using laboratorna6_schedrov.Models;
using System.Collections.Generic;

namespace laboratorna6_schedrov.Controllers
{
    public class OrderController : Controller
    {
        private readonly IEnumerable<Product> _availablePizzas;

        public OrderController(IEnumerable<Product> availablePizzas)
        {
            _availablePizzas = availablePizzas;
        }

        public IActionResult ProductForm(int productQuantity)
        {
            ViewBag.AvailablePizzas = _availablePizzas;
            return View();
        }

        [HttpPost]
        public IActionResult ProductForm(Dictionary<int, int> selectedPizzas, Dictionary<int, int> quantities)
        {
            var products = new List<Product>();

            foreach (var pizza in selectedPizzas)
            {
                if (pizza.Value > 0)
                {
                    var product = _availablePizzas.FirstOrDefault(p => p.Id == pizza.Key);
                    if (product != null)
                    {
                        if (quantities.ContainsKey(pizza.Key))
                        {
                            product.Quantity = quantities[pizza.Key];
                        }
                        products.Add(product);
                    }
                }
            }

            return RedirectToAction("Summary", new { productsIds = string.Join(",", products.Select(p => p.Id)), quantities = string.Join(",", products.Select(p => p.Quantity)) });
        }




        public IActionResult Summary(string productsIds, string quantities)
        {
            if (string.IsNullOrEmpty(productsIds))
            {
                return View(new List<Product>());
            }

            var productIdsList = productsIds.Split(',').Select(int.Parse).ToList();
            var quantitiesList = quantities.Split(',').Select(int.Parse).ToList();

            var products = _availablePizzas.Where(p => productIdsList.Contains(p.Id)).ToList();

            decimal totalPrice = 0;
            for (int i = 0; i < products.Count; i++)
            {
                products[i].Quantity = quantitiesList[i];
                totalPrice += products[i].Price * products[i].Quantity;
            }

            ViewBag.TotalPrice = totalPrice;

            return View(products);
        }




    }
}
