﻿using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class Service
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public decimal Price { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = [];
}
