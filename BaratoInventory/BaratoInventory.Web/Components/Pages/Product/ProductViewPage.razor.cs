using Blazored.Toast.Services;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BaratoInventory.Web.Components.Pages.Product
{
    public partial class ProductViewPage
    {
        [Parameter]
        public Guid Id { get; set; }

        public ProductModel ProductModel { get; set; } = new ProductModel();

        [Inject]
        private ApiClient ApiClient { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // Fetch the product from the API
            var res = await ApiClient.GetFromJsonAsync<ResponseModel>($"/api/Product/{Id}");
            if (res != null && res.Success)
            {
                ProductModel = JsonConvert.DeserializeObject<ProductModel>(res.Data.ToString());
            }
            else
            {
                ToastService.ShowError("Failed to load product");
                NavigationManager.NavigateTo("/product");
            }
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo("/product");
        }
    }
}
