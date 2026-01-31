using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PharmaCoreApi.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PharmaCoreApi.Models
{
    public class CouchRepository : ICouchRepository
    {
        private readonly ILogger<CouchRepository> _logger;
        private readonly string _couchDbUrl;
        private readonly string _couchDbName;
        private readonly string _couchDbUser;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        // _httpClient isn't exposed publicly
       // private readonly HttpClient _httpClient;

        public CouchRepository(IConfiguration configuration, IHttpClientFactory clientFactory, ILogger<CouchRepository> logger)
        {
            _logger = logger;
            _configuration = configuration;
           // _httpClient = client;
             _clientFactory = clientFactory;
            _couchDbUrl = this._configuration["CouchDB:URL"];
            _couchDbName = this._configuration["CouchDB:DbName"];
            _couchDbUser = this._configuration["CouchDB:User"];
        }

        public async Task<HttpClientResponse> DeleteDocumentAsync(string id, string rev)
        {
            HttpClientResponse response = new HttpClientResponse();
            var _httpClient = _clientFactory.CreateClient("Couchdb");

            //CouchDB URL : DELETE http://{hostname_or_IP}:{Port}/{couchDbName}/{_id}/?rev={_rev}  
            var dbResult = await _httpClient.DeleteAsync(_couchDbName + "/" + id + "?rev=" + rev);

            if (dbResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await dbResult.Content.ReadAsStringAsync();
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = dbResult.ReasonPhrase;
            }
            return response;
        }

        public async Task<HttpClientResponse> GetAllDocumentsAsync(int? limit = null, int? skip = null)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            HttpClientResponse response = new HttpClientResponse();
            var _httpClient = _clientFactory.CreateClient("Couchdb");
            string url = _couchDbName + "/_all_docs?include_docs=true";
            if (limit.HasValue) url += "&limit=" + limit.Value;
            if (skip.HasValue) url += "&skip=" + skip.Value;
            var dbResult = await _httpClient.GetAsync(url).ConfigureAwait(false);
            sw.Stop();
            _logger.LogInformation($"GetAllDocumentsAsync took {sw.ElapsedMilliseconds} ms");
            if (dbResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await dbResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = dbResult.ReasonPhrase;
            }
            return response;
        }

        public async Task<HttpClientResponse> GetDocumentAsync(string id)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            HttpClientResponse response = new HttpClientResponse();
            var _httpClient = _clientFactory.CreateClient("Couchdb");
            var dbResult = await _httpClient.GetAsync(_couchDbName + "/" + id).ConfigureAwait(false);
            sw.Stop();
            _logger.LogInformation($"GetDocumentAsync for id {id} took {sw.ElapsedMilliseconds} ms");
            if (dbResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await dbResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = dbResult.ReasonPhrase;
            }
            return response;
        }

        public async Task<HttpClientResponse> GetViewAsync(QueryView queryView)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            _logger.LogInformation("starting in FindDocument.");
            HttpClientResponse response = new HttpClientResponse();
            _logger.LogInformation("starting httpclient from clientfactory.");
            var _httpClient = _clientFactory.CreateClient("Couchdb");
            _logger.LogInformation("Completed httpclient from clientfactory.");
            var queryParameters = queryView.Filters.GetQueryString();
            _logger.LogInformation("Fetching data from client");
            var dbResult = await _httpClient.GetAsync(_couchDbName + "/_design/" + queryView.DesignDocumentName + "/_view/" + queryView.ViewName + queryParameters).ConfigureAwait(false);
            sw.Stop();
            _logger.LogInformation($"GetViewAsync took {sw.ElapsedMilliseconds} ms");
            _logger.LogInformation("Fetched data from client");
            if (dbResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await dbResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = dbResult.ReasonPhrase;
                throw new Exception(String.Format("{0} {1} {2}", dbResult.ReasonPhrase, dbResult.Content.ReadAsStringAsync().Result, dbResult.StatusCode));
            }
            return response;
        }

        public async Task<HttpClientResponse> FindDocument(Query str)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            _logger.LogInformation("starting in FindDocument.");
            HttpClientResponse response = new HttpClientResponse();
            var _httpClient = _clientFactory.CreateClient("Couchdb");
            var jsonData = JsonConvert.SerializeObject(str);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var dbResult = await _httpClient.PostAsync(_couchDbName + "/_find", httpContent).ConfigureAwait(false);
            sw.Stop();
            _logger.LogInformation($"FindDocument took {sw.ElapsedMilliseconds} ms");
            if (dbResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await dbResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = dbResult.ReasonPhrase;
            }
            return response;
        }

        private dynamic AttachmentsSelector()
        {

            var selector = new
            {
                selector = new
                {
                    _id = "1000004606",
                    //_attachments = isExists
                },
                fields = new ArrayList {
                "_id",
                "_rev"

            }
            };
            return selector;
        }

        public async Task<HttpClientResponse> PostDocumentAsync(PharmaDetails pharmaDetails)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            HttpClientResponse response = null;
            try
            {
                response = new HttpClientResponse();
                var _httpClient = _clientFactory.CreateClient("Couchdb");
                var jsonData = JsonConvert.SerializeObject(pharmaDetails);
                var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var postResult = await _httpClient.PostAsync(_couchDbName, httpContent).ConfigureAwait(false);
                sw.Stop();
                _logger.LogInformation($"PostDocumentAsync took {sw.ElapsedMilliseconds} ms");
                if (postResult.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    response.SuccessContentObject = await postResult.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else
                {
                    response.IsSuccess = false;
                    response.FailedReason = postResult.ReasonPhrase;
                }
            }
            catch (WebException exception)
            {
                using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    response.FailedReason = reader.ReadToEnd();
                    response.IsSuccess = false;
                }
            }
            return response;
        }

        public async Task<HttpClientResponse> PutDocumentAsync(UpdatePharmaDetails update)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            HttpClientResponse response = new HttpClientResponse();
            var _httpClient = _clientFactory.CreateClient("Couchdb");
            var updateToDb = new
            {
                update.Name,
                update.ExpiredOn,
                update.UpdatedOn
            };
            var jsonData = JsonConvert.SerializeObject(updateToDb);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var putResult = await _httpClient.PutAsync(_couchDbName + "/" +
                                              update.Id +
                                              "?rev=" + update.Rev, httpContent).ConfigureAwait(false);
            sw.Stop();
            _logger.LogInformation($"PutDocumentAsync took {sw.ElapsedMilliseconds} ms");
            if (putResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await putResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = putResult.ReasonPhrase;
            }
            return response;
        }

        //private HttpClient DbHttpClient()
        //{
        //    var httpClient = this._clientFactory.CreateClient();
        //    httpClient.DefaultRequestHeaders.Accept.Clear();
        //    httpClient.DefaultRequestHeaders.Clear();

        //    httpClient.BaseAddress = new Uri(_couchDbUrl);
        //    var dbUserByteArray = Encoding.ASCII.GetBytes(_couchDbUser);
        //    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(dbUserByteArray));
        //    return httpClient;
        //}
    }
}
