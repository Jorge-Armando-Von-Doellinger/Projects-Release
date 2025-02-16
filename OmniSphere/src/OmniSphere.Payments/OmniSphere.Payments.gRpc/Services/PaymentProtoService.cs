using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OmniSphere.Payments.Application.Interfaces.UseCases;
using OmniSphere.Payments.gRpc.Mapper;

namespace OmniSphere.Payments.gRpc.Services;
 
public class PaymentProtoService : gRpc.PaymentProtoService.PaymentProtoServiceBase
{
    private readonly IPaymentUseCase _paymentUseCase;
    private readonly PaymentProtoMapper _mapper;

    public PaymentProtoService(IPaymentUseCase paymentUseCase,
        PaymentProtoMapper mapper)
    {
        _paymentUseCase = paymentUseCase;
        _mapper = mapper;
    }
    public override async Task<Empty> AddPayment(PaymentProto request, ServerCallContext context)
    {
        Console.WriteLine(request.AccountId);
        var dto = _mapper.MapToPaymentDTO(request);
        await _paymentUseCase.AddPaymentAsync(dto);
        return new Empty();
    }

    public override async Task<Empty> DeletePayment(PaymentIdAndAccountId request, ServerCallContext context)
    {
        await _paymentUseCase.DeletePaymentAsync(request.Id, request.AccountId);  
        return new Empty();
    }

    public override async Task<PaymentProto> GetPaymentById(PaymentId request, ServerCallContext context)
    {
        var dto = await _paymentUseCase.GetPaymentByIdAsync(request.Id);
        var proto = _mapper.MapToPaymentProto(dto);
        return proto;
    }

    public override async Task<ListPaymentsWithMessageProto> GetPaymentsByAccountId(AccountIdentifier request, ServerCallContext context)
    {
        var dtos = await _paymentUseCase.GetPaymentsByAccountIdAsync(request.AccountId);
        var protos = _mapper.MapToListPaymentDTOs(dtos);
        var protoList = _mapper.MapToListPaymentsWithMessageProto(protos);
        return protoList;
    }
}