﻿namespace net_moto_bot.Application.Dtos.Custom;

public class CategoryDTO
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public string? Logo { get; set; }

    public bool Active { get; set; }
}
