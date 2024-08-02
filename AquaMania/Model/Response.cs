﻿namespace AquaMania.Model;

public class Response<T>
{
    public int? Status { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public string Error { get; set; }
}
