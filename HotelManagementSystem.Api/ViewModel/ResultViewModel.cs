﻿namespace HotelManagementSystem.Api.ViewModel;

public class ResultViewModel<T>
{
   public bool IsSuccess { get; set; }
   public T Data { get; set; }
   public string Message { get; set; }
}

