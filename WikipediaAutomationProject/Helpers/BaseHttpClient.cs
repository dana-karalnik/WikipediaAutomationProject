namespace WikipediaAutomationProject.Helpers.Api
{
    public class BaseHttpClient
    {
        protected HttpClient Client { get; }
        protected string BaseUrl { get; }
        public BaseHttpClient(string baseUrl)
        {
            this.BaseUrl = baseUrl;
            Client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            Client.DefaultRequestHeaders.Add("User-Agent", "Automation");
        }

        protected async Task<string> GetAsync(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
