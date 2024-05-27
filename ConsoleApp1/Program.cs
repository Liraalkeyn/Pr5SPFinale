using StablingApiClient;

const string apiUrl = "http://localhost:5269";

HttpClient a = new HttpClient();

CountryHttpClient httpClient = new CountryHttpClient(apiUrl, a);

Console.WriteLine(httpClient.GetCountryAsync(1).GetAwaiter().GetResult().NameOfCountry);

