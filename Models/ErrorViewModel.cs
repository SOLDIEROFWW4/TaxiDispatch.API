﻿namespace TaxiDispatch.API.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
