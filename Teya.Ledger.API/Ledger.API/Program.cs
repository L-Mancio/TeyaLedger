using Ledger.Application.Features.Deposit;
using Ledger.Application.Features.GetCurrentBalance;
using Ledger.Application.Features.GetTransactions;
using Ledger.Application.Features.Withdraw;
using Ledger.Application.Services;
using Ledger.Application.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using FluentValidation;
using Ledger.Application.Features.GetTransactions.Validation;
using Ledger.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionsRequestValidator>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Ledger API", Version = "v1" });
});

builder.Services.AddSingleton<ILedgerService, LedgerService>();
builder.Services.AddSingleton<GetBalanceQueryHandler>();
builder.Services.AddSingleton<TransactionsQueryHandler>();
builder.Services.AddSingleton<DepositQueryHandler>();
builder.Services.AddSingleton<WithdrawQueryHandler>();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
