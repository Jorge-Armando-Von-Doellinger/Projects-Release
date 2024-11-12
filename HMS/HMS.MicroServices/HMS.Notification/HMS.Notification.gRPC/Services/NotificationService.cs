using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HMS.Notification.Application.Interfaces;
using HMS.Notification.gRPC.Mapper;
using HMS.Notification.gRPC.Protos;

namespace HMS.Notification.gRPC.Services
{
    public class NotificationService : SendNotificationService.SendNotificationServiceBase
    {
        private readonly INotificationManager _notificationManager;
        private readonly ProtoDtoMapper _protoDtoMapper;

        public NotificationService(INotificationManager notificationManager, ProtoDtoMapper protoDtoMapper)
        {
            _notificationManager = notificationManager;
            _protoDtoMapper = protoDtoMapper;
        }
        public override async Task<Empty> SendNotification(NotificationProtoDto request, ServerCallContext context)
        {
            var dto = _protoDtoMapper.MapToDto(request);
            await _notificationManager.SendAsync(dto, request.Email);
            return new Empty();
        }
    }
}
