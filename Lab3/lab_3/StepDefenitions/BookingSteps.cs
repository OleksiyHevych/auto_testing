using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace lab_3
{
    public class BookingDetails
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public BookingDates bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }

    public class BookingDates
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }
    
    [Binding]
    public class BookingSteps
    {
        protected RestClient client;
        protected RestRequest request;
        protected RestResponse response;
        private BookingDetails bookingDetails;
        private ScenarioContext _scenarioContext;

        public BookingSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have access to the bookings API")]
        public void GivenIHaveAccessToTheBookingsAPI()
        {
            client = new RestClient("https://restful-booker.herokuapp.com");
        }

        [When(@"I send a GET request to ""(.*)""")]
        public void WhenISendAGETRequestTo(string endpoint)
        {
            request = new RestRequest(endpoint, Method.Get);
            response = client.Execute(request);
        }

        [Then(@"I receive a response with status code ""(.*)""")]
        public void ThenIReceiveAResponseWithStatusCode(string expectedStatusCode)
        {
            var actualStatusCode = (int)response.StatusCode;
            Assert.That(Enum.Parse(typeof(HttpStatusCode), expectedStatusCode), Is.EqualTo(response.StatusCode));
        }
        
        [Then(@"The response contains a list of booking IDs")]
        public void ThenTheResponseContainsAListOfBookingIDs()
        {
            var bookings = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(response.Content);
            Assert.IsTrue(bookings.Count > 0);
        }

        [Given(@"I have valid booking details")]
        public void GivenIHaveValidBookingDetails()
        {
            bookingDetails = new BookingDetails
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new BookingDates { checkin = "2023-01-01", checkout = "2023-01-02" },
                additionalneeds = "Breakfast"
            };
        }

        [When(@"I send a POST request to ""(.*)"" with the booking details")]
        public void WhenISendAPOSTRequestToWithTheBookingDetails(string endpoint)
        {
            request = new RestRequest(endpoint, Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(bookingDetails);
            response = client.Execute(request);
        }

        [Then(@"The response contains the booking ID and details")]
        public void ThenTheResponseContainsTheBookingIDAndDetails()
        {

            var bookingResponse = JObject.Parse(response.Content);
            Assert.IsNotNull(bookingResponse["bookingid"]);

            var booking = bookingResponse["booking"];

            AssertBookingAndJTocken(bookingDetails, booking);

            var bookingId = (int)bookingResponse["bookingid"];
            _scenarioContext["BookingId"] = bookingId;
        }

        [Given(@"I have a booking ID from a created booking")]
        public void GivenIHaveABookingIDFromACreatedBooking()
        {
            if (!_scenarioContext.ContainsKey("BookingId"))
            {
                Assert.Fail("Booking ID is not available in ScenarioContext");
            }

            bookingDetails = new BookingDetails
            {
                firstname = "Daryna",
                lastname = "R",
                totalprice = 113,
                depositpaid = true,
                bookingdates = new BookingDates { checkin = "2033-02-01", checkout = "2034-01-02" },
                additionalneeds = "Mood"
            };
        }

        [When(@"I send a PUT request to ""(.*)"" with the updated details")]
        public void WhenISendAPUTRequestToWithTheUpdatedDetailsInXML(string endpoint)
        {
            var bookingId = (int)_scenarioContext["BookingId"];
            request = new RestRequest(endpoint.Replace("{bookingId}", bookingId.ToString()), Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");
            request.AddJsonBody(bookingDetails);
            response = client.Execute(request);
        }

        [Then(@"The response reflects the updated booking details")]
        public void ThenTheResponseReflectsTheUpdatedBookingDetails()
        {
            var receivedBookingDetails = JObject.Parse(response.Content);

            
        }

        [When(@"I send a DELETE request to ""(.*)""")]
        public void WhenISendADELETERequestTo(string endpoint)
        {
            var bookingId = (int)_scenarioContext["BookingId"];
            request = new RestRequest(endpoint.Replace("{bookingId}", bookingId.ToString()), Method.Delete);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");
            response = client.Execute(request);
        }
        public void AssertBookingAndJTocken(BookingDetails excepted, JToken received) {
            Assert.That(excepted.firstname, Is.EqualTo((string)received["firstname"]));
            Assert.That(excepted.lastname, Is.EqualTo((string)received["lastname"]));
            Assert.That(excepted.totalprice, Is.EqualTo((int)received["totalprice"]));
            Assert.That(excepted.depositpaid, Is.EqualTo((bool)received["depositpaid"]));
            Assert.That(excepted.bookingdates.checkin, Is.EqualTo((string)received["bookingdates"]["checkin"]));
            Assert.That(excepted.bookingdates.checkout, Is.EqualTo((string)received["bookingdates"]["checkout"]));
            Assert.That(excepted.additionalneeds, Is.EqualTo((string)received["additionalneeds"]));

        }
    }
}
