using Blazored.Toast.Services;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BaratoInventory.Web.Components.Pages.Product
{
    public partial class ProductUpdatePage
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
            var res = await ApiClient.GetFromJsonAsync<ResponseModel>($"/api/Product/{Id}");
            if (res != null && res.Success)
            {
                ProductModel = JsonConvert.DeserializeObject<ProductModel>(res.Data.ToString());
            }
        }

        public async Task Submit()
        {
            ProductModel.LastUpdated = DateTime.UtcNow;
            var res = await ApiClient.PutAsync<ResponseModel, ProductModel>($"/api/Product/{Id}", ProductModel);
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Product Updated successfully");
                NavigationManager.NavigateTo("/product");
            }
        }

    }
}
