﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpecRandomizer.Server.Models;
using SpecRandomizer.Server.Model;

public class ConfigurationService
{
    private readonly SpecRandomizerDbContext _context;

    public ConfigurationService(SpecRandomizerDbContext context)
    {
        _context = context;
    }


    public async Task<List<ConfigurationDto>> GetAllConfigurationsByUserIdAsync(int UserId)
    {
        return await _context.Configurations
    .Where(c => c.UserId == UserId)
    .Include(c => c.Players)  // Ensure players are included
    .Select(c => new ConfigurationDto
    {
        ConfigurationId = c.ConfigurationId,
        Players = c.Players.Select(p => new PlayerDto  // Convert Players to a clean list
        {
            PlayerId = p.PlayerId,
            PlayerName = p.PlayerName,
            SpecList = p.SpecList
        }).ToList()
    })
    .ToListAsync();
    }

    public async Task<Configuration?> GetConfigurationByIdAsync(int id)
    {
        return await _context.Configurations
            .Include(c => c.Players)
            .FirstOrDefaultAsync(c => c.ConfigurationId == id);
    }
    public async Task<Configuration?> GetConfigurationByNewestAsync()
    {
        return await _context.Configurations
            .OrderByDescending(c => c.CreatedAt)
            .Include(c => c.Players)
            .FirstAsync();
    }

    // Add a new configuration while ensuring a max limit of 10 per UserId
    public async Task<Configuration> AddConfigurationAsync(Configuration config)
    {
        var userConfigs = await _context.Configurations
            .Where(c => c.UserId == config.UserId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();

        if (userConfigs.Count >= 10)
        {
            _context.Configurations.Remove(userConfigs.First()); // Remove the oldest one
        }

        _context.Configurations.Add(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task<bool> DeleteConfigurationAsync(int id)
    {
        var config = await _context.Configurations.FindAsync(id);
        if (config == null) return false;

        _context.Configurations.Remove(config);
        await _context.SaveChangesAsync();
        return true;
    }
}
