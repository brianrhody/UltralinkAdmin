using DPPAtHomeCommon.ViewModels.Dealers;
using DPPAtHomeCommon.ViewModels.User;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltralinkAdmin.Components.Pages.Dealers
{
    public partial class Dealers
    {
        private MudTable<DealerVm> table { get; set; } = new();
        private DealerFilterParametersVm filterParameters { get; set; } = new DealerFilterParametersVm();
        private UserSessionVm? userSessionVm { get; set; }
        private bool shouldRender = true;

        protected override async Task OnInitializedAsync()
        {
            LoaderService.ShowTheLoader(true);
            userSessionVm = await sessionStorage.GetItemAsync<UserSessionVm>("User");
            if (userSessionVm == null)
            {
                NavManager.NavigateTo("/login");
            }
            LoaderService.ShowTheLoader(false);
        }

        private async Task FilterDealers()
        {
            await table.ReloadServerData();
        }

        private async Task ClearFilter()
        {
            try
            {
                filterParameters.Filter = "";
                await table.ReloadServerData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<TableData<DealerVm>> ServerReload(TableState state, CancellationToken token)
        {
            LoaderService.ShowTheLoader(true);
            shouldRender = false;
            List<DealerVm>? _dealers = new List<DealerVm>();
            try
            {
                filterParameters.PageNumber = state.Page;
                filterParameters.PageSize = state.PageSize;

                if (state.SortLabel != null)
                {
                    filterParameters.SortBy = state.SortLabel;
                    filterParameters.SortDirection = state.SortDirection.ToString();
                }

                if (!string.IsNullOrEmpty(filterParameters.Filter))
                {
                    filterParameters.Filter = filterParameters.Filter.TrimEnd();
                }

                var dealerList = await DealerService.GetAllDealers(filterParameters);
                if (dealerList != null && dealerList.Any())
                {
                    _dealers = dealerList.ToList();
                    return new TableData<DealerVm> { Items = _dealers, TotalItems = (int)(_dealers.First().MaxRows ?? 0) };
                }
                return new TableData<DealerVm> { Items = null };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                LoaderService.ShowTheLoader(false);
                shouldRender = true;
                StateHasChanged();
            }

            return new TableData<DealerVm> { Items = null };
        }

        private async Task AddNewDealer()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };

            //var dialog = DialogService.Show<AddEditDealer>("Add Dealer", options);
            //await dialog.Result;

            await table.ReloadServerData();
        }

        private async Task EditDealer(DealerVm dealer)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
            var parameters = new DialogParameters
            {
                { "DealerId", dealer.Id }
            };

            //var dialog = DialogService.Show<AddEditDealer>("Edit Dealer", parameters, options);
            //await dialog.Result;

            await table.ReloadServerData();
        }

        private void DealerUsers(DealerVm dealer)
        {
            NavManager.NavigateTo($"/Dealers/Users/{dealer.Id}");
        }
    }
}
