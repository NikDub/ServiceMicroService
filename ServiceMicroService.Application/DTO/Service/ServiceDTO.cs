﻿namespace ServiceMicroService.Application.DTO.Service
{
    public class ServiceDTO
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SpecializationId { get; set; }
        public string SpecializationName { get; set; }
    }
}