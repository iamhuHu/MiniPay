using Microsoft.AspNetCore.Mvc.Rendering;
using MiniPay_Contracts;

namespace MiniPay_Frontend.Models
{
    public class TransactionExtViewModel
    {
        public List<Transaction>? TransactionList { get; set; }
        public List<Provider>? ProviderList { get; set; }
        public int SelectedProvider {  get; set; }

        public SelectList ProviderListSelection
        {
            get
            {                
                ProviderList?.Insert(0, new Provider
                {
                    Id = -1,
                    Name = "all providers"
                });
                return new SelectList(ProviderList, "Id", "Name", SelectedProvider);                
            }
        }

        public string? ProviderNameSelected
        {
            get
            {
                return ProviderList?.FirstOrDefault(d => d.Id == SelectedProvider)?.Name;
            }
        }
    }
}
