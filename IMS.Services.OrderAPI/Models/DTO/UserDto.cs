﻿namespace IMS.Services.OrderAPI.Models.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email {  get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
