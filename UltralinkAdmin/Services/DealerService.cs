
using DPPAtHomeCommon.ViewModels.Auth0;
using DPPAtHomeCommon.ViewModels.Dealers;
using DPPAtHomeCommon.ViewModels.Device;
using DPPAtHomeCommon.ViewModels.Reporting;
using DPPAtHomeCommon.ViewModels.User;
using DPPAtHomeCommon.ViewModels.Voucher;
using System.Net.Http.Json;
using System.Text;
using static MudBlazor.CategoryTypes;
using static MudBlazor.Icons;
using static System.Net.WebRequestMethods;

namespace UltralinkAdmin.Services
{
    public class DealerService : IDealerService
    {
        private readonly HttpClient _httpClient;

        public DealerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DealerVm>> GetAllDealers(DealerFilterParametersVm dealerFilterParameters)
        {
            List<DealerVm> dealers = new List<DealerVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/GetAllDealersPaged");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(dealerFilterParameters), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                var dealerList = await response.Content.ReadFromJsonAsync<List<DealerVm>>();
                if (dealerList != null)
                {
                    dealers = dealerList.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealers;
        }

        public async Task<IEnumerable<DealerVm>> GetAllDealers()
        {
            List<DealerVm> dealers = new List<DealerVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "Dealer/GetAllDealers");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var dealerList = await response.Content.ReadFromJsonAsync<IEnumerable<DealerVm>>();
                if (dealerList != null)
                {
                    dealers = dealerList.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealers;
        }

        public async Task<DealerVm> GetDealerById(long dealerId)
        {
            DealerVm dealer = new DealerVm();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/GetDealerById?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var dealerVm = await response.Content.ReadFromJsonAsync<DealerVm>();
                if (dealerVm != null)
                {
                    dealer = dealerVm;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealer;
        }

        public async Task<IEnumerable<UserVm>> GetDealerUsers(long dealerId)
        {
            List<UserVm> dealerUsers = new List<UserVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/GetDealerUsersById?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var users = await response.Content.ReadFromJsonAsync<IEnumerable<UserVm>>();
                if (response.IsSuccessStatusCode && users != null && users.Any())
                {
                    dealerUsers = users.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerUsers;
        }
        public async Task<IEnumerable<UserVm>> GetDealerCustomers(long dealerId)
        {
            List<UserVm> dealerUsers = new List<UserVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/GetDealerCustomersById?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var users = await response.Content.ReadFromJsonAsync<IEnumerable<UserVm>>();
                if (response.IsSuccessStatusCode && users != null && users.Any())
                {
                    dealerUsers = users.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerUsers;
        }

        public async Task<UserVm> AddUpdateDealerCustomer(DealerCustomerVm dealerCustomer)
        {
            UserVm? customer = new UserVm();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/AddUpdateDealerCustomer");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(dealerCustomer), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                var dealerCust = await response.Content.ReadFromJsonAsync<UserVm>();

                if (response.IsSuccessStatusCode && dealerCust != null)
                {
                    customer = dealerCust;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return customer;
        }

        public async Task<IEnumerable<DealerDeviceSerialNumberVm>> GetDealerSerialNumbers(DealerSerialNumberFilterParametersVm dealerSerialNumberFilterParameters)
        {
            List<DealerDeviceSerialNumberVm> dealerSerialNumbers = new List<DealerDeviceSerialNumberVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/GetDeviceSerialNumbersForDealer");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(dealerSerialNumberFilterParameters), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                var dealerList = await response.Content.ReadFromJsonAsync<List<DealerDeviceSerialNumberVm>>();
                if (dealerList != null)
                {
                    dealerSerialNumbers = dealerList.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerSerialNumbers;
        }

        public async Task<DealerDeviceSerialNumberVm> AddEditDealerDeviceSerialNumber(DealerDeviceSerialNumberVm deviceSerialNumberVm)
        {
            DealerDeviceSerialNumberVm? dealerDeviceSerialNumber = new DealerDeviceSerialNumberVm();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/AddUpdateDealerDeviceSerialNumber");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(deviceSerialNumberVm), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                DealerDeviceSerialNumberVm? dealerSerialNumber = await response.Content.ReadFromJsonAsync<DealerDeviceSerialNumberVm>();

                dealerDeviceSerialNumber = dealerSerialNumber;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerDeviceSerialNumber;
        }

        public async Task<IEnumerable<DealerVm>> GetDealersByName(string dealerName)
        {
            List<DealerVm> dealers = new List<DealerVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/GetDealersByName?dealerName={dealerName}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var dealerList = await response.Content.ReadFromJsonAsync<IEnumerable<DealerVm>>();
                if (dealerList != null)
                {
                    dealers = dealerList.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealers;
        }

        public async Task<IEnumerable<DealerDeviceSerialNumberVm>> GetDealerSerialNumbersBySerialNumber(long dealerId, string serialNumber)
        {
            List<DealerDeviceSerialNumberVm>? dealerDeviceSerialNumbers = new List<DealerDeviceSerialNumberVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/GetDealerSerialNumbersBySerialNumber?dealerId={dealerId}&serialNumber={serialNumber}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                List<DealerDeviceSerialNumberVm>? dealerSerialNumbers = await response.Content.ReadFromJsonAsync<List<DealerDeviceSerialNumberVm>>();

                if(dealerSerialNumbers != null && dealerSerialNumbers.Any())
                    dealerDeviceSerialNumbers = dealerSerialNumbers;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerDeviceSerialNumbers;
        }

        public async Task<DealerDeviceSerialNumberVm> UpdateDealerDeviceSerialNumberOverride(DealerDeviceSerialNumberVm deviceSerialNumber)
        {
            DealerDeviceSerialNumberVm? dealerDeviceSerialNumber = new DealerDeviceSerialNumberVm();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/UpdateDealerDeviceSerialNumberOverride");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(deviceSerialNumber), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                DealerDeviceSerialNumberVm? dealerSerialNumber = await response.Content.ReadFromJsonAsync<DealerDeviceSerialNumberVm>();

                dealerDeviceSerialNumber = dealerSerialNumber;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerDeviceSerialNumber;
        }

        public async Task<IEnumerable<DealerAccountBalanceVm>> GetDealerAccountBalance(long dealerId)
        {
            List<DealerAccountBalanceVm> dealerAccountBalanceVms = new List<DealerAccountBalanceVm>();

            try 
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/GetDealerAccountBalance?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var dealerAccountBalances = await response.Content.ReadFromJsonAsync<IEnumerable<DealerAccountBalanceVm>>();

                if (dealerAccountBalances != null && dealerAccountBalances.Any())
                { 
                    dealerAccountBalanceVms = dealerAccountBalances.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerAccountBalanceVms;
        }

        public async Task<AddCustomerVoucherForDealerVm> AddCustomerVoucher(AddCustomerVoucherForDealerVm voucherCreate)
        {
            AddCustomerVoucherForDealerVm addCustomerVoucherForDealerVm = new();

            try 
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/AddCustomerVoucherForDealer");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(voucherCreate), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                AddCustomerVoucherForDealerVm? addCustomerVoucher = await response.Content.ReadFromJsonAsync<AddCustomerVoucherForDealerVm>();
                
                if(addCustomerVoucher != null)
                    addCustomerVoucherForDealerVm = addCustomerVoucher;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return addCustomerVoucherForDealerVm;
        }

        public async Task<DealerHoldCheckVm> IsDealerOnHold(long dealerId)
        {
            DealerHoldCheckVm dealerHoldCheckVm = new DealerHoldCheckVm();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/IsDealerOnHold?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    DealerHoldCheckVm? dealerHoldCheck = await response.Content.ReadFromJsonAsync<DealerHoldCheckVm>();
                    if (dealerHoldCheck != null)
                    {
                        dealerHoldCheckVm = dealerHoldCheck;
                    }
                }
                else 
                {
                    dealerHoldCheckVm!.IsOnHold = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                dealerHoldCheckVm!.IsOnHold = false;
            }

            return dealerHoldCheckVm;
        }

        public async Task<IEnumerable<DealerHoldVm>> GetActiveDealerHolds()
        {
            List<DealerHoldVm> dealerHolds = new List<DealerHoldVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "Dealer/GetAllActiveDealerHolds");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var activeHolds = await response.Content.ReadFromJsonAsync<IEnumerable<DealerHoldVm>>();

                if (activeHolds != null && activeHolds.Any())
                {
                    dealerHolds = activeHolds.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerHolds;
        }

        public async Task<IEnumerable<DealerHoldVm>> GetAllHoldsForDealer(long dealerId)
        {
            List<DealerHoldVm> dealerHolds = new List<DealerHoldVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/GetAllHoldsForDealer?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var holds = await response.Content.ReadFromJsonAsync<IEnumerable<DealerHoldVm>>();

                if (holds != null && holds.Any())
                {
                    dealerHolds = holds.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerHolds;
        }

        public async Task<DealerHoldVm> AddDealerHold(DealerHoldVm dealerHold)
        {
            DealerHoldVm dealerHoldVm = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/AddDealerHold");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(dealerHold), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                DealerHoldVm? addDealerHold = await response.Content.ReadFromJsonAsync<DealerHoldVm>();

                if (addDealerHold != null)
                    dealerHoldVm = addDealerHold;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerHoldVm;
        }

        public async Task<DealerHoldVm> RemoveDealerHold(DealerHoldVm dealerHold)
        {
            DealerHoldVm dealerHoldVm = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "Dealer/RemoveDealerHold");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(dealerHold), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                DealerHoldVm? removeDealerHold = await response.Content.ReadFromJsonAsync<DealerHoldVm>();

                if (removeDealerHold != null)
                    dealerHoldVm = removeDealerHold;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerHoldVm;
        }

        public async Task<IEnumerable<DealerCreditLimitVm>> GetDealerCreditLimits(string? filter)
        {
            List<DealerCreditLimitVm> dealerCreditLimits = new();
            
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Reporting/GetDealerCreditLimitReport?filter={filter}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var creditLimits = await response.Content.ReadFromJsonAsync<IEnumerable<DealerCreditLimitVm>>();

                if (creditLimits != null && creditLimits.Any())
                {
                    dealerCreditLimits = creditLimits.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerCreditLimits;
        }

        public async Task<DealerCreditLimitVm> AddUpdateDealerCreditLimit(DealerCreditLimitVm dealerCreditLimit)
        {
            DealerCreditLimitVm dealerCreditLimitVm = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/AddUpdateDealerCreditLimit");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(dealerCreditLimit), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                DealerCreditLimitVm? creditLimit = await response.Content.ReadFromJsonAsync<DealerCreditLimitVm>();

                if (creditLimit != null)
                    dealerCreditLimitVm = creditLimit;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerCreditLimitVm;
        }

        public async Task<DealerCreditLimitVm> GetDealerCreditLimitForDealer(long dealerId)
        {
            DealerCreditLimitVm dealerCreditLimit = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Reporting/GetDealerCreditLimitForDealer?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var creditLimit = await response.Content.ReadFromJsonAsync<DealerCreditLimitVm>();

                if (creditLimit != null)
                {
                    dealerCreditLimit = creditLimit;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerCreditLimit;
        }

        public async Task<IEnumerable<DealerAccountBalanceRequestVm>> GetAccountBalanceRequestsForDealer(long dealerId)
        {
            List<DealerAccountBalanceRequestVm> dealerAccountBalanceRequests = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Voucher/GetDealerAccountBalanceRequestsForDealer?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var balanceRequests = await response.Content.ReadFromJsonAsync<IEnumerable<DealerAccountBalanceRequestVm>>();

                if (balanceRequests != null && balanceRequests.Any())
                {
                    dealerAccountBalanceRequests = balanceRequests.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerAccountBalanceRequests;
        }

        public async Task<DealerAccountBalanceRequestVm> AddUpdateDealerAccountBalanceRequest(DealerAccountBalanceRequestVm accountBalanceRequest)
        {
            DealerAccountBalanceRequestVm accountBalanceRequestVm = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Voucher/AddUpdateDealerAccountBalanceRequest");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(accountBalanceRequest), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                DealerAccountBalanceRequestVm? balanceRequest = await response.Content.ReadFromJsonAsync<DealerAccountBalanceRequestVm>();

                if (balanceRequest != null)
                {
                    accountBalanceRequestVm = balanceRequest;
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex); 
            }

            return accountBalanceRequestVm;
        }

        public async Task<IEnumerable<DealerVm>> GetSubDealersForDealer(long dealerId)
        {
            List<DealerVm> dealers = new List<DealerVm>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Dealer/GetSubDealersForDealer?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var dealerList = await response.Content.ReadFromJsonAsync<IEnumerable<DealerVm>>();
                if (dealerList != null)
                {
                    dealers = dealerList.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealers;
        }

        public async Task<bool> ApproveDealerAccountBalanceRequest(DealerAccountBalanceRequestVm dealerAccountBalanceRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/ApproveDealerAccountBalanceRequest");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(dealerAccountBalanceRequest), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                bool result = await response.Content.ReadFromJsonAsync<bool>();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DenyDealerAccountBalanceRequest(DealerAccountBalanceRequestVm dealerAccountBalanceRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/DenyDealerAccountBalanceRequest");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(dealerAccountBalanceRequest), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                bool result = await response.Content.ReadFromJsonAsync<bool>();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> RevokeSubDealerAccountBalance(RevokeSubDealerAccountBalanceVm revokeSubDealerAccountBalance)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Dealer/RevokeSubDealerAccountBalanceRequest");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(revokeSubDealerAccountBalance), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                bool result = await response.Content.ReadFromJsonAsync<bool>();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<int> GetDealerPendingAccountBalanceRequests(long dealerId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Reporting/GetDealerPendingAccountBalanceRequests?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var count = await response.Content.ReadFromJsonAsync<int>();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public async Task<IEnumerable<DealerAccountBalanceRequestVm>> GetAccountBalanceRequestForParentDealer(long dealerId)
        {
            List<DealerAccountBalanceRequestVm> dealerAccountBalanceRequests = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Voucher/GetDealerAccountBalanceRequestsForParentDealer?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var balanceRequests = await response.Content.ReadFromJsonAsync<IEnumerable<DealerAccountBalanceRequestVm>>();

                if (balanceRequests != null && balanceRequests.Any())
                {
                    dealerAccountBalanceRequests = balanceRequests.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dealerAccountBalanceRequests;
        }

        public async Task<IEnumerable<DealerTransactionVm>> GetTransactionsForDealer(DealerTransactionParametersVm queryParameters)
        {
            List<DealerTransactionVm> transactions = new();

            try 
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "Reporting/GetDealerTransactions");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(queryParameters), Encoding.UTF8, "application/json");
                using var response = await _httpClient.SendAsync(request);
                var results = await response.Content.ReadFromJsonAsync<IEnumerable<DealerTransactionVm>>();
                if (results != null && results.Any())
                { 
                    transactions = results.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return transactions;
        }

		public async Task<IEnumerable<DealerSpendingChartDataVm>> GetDealerSpendingChartData(long dealerId)
		{
			List<DealerSpendingChartDataVm> spendingChartDataVms = new();

            try 
            {
				var request = new HttpRequestMessage(HttpMethod.Get, $"Reporting/GetDealerSpendingChartData?dealerId={dealerId}");
				using HttpResponseMessage? response = await _httpClient.SendAsync(request);
				var results = await response.Content.ReadFromJsonAsync<IEnumerable<DealerSpendingChartDataVm>>();

				if (results != null && results.Any())
				{
					spendingChartDataVms = results.ToList();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return spendingChartDataVms;
		}

        public async Task<int> GetDealerCompletedRequestCount(long dealerId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Reporting/GetDealerCompletedRequestCount?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var count = await response.Content.ReadFromJsonAsync<int>();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public async Task<int> GetDealerDeniedRequestCount(long dealerId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Reporting/GetDealerDeniedRequestCount?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var count = await response.Content.ReadFromJsonAsync<int>();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public async Task<decimal> GetDealerSubDealersAccountBalanceTotal(long dealerId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Reporting/GetDealerSubDealersAccountBalanceTotal?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var total = await response.Content.ReadFromJsonAsync<decimal>();
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public async Task<decimal> GetDealerVoucherBalanceTotal(long dealerId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Reporting/GetDealerVoucherBalanceTotal?dealerId={dealerId}");
                using HttpResponseMessage? response = await _httpClient.SendAsync(request);
                var total = await response.Content.ReadFromJsonAsync<decimal>();
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }
    }

    public interface IDealerService 
    {
        public Task<IEnumerable<UserVm>> GetDealerUsers(long dealerId);

        public Task<IEnumerable<UserVm>> GetDealerCustomers(long dealerId);

        public Task<DealerVm> GetDealerById(long dealerId);

        public Task<IEnumerable<DealerVm>> GetAllDealers(DealerFilterParametersVm dealerFilterParameters);

        public Task<IEnumerable<DealerVm>> GetAllDealers();

        public Task<IEnumerable<DealerVm>> GetSubDealersForDealer(long dealerId);

        public Task<IEnumerable<DealerVm>> GetDealersByName(string dealerName);

        public Task<UserVm> AddUpdateDealerCustomer(DealerCustomerVm dealerCustomer);

        public Task<IEnumerable<DealerDeviceSerialNumberVm>> GetDealerSerialNumbers(DealerSerialNumberFilterParametersVm dealerSerialNumberFilterParameters);

        public Task<DealerDeviceSerialNumberVm> AddEditDealerDeviceSerialNumber(DealerDeviceSerialNumberVm deviceSerialNumberVm);

        public Task<IEnumerable<DealerDeviceSerialNumberVm>> GetDealerSerialNumbersBySerialNumber(long dealerId, string serialNumber);

        public Task<DealerDeviceSerialNumberVm> UpdateDealerDeviceSerialNumberOverride(DealerDeviceSerialNumberVm deviceSerialNumber);

        public Task<IEnumerable<DealerAccountBalanceVm>> GetDealerAccountBalance(long dealerId);

        public Task<AddCustomerVoucherForDealerVm> AddCustomerVoucher(AddCustomerVoucherForDealerVm voucherCreate);

        public Task<DealerHoldVm> AddDealerHold(DealerHoldVm dealerHold);

        public Task<DealerHoldVm> RemoveDealerHold(DealerHoldVm dealerHold);

        public Task<DealerHoldCheckVm> IsDealerOnHold(long dealerId);

        public Task<IEnumerable<DealerHoldVm>> GetActiveDealerHolds();

        public Task<IEnumerable<DealerHoldVm>> GetAllHoldsForDealer(long dealerId);

        public Task<IEnumerable<DealerCreditLimitVm>> GetDealerCreditLimits(string filter);

        public Task<DealerCreditLimitVm> AddUpdateDealerCreditLimit(DealerCreditLimitVm dealerCreditLimit);

        public Task<DealerCreditLimitVm> GetDealerCreditLimitForDealer(long dealerId);
        
        public Task<IEnumerable<DealerAccountBalanceRequestVm>> GetAccountBalanceRequestsForDealer(long dealerId);

        public Task<DealerAccountBalanceRequestVm> AddUpdateDealerAccountBalanceRequest(DealerAccountBalanceRequestVm accountBalanceRequest);

        public Task<bool> ApproveDealerAccountBalanceRequest(DealerAccountBalanceRequestVm approveDealerAccountBalanceRequest);

        public Task<bool> DenyDealerAccountBalanceRequest(DealerAccountBalanceRequestVm approveDealerAccountBalanceRequest);

        public Task<bool> RevokeSubDealerAccountBalance(RevokeSubDealerAccountBalanceVm revokeSubDealerAccountBalance);

        public Task<int> GetDealerPendingAccountBalanceRequests(long dealerId);

        public Task<IEnumerable<DealerAccountBalanceRequestVm>> GetAccountBalanceRequestForParentDealer(long dealerId);

        public Task<IEnumerable<DealerTransactionVm>> GetTransactionsForDealer(DealerTransactionParametersVm queryParameters);

		public Task<IEnumerable<DealerSpendingChartDataVm>> GetDealerSpendingChartData(long dealerId);

        public Task<int> GetDealerCompletedRequestCount(long dealerId);

        public Task<int> GetDealerDeniedRequestCount(long dealerId);

        public Task<decimal> GetDealerSubDealersAccountBalanceTotal(long dealerId);

        public Task<decimal> GetDealerVoucherBalanceTotal(long dealerId);
    }
}
