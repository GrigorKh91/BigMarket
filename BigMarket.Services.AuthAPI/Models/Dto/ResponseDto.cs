﻿namespace BigMarket.Services.AuthAPI.Models.Dto
{
    public sealed class ResponseDto
    {
        public object Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}