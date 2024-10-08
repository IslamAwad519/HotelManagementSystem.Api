﻿using HotelManagementSystem.Api.Models.Common;
using HotelManagementSystem.Api.Models.Enums;
using Stripe;

namespace HotelManagementSystem.Api.Models;

public class Reservation : BaseModel
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalAmount { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public int NumberAdults { get; set; }
    public int? NumberChildren { get; set; }
    public int CustomerId { get; set; }

    public string PaymentIntentId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<ReservationRoom> Rooms { get; set; }
    public ICollection<ReservationRoomFacility> RoomFacilities { get; set; }

}
