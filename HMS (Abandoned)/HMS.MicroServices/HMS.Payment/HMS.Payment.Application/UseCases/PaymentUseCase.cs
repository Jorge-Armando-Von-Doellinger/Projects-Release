using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Mapper;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Exceptions;
using HMS.Payments.Core.Interfaces.Internal_Services;
using HMS.Payments.Core.Interfaces.Repository;

namespace HMS.Payments.Application.UseCases;

public sealed class PaymentUseCase : IPaymentUseCase
{
    private readonly PaymentMapper _mapper;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentRepository _repository;

    public PaymentUseCase(IPaymentRepository repository, IPaymentService paymentService, PaymentMapper mapper)
    {
        _repository = repository;
        _paymentService = paymentService;
        _mapper = mapper;
    }

    public async Task AddAsync(PaymentModel input)
    {
        var entity = _mapper.Map(input);
        entity.ValidateEntity();
        var success = await _paymentService.TryProcessPayment(entity);
        await _repository.AddAsync(entity);
        if (!success) throw new PaymentInvalidException("Não foi possivel realizar o pagamento!");
    }

    public async Task AddBatchAsync(IEnumerable<PaymentModel> input)
    {
        var entities = await _mapper.Map(input);
        await _repository.AddManyAsync(entities);
    }

    public async Task<List<PaymentOutput>> GetAllAsync()
    {
        var payments = await _repository.GetAllAsync();
        var output = _mapper.Map(payments);
        return output;
    }

    public async Task<PaymentOutput> GetByIdAsync(string id)
    {
        var payment = await _repository.GetByIdAsync(id);
        var output = _mapper.Map(payment);
        return output;
    }

    public async Task AddRefundAsync(PaymentUpdateModel input)
    {
        var entity = _mapper.Map(input);
        entity.ValidateEntity();
        await _repository.AddRefundAsync(entity);
    }

    public async Task AddRefundBatchAsync(List<PaymentModel> inputs)
    {
        var entities = await _mapper.Map(inputs);
        foreach (var entity in entities) entity.ValidateEntity();

        await _repository.AddManyRefundsAsync(entities);
    }
}