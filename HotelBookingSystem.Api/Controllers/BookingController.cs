﻿
using HotelBookingSystem.Application.DTO.BookingDTO;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Infrastructure.PdfGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingSystem.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = "AdminOrCustomer")]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IPdfService _pdfService;

        public BookingController(IBookingService bookingService, IPdfService pdfService)
        {
            _bookingService = bookingService;
            _pdfService = pdfService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest bookingRequest)
        {

            var result = await _bookingService.CreateBookingAsync(bookingRequest);
            return CreatedAtAction(nameof(CreateBooking), result);
        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] BookingRequest bookingRequest)
        {
            var result = await _bookingService.UpdateBookingAsync(bookingId, bookingRequest);
            return Ok(result);
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingDetails(int bookingId)
        {
            var result = await _bookingService.GetBookingDetailsAsync(bookingId);
            return Ok(result);
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            await _bookingService.CancelBookingAsync(bookingId);
            return NoContent();
        }

        [HttpGet("{bookingId}/pdf")]
        public async Task<IActionResult> GetBookingPdf(int bookingId)
        {
            var bookingPdf = await _bookingService.GetBookingPdfAsync(bookingId);
            return File(bookingPdf, "application/pdf", $"Booking_{bookingId}.pdf");
        }
    }
}

