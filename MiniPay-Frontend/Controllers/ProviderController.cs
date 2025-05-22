using Microsoft.AspNetCore.Mvc;
using MiniPay_Contracts;
using MiniPay_Frontend.Models;

namespace MiniPay_Frontend.Controllers
{
    public class ProviderController(IPaymentApi paymentApi) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var providerList = await paymentApi.GetProviders();
            return View(providerList);
        }

        public IActionResult Create()
        {
            return View();          
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProviderModel provider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await paymentApi.AddProvider(new Provider
            {
                IsActive = provider.IsActive,
                Name = provider.Name,
                EndpointUrl = provider.EndpointUrl,
                Description = provider.Description,
                CurrenciesAllowed = provider.CurrenciesAllowed
            });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var provider = await paymentApi.GetProvider(id);           
            if (provider != null)
            {
                return View(new ProviderModel
                {
                    Id = id,
                    IsActive = provider.IsActive,
                    Name = provider.Name,
                    EndpointUrl = provider.EndpointUrl,
                    Description = provider.Description,
                    CurrenciesAllowed = provider.CurrenciesAllowed
                });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProviderModel provider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await paymentApi.EditProvider(new Provider
            {
                Id = provider.Id,
                IsActive = provider.IsActive,
                Name = provider.Name,
                EndpointUrl = provider.EndpointUrl,
                Description = provider.Description,
                CurrenciesAllowed = provider.CurrenciesAllowed
            });

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> GetAvailableCurrencies(int providerId)
        {
            var provider = await paymentApi.GetProvider(providerId);

            Dictionary<string, int> currenciesDict = new Dictionary<string, int>();
            foreach (Currency currency in provider?.CurrenciesAllowed ?? [])
            {
                currenciesDict.Add(currency.ToString(), (int)currency);
            }

            return Json(new
            {
                providerId,
                provider?.Name,
                currenciesDict
            });
        }
    }
}
