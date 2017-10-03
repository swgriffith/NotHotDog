using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NotHotdog
{
    public static class NotHotdog
    {
        const string uriBase = "https://westus2.api.cognitive.microsoft.com/vision/v1.0/analyze";

        [FunctionName("NotHotdog")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
            //Read image URL from request body
            string imageURL = await req.Content.ReadAsStringAsync();

            //Call Cognitive Services via Make Request method
            HttpResponseMessage response = await MakeRequest(imageURL);

            //Read response message
            string cogResponse = await response.Content.ReadAsStringAsync();

            //Check if the response contains 'hot dog' and respond to caller accordingly
            if (cogResponse.Contains("hot dog"))
            {
                return req.CreateResponse(HttpStatusCode.OK, "Hot dog");
            }
            else
            {
                return req.CreateResponse(HttpStatusCode.OK, "Not hot dog");
            }
            }
            catch (Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Wrapper on the REST API call to Azure Cognitive Services
        /// </summary>
        /// <param name="imageURL">URL to Image you want to analyze</param>
        /// <returns></returns>
        static async Task<HttpResponseMessage> MakeRequest(string imageURL)
        {
            var client = new HttpClient();
            
            // Set Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", GetVisionAPIKey());

            // Set Request parameters. 
            string requestParameters = "visualfeatures=tags,description";

            // Assemble the URI for the REST API Call.
            string coguri = uriBase + "?" + requestParameters;
            
            HttpResponseMessage response;

            //Create the JSON payload for the Azure Computer Vision API
            reqbody body = new reqbody();
            body.url = imageURL;
            string payload = JsonConvert.SerializeObject(body);

            // Build Request body
            byte[] byteData = Encoding.UTF8.GetBytes(payload);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(coguri, content);
            }

            return response;
        }

        public static string GetVisionAPIKey()
        {
                var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
                string subscriptionKey = kv.GetSecretAsync("<secret ID>").Result.Value;
                return subscriptionKey;
        }

        public async static Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);

            ClientCredential clientCred = new ClientCredential("<AppID>", "<AppKey>");

            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)

                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

    }

    /// <summary>
    /// Class to JSON Serialize for the Vision API request body
    /// </summary>
    public class reqbody
    {
        public string url { get; set; }
    }
}
