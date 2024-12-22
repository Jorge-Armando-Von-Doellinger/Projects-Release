using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Mapper;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Core.Interfaces.Internal_Services;
using HMS.Payments.Core.Interfaces.Repository;

namespace HMS.Payments.Application.UseCases;

public sealed class RefundUseCase : IRefundUseCase
{
    private readonly RefundMapper _mapper;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentService _paymentService;
    private readonly IRefundRepository _repository;

    public RefundUseCase(IRefundRepository repository,
        IPaymentRepository paymentRepository,
        IPaymentService paymentService,
        RefundMapper mapper)
    {
        _repository = repository;
        _paymentRepository = paymentRepository;
        _paymentService = paymentService;
        _mapper = mapper;
    }

    public async Task AddAsync(RefundModel refund)
    {
        var entity = _mapper.Map(refund);
        await _repository.AddAsync(entity);
    }

    public async Task AddBatchAsync(IEnumerable<RefundModel> refunds)
    {
        var entities = _mapper.Map(refunds);
        var payments = await _paymentRepository.GetAllAsync();
        var paymentsToRefund = payments.Where(x => payments.Contains(x));
        await _paymentService.TryRefundPaymentBatchAsync(paymentsToRefund);
        await _repository.AddBatchAsync(entities);
    }
}