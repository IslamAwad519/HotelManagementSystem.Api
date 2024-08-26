using AutoMapper;
using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.DTOs.Reservations;
using HotelManagementSystem.Api.Models;
using HotelManagementSystem.Api.Repository;
using HotelManagementSystem.Api.Services.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly PaymentService _paymentService;
    private readonly IMapper _mapper;

    public PaymentController(
        PaymentService paymentService, 
        IMapper mapper, 
        IRepository<Reservation> reservationRepository)
    {
        _paymentService = paymentService;
        _mapper = mapper;
        _reservationRepository = reservationRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> CreateOrUpdatePaymentIntent([FromQuery] int reservationId)
    {
        var reservation = await _reservationRepository.GetById(reservationId);

        if (reservation == null) return NotFound();

        var intent = await _paymentService.CreateOrUpdatePaymentIntent(reservation);

        if (intent == null) return BadRequest(new ProblemDetails { Title = "Problem creating payment intent" });

        reservation.PaymentIntentId = reservation.PaymentIntentId ?? intent.Id;
        //reservation.ClientSecret = reservation.ClientSecret ?? intent.ClientSecret;

       await _reservationRepository.Update(reservation);

        //var result = await _context.SaveChangesAsync() > 0;

        //if (!result)
        //{
        //    return BadRequest(new ProblemDetails { Title = "Problem updating basket with intent" });
        //}

        return _mapper.Map<ReservationDto>(reservation);
    }





}
