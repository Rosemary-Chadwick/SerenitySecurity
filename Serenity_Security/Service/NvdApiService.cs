using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serenity_Security.Models.DTOs;

namespace Serenity_Security.Services
{
    public class NvdApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BaseUrl = "https://services.nvd.nist.gov/rest/json/cves/2.0";
        private readonly JsonSerializerOptions _jsonOptions;

        public NvdApiService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NvdApi:ApiKey"];

            if (!string.IsNullOrEmpty(_apiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("apiKey", _apiKey);
            }

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }

        public async Task<string> TestApiConnection()
        {
            try
            {
                // Test with a known CVE
                var response = await _httpClient.GetAsync($"{BaseUrl}?cveId=CVE-2021-44228");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException ex)
            {
                return $"API connection failed: {ex.Message}";
            }
        }

        public async Task<NvdApiResponseDto> SearchVulnerabilitiesByCveIdAsync(string cveId)
        {
            if (string.IsNullOrWhiteSpace(cveId))
            {
                return new NvdApiResponseDto { Vulnerabilities = new List<NvdVulnerabilityDto>() };
            }

            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}?cveId={cveId}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(
                    $"Raw API response: {content.Substring(0, Math.Min(500, content.Length))}"
                );

                // Try to manually deserialize just to inspect structure
                var jsonDoc = JsonDocument.Parse(content);
                Console.WriteLine(
                    $"cve property type: {jsonDoc.RootElement.GetProperty("vulnerabilities")[0].GetProperty("cve").ValueKind}"
                );

                var result = JsonSerializer.Deserialize<NvdApiResponseDto>(content, _jsonOptions);
                return result
                    ?? new NvdApiResponseDto { Vulnerabilities = new List<NvdVulnerabilityDto>() };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API error: {ex.Message}");
                throw;
            }

            // try
            // {
            //     var response = await _httpClient.GetAsync($"{BaseUrl}?cveId={cveId}");
            //     response.EnsureSuccessStatusCode();

            //     var content = await response.Content.ReadAsStringAsync();
            //     Console.WriteLine(
            //         $"NVD API Response for {cveId}: {content.Substring(0, Math.Min(500, content.Length))}"
            //     );

            //     var result = JsonSerializer.Deserialize<NvdApiResponseDto>(content, _jsonOptions);

            //     return result
            //         ?? new NvdApiResponseDto { Vulnerabilities = new List<NvdVulnerabilityDto>() };
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"NVD API error searching for CVE {cveId}: {ex.Message}");
            //     throw;
            // }
        }

        public async Task<NvdApiResponseDto> SearchVulnerabilitiesByKeywordAsync(
            string keyword,
            int resultsPerPage = 20,
            int startIndex = 0
        )
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new NvdApiResponseDto { Vulnerabilities = new List<NvdVulnerabilityDto>() };
            }

            try
            {
                var encodedKeyword = Uri.EscapeDataString(keyword);
                var url =
                    $"{BaseUrl}?keywordSearch={encodedKeyword}&resultsPerPage={resultsPerPage}&startIndex={startIndex}";

                Console.WriteLine($"Making NVD API request to: {url}");

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(
                    $"NVD API Response: {content.Substring(0, Math.Min(500, content.Length))}"
                );

                var result = JsonSerializer.Deserialize<NvdApiResponseDto>(content, _jsonOptions);

                return result
                    ?? new NvdApiResponseDto { Vulnerabilities = new List<NvdVulnerabilityDto>() };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NVD API error: {ex.Message}");
                throw;
            }
        }

        public async Task<NvdApiResponseDto> SearchVulnerabilitiesByOsAsync(
            string osName,
            string osVersion,
            int resultsPerPage = 20,
            int startIndex = 0
        )
        {
            // Try multiple search strategies
            try
            {
                // Strategy 1: Direct keyword search
                var keyword = "ubuntu 18.04 vulnerability";
                var result = await SearchVulnerabilitiesByKeywordAsync(
                    keyword,
                    resultsPerPage,
                    startIndex
                );

                // If no results, try second strategy
                if (result.Vulnerabilities.Count == 0)
                {
                    Console.WriteLine("Trying secondary search strategy");
                    keyword = "CVE ubuntu bionic";
                    result = await SearchVulnerabilitiesByKeywordAsync(
                        keyword,
                        resultsPerPage,
                        startIndex
                    );
                }

                // If still no results, try specific CVEs
                if (result.Vulnerabilities.Count == 0)
                {
                    Console.WriteLine("Trying specific CVE lookup");
                    var cveResult = await SearchVulnerabilitiesByCveIdAsync("CVE-2021-3156");
                    if (cveResult.Vulnerabilities.Count > 0)
                    {
                        return cveResult;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in search: {ex.Message}");
                throw;
            }

            // public async Task<NvdApiResponseDto> SearchVulnerabilitiesByOsAsync(
            //     string osName,
            //     string osVersion,
            //     int resultsPerPage = 20,
            //     int startIndex = 0
            // )
            // {
            //     if (string.IsNullOrWhiteSpace(osName))
            //     {
            //         return new NvdApiResponseDto { Vulnerabilities = new List<NvdVulnerabilityDto>() };
            //     }

            //     try
            //     {
            //         // Create a more specific search query
            //         //var keyword = $"{osName} {osVersion}".Trim();
            //         var keyword = "ubuntu 18.04 vulnerability";
            //         return await SearchVulnerabilitiesByKeywordAsync(
            //             keyword,
            //             resultsPerPage,
            //             startIndex
            //         );
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine($"NVD API error: {ex.Message}");
            //         throw;
            //     }
        }
    }
}
