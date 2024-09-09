@echo off
rmdir /s /q Migrations

dotnet ef migrations add First --context EmployeeContext
dotnet ef migrations add First --context ContractualInformationContext 
dotnet ef migrations add First --context PayrollContext 

dotnet ef database update --context EmployeeContext
dotnet ef database update --context ContractualInformationContext
dotnet ef database update --context PayrollContext

