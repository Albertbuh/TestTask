﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращать пользователя с максимальной общей суммой товаров, доставленных в 2003
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetUser()
        {
            var user = await _context.Users.OrderByDescending(
                u => _context.Orders
                            .Where(o => u.Id == o.UserId && o.Status == OrderStatus.Delivered && o.CreatedAt.Year == 2003)
                            .Sum(o => o.Quantity * o.Price)
                )
                .FirstAsync();

            if (user == null)
                throw new InvalidOperationException("No user with goods delivered in 2003");

            return user;
        }

        /// <summary>
        /// Возвращать пользователей у которых есть оплаченные заказы в 2010
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<User>> GetUsers()
        {
            var users = _context.Users.Where(u => _context.Orders.Where(o => o.CreatedAt.Year == 2010 && o.Status == OrderStatus.Paid).GroupBy(o => o.UserId).Count() > 0);

            if (users == null)
                throw new InvalidOperationException("No users with paid orders in 2010");

            return await users.ToListAsync();
        }
    }
}