using HMS.Employee.Core.Entity;
using Nuget.Employee.Inputs;

namespace HMS.Employee.Application.Mappers
{
    public static class EmployeeMapper
    {
        public async static Task<Core.Entity.Employee> Map(EmployeeInput inputModel)
        {
            return await Task.Run(() =>
            {
                return new Core.Entity.Employee()
                {
                    Name = inputModel.Name,
                    Age = inputModel.Age,
                    PIS = inputModel.PIS,
                    BirthDate = inputModel.BirthDate,
                    Email = inputModel.Email,
                    PhoneNumber = inputModel.PhoneNumber,
                    MaritalStatus = inputModel.MaritalStatus,
                    CPF = inputModel.CPF
                };
            });
        }
    }
}
