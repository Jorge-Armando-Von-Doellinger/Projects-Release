using OmniSphere.Payments.Application.DTOs;
using OmniSphere.Payments.Application.Interfaces.UseCases;
using OmniSphere.Payments.Application.Mappers;
using OmniSphere.Payments.Core.Enums;
using OmniSphere.Payments.Core.Interfaces.Repository;
using OmniSphere.Payments.Core.Interfaces.Services;

namespace OmniSphere.Payments.Application.UseCases;

public class PaymentUseCase : IPaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentTransactionService _transactionService;
    private readonly IPaymentService _service;
    private readonly PaymentMapper _mapper;

    public PaymentUseCase(IPaymentRepository paymentRepository,
        IPaymentTransactionService transactionService,
        IPaymentService service,
        PaymentMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _transactionService = transactionService;
        _service = service;
        _mapper = mapper;
    }
    public async Task AddPaymentAsync(PaymentDTO payment)
    {
        var entity = _mapper.MapToPayment(payment);
        try
        {
            await _transactionService.ExecuteAsync(async () =>
            {
                await _service.ExecutePaymentAsync(entity);
            });
        }
        catch
        {
            entity.Status = PaymentStatusEnum.Failed;
        }
        finally
        {
            await _paymentRepository.RegisterAsync(entity);
        }
    }

    public async Task DeletePaymentAsync(string paymentId, string accountId)
    {
        await _paymentRepository.DeleteAsync(new () {Id = paymentId, AccountId = accountId});
    }

    public async Task<PaymentWithStatusAndIdDTO> GetPaymentByIdAsync(string paymentId)
    {
        var entity = await _paymentRepository.GetByIdAsync(paymentId);
        var output = _mapper.MapToPaymentDTO(entity);
        return output;
    }

    public async Task<IEnumerable<PaymentWithStatusAndIdDTO>> GetPaymentsByAccountIdAsync(string accountId)
    {
        var entities = await _paymentRepository.GetByAccountIdAsync(accountId);
        var outputs = _mapper.MapToListPaymentDTO(entities);
        return outputs;
    }
}