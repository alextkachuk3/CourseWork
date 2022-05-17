﻿namespace BookingWebService.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string? Login { get; set; } = string.Empty;

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public List<Hotel>? HotelList { get; set; }

    }
}
