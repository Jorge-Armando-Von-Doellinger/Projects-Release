using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Mapper;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Core.Exceptions;
using HMS.Payments.Core.Interfaces.Internal_Services;
using HMS.Payments.Core.Interfaces.Repository;

namespace HMS.Payments.Application.UseCases;

public sealed class PaymentEmployeeUseCase : IPaymentEmployeeUseCase
{
    private readonly EmployeePaymentMapper _mapper;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentEmployeeRepository _repository;

    public PaymentEmployeeUseCase(IPaymentEmployeeRepository repository, EmployeePaymentMapper mapper,
        IPaymentService paymentService)
    {
        _repository = repository;
        _mapper = mapper;
        _paymentService = paymentService;
    }

    public async Task AddAsync(PaymentEmployeeModel input)
    {
        var entity = _mapper.Map(input);
        entity.ValidateEntity();
        var success = await _paymentService.TryProcessPayment(entity);
        await _repository.AddAsync(entity);
        if (!success) throw new PaymentInvalidException("Não foi possivel realizar o pagamento!");
    }

    public async Task AddManyAsync(List<PaymentEmployeeModel> input)
    {
        var entities = await _mapper.Map(input);
        await _repository.AddManyAsync(entities);
    }

    public async Task<List<PaymentEmployeeOutput>> GetAllAsync()
    {
        var payments = await _repository.GetAllAsync();
        var output = _mapper.Map(payments);
        return output;
    }

    public async Task<List<PaymentEmployeeOutput>> GetByEmployeeIdAsync(string employeeId)
    {
        var payments = await _repository.GetByEmployeeId(employeeId);
        var output = _mapper.Map(payments);
        return output;
    }

    public async Task<PaymentEmployeeOutput> GetByIdAsync(string id)
    {
        var payment = await _repository.GetByIdAsync(id);
        var output = _mapper.Map(payment);
        return output;
    }
}