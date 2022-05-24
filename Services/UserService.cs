using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.Data;
using TestBot.Models;

namespace TestBot.Services;
class UserService
{
    private readonly BotContext _context;
    public UserService(BotContext context)
    {
        _context = context;
    }

    public async Task<User?> GetById(ulong id)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> AddUser(ulong id)
    {
        var user = new User { Id = id };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task AddExperience(ulong id, byte exp)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is null)
            return;

        user.Experience += exp;

        await _context.SaveChangesAsync();
    }
}
