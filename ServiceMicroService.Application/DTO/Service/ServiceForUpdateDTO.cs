﻿namespace ServiceMicroService.Application.DTO.Service
{
    public class ServiceForUpdateDTO
    {
        public string ServiceName { get; set; }
        public float Price { get; set; }

        public string CategoryName { get; set; }
        public string SpecializationName { get; set; }
    }
}
