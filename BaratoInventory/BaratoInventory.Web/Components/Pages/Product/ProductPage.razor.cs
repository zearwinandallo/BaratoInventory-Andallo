using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BaratoInventory.Web.Components.Pages.Product
{
    public partial class ProductPage
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        public List<ProductModel> ProductModels { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var res = await ApiClient.GetFromJsonAsync<ResponseModel>("/api/Product");
            if (res != null && res.Success)
            {
                ProductModels = JsonConvert.DeserializeObject<List<ProductModel>>(res.Data.ToString());
            }
            await base.OnInitializedAsync();
        }
    }
}
