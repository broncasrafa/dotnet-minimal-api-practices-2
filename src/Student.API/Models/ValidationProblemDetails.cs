using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Student.API.Models;

/// <summary>
/// Classe apenas para organizar no retorno do json, onde os erros vem por último
/// </summary>
public class ValidationProblemDetails : ProblemDetails
{
    [JsonProperty(Order = 4)]
    public List<string> Errors { get; set; } = new List<string>();
}
