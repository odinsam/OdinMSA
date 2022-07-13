using System;
using System.Threading.Tasks;

namespace OdinMSA.OdinConsul.Core.Interface;

public interface IRestTemplate
{
    Task<String> ResolveUrlAsync(String url);
}