using Framework.Context;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Core;

public class OrderManagementContext:CoreDbContext
{
    public OrderManagementContext(DbContextOptions options) : base(options)
    {
    }
}