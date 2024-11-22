using HMS.Notification.Core.Entity;
using HMS.Notification.Core.Interfaces.Repository;
using HMS.Notification.Infrastructure.Context;
using MongoDB.Driver;

namespace HMS.Notification.Infrastructure.Repository;

public sealed class NotificationRepository : INotificationRepository
{
    private readonly MongoContext _context;
    private readonly IMongoCollection<NotificationBase> _collection; 
    public NotificationRepository(MongoContext context)
    {
        _context = context;
        _collection = _context.GetNotificationCollection();
    }
    public async Task AddAsync(NotificationBase notification)
    {
        _context.AddOperation(new InsertOneModel<NotificationBase>(notification));
        await Task.CompletedTask;
    }

    public Task UpdateAsync(NotificationBase notification)
    {
        _context.AddOperation(new ReplaceOneModel<NotificationBase>(notification.Id, notification));
        return Task.CompletedTask;  
    }

    public Task DeleteAsync(string id)
    {
        _context.AddOperation(new DeleteOneModel<NotificationBase>(id));
        return Task.CompletedTask;  
    }

    public async Task<NotificationBase> GetByIdAsync(string id)
    {
        var doc = await _collection.FindAsync(notification => notification.Id == id);
        return await doc.FirstOrDefaultAsync();
    }

    public async Task<List<NotificationBase>> GetAllAsync()
    {
        var docs = await _collection.FindAsync(notification => true);
        return await docs.ToListAsync();
    }
}