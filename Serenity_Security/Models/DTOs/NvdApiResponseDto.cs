using System.Text.Json.Serialization;

namespace Serenity_Security.Models.DTOs
{
    public class NvdApiResponseDto
    {
        public int ResultsPerPage { get; set; }
        public int StartIndex { get; set; }
        public int TotalResults { get; set; }
        public List<NvdVulnerabilityDto> Vulnerabilities { get; set; } =
            new List<NvdVulnerabilityDto>();
    }

    public class NvdVulnerabilityDto
    {
        // public object Cve { get; set; }
        [JsonPropertyName("cve")]
        public VulnerabilityDto Vulnerability { get; set; }
    }

    public class VulnerabilityDto
    {
        public string Id { get; set; }

        [JsonPropertyName("descriptions")]
        public List<DescriptionEntry> Descriptions { get; set; } = new List<DescriptionEntry>();
        public MetricsDto Metrics { get; set; }

        [JsonPropertyName("references")]
        public List<ReferenceDto> References { get; set; } = new List<ReferenceDto>();
        public string Published { get; set; }
    }

    public class DescriptionEntry
    {
        public string Lang { get; set; }
        public string Value { get; set; }
    }

    public class MetricsDto
    {
        public List<CvssDataV3Dto> CvssMetricV31 { get; set; } = new List<CvssDataV3Dto>();
    }

    public class CvssDataV3Dto
    {
        public string Type { get; set; }
        public CvssDataV3ScoreDto CvssData { get; set; }
    }

    public class CvssDataV3ScoreDto
    {
        public decimal BaseScore { get; set; }
        public string BaseSeverity { get; set; }
    }

    public class ReferenceDto
    {
        public string Url { get; set; }
        public string Source { get; set; }
    }
}
