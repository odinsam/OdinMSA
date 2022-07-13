using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OdinMSA.Core.Core.Interface;

public interface IMsaWebApi
{
    /// <summary>
    /// http Request Get
    /// </summary>
    /// <param name="apiPath">apiPath</param>
    /// <param name="clientName">clientName</param>
    /// <param name="headers">headers</param>
    /// <param name="timeOut">timeOut</param>
    /// <param name="mediaType">mediaType</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T Get<T>(
        string apiPath,
        string clientName = "default",
        Dictionary<string, string> headers = null,
        int timeOut = 10,
        string mediaType = "application/json");

    /// <summary>
    /// http request post
    /// </summary>
    /// <param name="apiPath">apiPath</param>
    /// <param name="postData">postData</param>
    /// <param name="clientName">clientName</param>
    /// <param name="headers">headers</param>
    /// <param name="timeOut">timeOut</param>
    /// <param name="mediaType">mediaType</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T Post<T>(
        string apiPath,
        Object postData = null,
        string clientName = "default",
        Dictionary<string, string> headers = null,
        int timeOut = 10,
        string mediaType = "application/json");
    
    /// <summary>
    /// http request post by httpContent
    /// </summary>
    /// <param name="apiPath">apiPath</param>
    /// <param name="content">content</param>
    /// <param name="clientName">clientName</param>
    /// <param name="headers">headers</param>
    /// <param name="timeOut">timeOut</param>
    /// <param name="mediaType">mediaType</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T PostByHttpContent<T>(
        string apiPath,
        HttpContent content,
        string clientName = "default",
        Dictionary<string, string> headers = null,
        int timeOut = 10,
        string mediaType = "application/json");
    
    /// <summary>
    /// Generate StringContent
    /// </summary>
    /// <param name="str">string</param>
    /// <returns></returns>
    HttpContent GenerateStringContent(string str);
    
    /// <summary>
    /// Generate MultipartFormDataContent
    /// </summary>
    /// <param name="dic">content data</param>
    /// <param name="dicFile">content files</param>
    /// <returns></returns>
    HttpContent GenerateMultipartFormDataContent(
        Dictionary<string, string> dic,
        Dictionary<string, string> dicFile);
}