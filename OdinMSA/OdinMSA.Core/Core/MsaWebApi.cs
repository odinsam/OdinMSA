using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OdinModels.OdinUtils.OdinExceptionExtensions;
using OdinMSA.Core.Core.Interface;

namespace OdinMSA.Core.Core;

public class MsaWebApi : IMsaWebApi
{
    private readonly IHttpClientFactory _httpClientFactory;
    public MsaWebApi(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
    }

    public T Get<T>(
        string apiPath,
        string clientName="default", 
        Dictionary<string,string> headers=null,
        int timeOut=10,
        string mediaType="application/json")
    {
        var client = GenerateHttpClient(apiPath, clientName, headers, timeOut, mediaType);
        var httpResponseMessage = client.GetAsync(apiPath).GetAwaiter().GetResult();
        return GetHttpClientResponse<T>(httpResponseMessage);
    }
    
    public T Post<T>(
        string apiPath,
        Object postData = null,
        string clientName="default", 
        Dictionary<string,string> headers=null,
        int timeOut=10,
        string mediaType="application/json")
    {
        var client = GenerateHttpClient(apiPath, clientName, headers, timeOut, mediaType);
        var httpContent = postData == null ? new StringContent("") : new StringContent(JsonConvert.SerializeObject(postData));
        var httpResponseMessage = client.PostAsync(apiPath,httpContent).GetAwaiter().GetResult();
        return GetHttpClientResponse<T>(httpResponseMessage);
    }
    
    public T PostByHttpContent<T>(
        string apiPath,
        HttpContent content,
        string clientName="default", 
        Dictionary<string,string> headers=null,
        int timeOut=10,
        string mediaType="application/json")
    {
        var client = GenerateHttpClient(apiPath, clientName, headers, timeOut, mediaType);
        var httpResponseMessage = client.PostAsync(apiPath,content).GetAwaiter().GetResult();
        return GetHttpClientResponse<T>(httpResponseMessage);
    }

    public HttpContent GenerateStringContent(string str)
    {
        return new StringContent(str);
    }
    
    public HttpContent GenerateMultipartFormDataContent(
        Dictionary<string,string> dic,
        Dictionary<string, string> dicFile)
    {
        var form = new MultipartFormDataContent();
        string boundary = String.Format("--{0}", DateTime.Now.Ticks.ToString("x"));
        form.Headers.Add("ContentType", $"multipart/form-data, boundary={boundary}");
        if (dic != null)
        {
            foreach (var key in dic.Keys)
                form.Add(new StringContent(dic[key].ToString()), key);
        }
        if (dicFile != null)
        {
            foreach (var key in dicFile.Keys)
            {
                var fileContent = new ByteArrayContent(File.ReadAllBytes(dicFile[key].ToString()));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                form.Add(fileContent, name: key, fileName: dicFile[key].ToString());
            }
        }
        return form;
    }

    private T GetHttpClientResponse<T>(HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return ConvertReadResult<T>(httpResponseMessage.Content);
        }
        else
            throw new OdinException(OdinMSACoreException.WebApiHttpResponseCode,httpResponseMessage.StatusCode.ToString());
    }
    private HttpClient GenerateHttpClient(
        string apiPath,
        string clientName="default", 
        Dictionary<string,string> headers=null,
        int timeOut=10,
        string mediaType="application/json")
    {
        var client = this._httpClientFactory.CreateClient(clientName);
        if (headers != null && headers.Count > 0)
        {
            foreach (var header in headers)   
            {
                client.DefaultRequestHeaders.Add(header.Key,header.Value);
            }
        }
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        client.Timeout = TimeSpan.FromSeconds(timeOut);
        return client;
    }
    
    private T ConvertReadResult<T>(HttpContent httpContent)
    {
        if (typeof(T)==typeof(String))
        {
            var result = httpContent.ReadAsStringAsync();
            return (T)Convert.ChangeType(result.GetAwaiter().GetResult(),typeof(T));
        }
        else if (typeof(T) == typeof(Stream))
        {
            var result = httpContent.ReadAsStreamAsync();
            return (T)Convert.ChangeType(result,typeof(T));
        }
        else if (typeof(T) == typeof(byte[]))
        {
            var result = httpContent.ReadAsByteArrayAsync();
            return (T)Convert.ChangeType(result,typeof(T));
        }
        else
        {
            var result = httpContent.ReadFromJsonAsync<T>();
            return result.Result;
        }
    }
}