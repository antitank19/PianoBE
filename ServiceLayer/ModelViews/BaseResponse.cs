using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using ServiceLayer.ModelViews.Enums;

namespace ServiceLayer.ModelViews;

public class BaseResponse<T>
{
    [JsonPropertyOrder(-2)]
    public string Message { get; set; } = "Sucessfull";

    [JsonPropertyOrder(-1)]
    public int StatusCode { get; set; } = StatusCodes.Status200OK;
    public T? Data { get; set; }
    public BaseResponse(string? message, int statusCode, T? data)
    {
        Message = message;
        StatusCode = statusCode;
        Data = data;
    }

    public BaseResponse(string? message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

}