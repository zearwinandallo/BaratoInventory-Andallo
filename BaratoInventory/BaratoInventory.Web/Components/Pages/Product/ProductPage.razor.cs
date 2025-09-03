using BaratoInventory.Web.Components.Pages.BaseComponents;
using Blazored.Toast.Services;
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

        public AppModal Modal { get; set; }
        public Guid DeleteID { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadProduct();
        }

        protected async Task HandleDelete()
        {
            var res = await ApiClient.DeleteAsync<ResponseModel>($"/api/Product/{DeleteID}");
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Delete product successfully");
                await LoadProduct();
                Modal.Close();
            }
        }

        protected async Task LoadProduct()
        {
            var res = await ApiClient.GetFromJsonAsync<ResponseModel>("/api/Product");
            if (res != null && res.Success)
            {
                ProductModels = JsonConvert.DeserializeObject<List<ProductModel>>(res.Data.ToString());
            }
        }
    }
}
