using HMS.Notification.Core.Entity;
using HMS.Notification.Core.Interfaces.Repository;
using HMS.Notification.Infrastructure.Context;
using MongoDB.Driver;

namespace HMS.Notification.Infrastructure.Repository;

public sealed class NotificationRepository : INotificationRepository
{
    private readonly MongoContext _context;
    public NotificationRepository(MongoContext context)
    {
        _context = context;
    }
    public async Task AddAsync(NotificationBase notification)
    {
        await _context.GetNotificationCollection().InsertOneAsync(notification);
    }

    public async Task UpdateAsync(NotificationBase notification)
    {
        await _context.GetNotificationCollection().ReplaceOneAsync(x => x.Id == notification.Id, notification);
    }

    public async Task DeleteAsync(string id)
    {
        await _context.GetNotificationCollection().DeleteOneAsync(x => x.Id == id);
    }

    public async Task<NotificationBase> GetByIdAsync(string id)
    {
        var doc = await _context.
            GetNotificationCollection()
            .FindAsync(notification => notification.Id == id);
        return await doc.FirstOrDefaultAsync();
    }

    public async Task<List<NotificationBase>> GetAllAsync()
    {
        var docs = await _context
            .GetNotificationCollection()
            .FindAsync(notification => true);
        return await docs.ToListAsync();
    }
}