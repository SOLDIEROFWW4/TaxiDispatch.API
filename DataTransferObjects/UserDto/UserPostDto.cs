﻿namespace TaxiDispatch.API.DataTransferObjects.UserDto
{
    public class UserPostDto
    {
        public string? Username { get; set; } = null!;

        public string? Password { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null!;
    }
}
