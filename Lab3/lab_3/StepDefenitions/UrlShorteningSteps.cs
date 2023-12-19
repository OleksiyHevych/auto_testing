using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

[Binding]
public class UrlShorteningSteps
{
    public static class UrlShortenerApi
    {
        public static RestResponse ShortenUrl(string longUrl, RestClient client)
        {
            var request = new RestRequest("shorten", Method.Post);
            request.AddParameter("url", longUrl);
            return client.Execute(request);
        }
    }

    private RestClient _client;
    private string _longUrl;
    private RestResponse _response;
    private string _shortenedUrl;

    [Given(@"I have a valid long URL ""(.*)""")]
    public void GivenIHaveAValidLongURL(string url)
    {
        _longUrl = url;
    }

    [When(@"I request to shorten the URL")]
    public void WhenIRequestToShortenTheURL()
    {
        _client = new RestClient("https://cleanuri.com/api/v1/");
        _response = UrlShortenerApi.ShortenUrl(_longUrl,_client);
        if (_response.StatusCode == HttpStatusCode.OK)
        {
            var responseContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(_response.Content);
            _shortenedUrl = responseContent.ContainsKey("result_url") ? responseContent["result_url"] : null;
        }
    }

    [Then(@"I should receive a shortened URL")]
    public void ThenIShouldReceiveAShortenedURL()
    {
        Assert.IsTrue(_response.StatusCode == HttpStatusCode.OK);
        Assert.IsNotNull(_shortenedUrl);
    }

    [Then(@"the shortened URL should redirect to the original URL")]
    public void ThenTheShortenedURLShouldRedirectToTheOriginalURL()
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(_shortenedUrl));

        _client = new RestClient(_longUrl);
        var request = new RestRequest("", Method.Get);
        _client = new RestClient(_shortenedUrl);
        var redirectResponse = _client.Execute(request);
        var originalUrlResponce = _client.Execute(request);

        Assert.That(HttpStatusCode.Moved, Is.EqualTo(redirectResponse.StatusCode));
        Assert.AreEqual(_longUrl, redirectResponse.Headers.FirstOrDefault(h => h.Name == "Location")?.Value);
    }

    [Then(@"I should receive an error message")]
    public void ThenIShouldReceiveAnErrorMessage()
    {
        Assert.IsFalse(_response.Content.ToString() == "error");
    }
}
