using Newtonsoft.Json;

namespace Student.API.Models;

/// <summary>
/// Classe padrão para retorno dos dados da api
/// </summary>
/// <typeparam name="T">objeto para retornar</typeparam>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
internal class ApiResult<T>
{
    private readonly T _value;

    [JsonProperty(Order = -3)]
    public bool Succeeded { get; }

    [JsonProperty(Order = -2)]
    public T Data
    {
        get
        {
            return _value!;
        }

        private init => _value = value;
    }

    [JsonProperty(Order = -1)]
    public List<string> Errors { get; }


    private ApiResult(T value)
    {
        Data = value;
        Succeeded = true;
        Errors = null;
    }
    private ApiResult(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            throw new ArgumentException("Invalid error", nameof(errorMessage));

        Succeeded = false;
        Errors = new List<string> { errorMessage };
    }

    public static ApiResult<T> Success(T value) => new ApiResult<T>(value);
    public static ApiResult<T> Failure(string error) => new ApiResult<T>(error);
}
