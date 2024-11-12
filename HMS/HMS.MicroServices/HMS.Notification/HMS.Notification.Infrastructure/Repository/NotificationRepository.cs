using HMS.Notification.Core.Entity;
using HMS.Notification.Core.Interfaces.Repository;
using HMS.Notification.Infrastructure.Context;
using MongoDB.Driver;

namespace HMS.Notification.Infrastructure.Repository;

public sealed class NotificationRepository : INotificationRepository
{
    private readonly MongoContext _context;
    private readonly IMongoCollection<NotificationEntity> _collection; 
    public NotificationRepository(MongoContext context)
    {
        _context = context;
        _collection = _context.GetNotificationCollection();
    }
    public async Task AddAsync(NotificationEntity notification)
    {
        _context.AddOperation(new InsertOneModel<NotificationEntity>(notification));
        await Task.CompletedTask;
    }

    public Task UpdateAsync(NotificationEntity notification)
    {
        _context.AddOperation(new ReplaceOneModel<NotificationEntity>(notification.Id, notification));
        return Task.CompletedTask;  
    }

    public Task DeleteAsync(string id)
    {
        _context.AddOperation(new DeleteOneModel<NotificationEntity>(id));
        return Task.CompletedTask;  
    }

    public async Task<NotificationEntity> GetByIdAsync(string id)
    {
        var doc = await _collection.FindAsync(notification => notification.Id == id);
        return await doc.FirstOrDefaultAsync();
    }

    public async Task<List<NotificationEntity>> GetAllAsync()
    {
        var docs = await _collection.FindAsync(notification => true);
        return await docs.ToListAsync();
    }
}