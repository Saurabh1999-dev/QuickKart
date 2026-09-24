using System;
using System.Collections.Generic;
using System.Text;

namespace QuickKart.Application.DTOs
{
    public class WorkerRegisterRequest
    {
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone {  get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public string ServiceCategory {  get; set; } = string.Empty;
        public string Experience {  get; set; } = string.Empty;
        public string ServiceArea {  get; set; } = string.Empty;
    }
}
