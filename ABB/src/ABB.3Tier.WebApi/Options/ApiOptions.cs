using System.ComponentModel.DataAnnotations;

namespace ABB.NTier.WebApi.Options
{
    public class ApiOptions
    {
        [Required] public string Url { get; set; }
    }
}