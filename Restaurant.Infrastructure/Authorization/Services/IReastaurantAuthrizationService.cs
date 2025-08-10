﻿using Restaurant.Domain.Constents;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Authorization.Services
{
    public interface IReastaurantAuthrizationService
    {
        bool Authorize(Restaurant2 restaurant, ResourceOperation resource);
    }
}