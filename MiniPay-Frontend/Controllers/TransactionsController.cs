using Microsoft.AspNetCore.Mvc;
using MiniPay_Contracts;
using MiniPay_Frontend.Models;

namespace MiniPay_Frontend.Controllers
{
    public class TransactionsController(IPaymentApi paymentApi) : Controller
    {
        public async Task<IActionResult> Index(int providerId = -1)
        {
            List<Transaction>? transactions;
            if (providerId > -1)
            {
                transactions = await paymentApi.GetTransactionsByProvider(providerId);
            }
            else
            {
                transactions = await paymentApi.GetTransactions();
            }

            var model = new TransactionExtViewModel
            {
                TransactionList = transactions != null ? [.. transactions.OrderByDescending(d => d.Timestamp)] : [],
                ProviderList = await paymentApi.GetProviders(),
                SelectedProvider = providerId
            };

            return View(model);
        }        

        public async Task<IActionResult> Create(int id = -1)
        {
            var model = new TransactionModel();
            var providers = await paymentApi.GetProviders();
            if (providers != null)
            {
                model.AllowedProviders = [.. providers.Where(d => d.IsActive)];

                if (id != -1)
                {
                    model.SelectedProvider = id;
                }
            };
            return View(model);          
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllowedProviders = await paymentApi.GetProviders();
                return View(model);
            }

            var result = await paymentApi.CreateTransaction(new PaymentRequest
            {
                ProviderId = model.SelectedProvider,
                Amount = model.Amount,
                Currency = model.Currency,
                Description = model.Description,
                ReferenceId = $"ORDER-{Guid.NewGuid()}"
            });

            return RedirectToAction(nameof(Index));           
        }

        public async Task<IActionResult> Delete(string id)
        {
            await paymentApi.DeleteTransaction(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
