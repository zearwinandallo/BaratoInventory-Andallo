using Blazored.Toast.Services;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Components;

namespace BaratoInventory.Web.Components.Pages.Product
{
    public partial class ProductCreatePage
    {
        public ProductModel ProductModel { get; set; } = new ProductModel();

        [Inject]
        private ApiClient ApiClient { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public async Task Submit()
        {
            var res = await ApiClient.PostAsync<ResponseModel, ProductModel>("/api/Product", ProductModel);
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Product Created successfully");
                NavigationManager.NavigateTo("/product");
            }
        }
    }
}
