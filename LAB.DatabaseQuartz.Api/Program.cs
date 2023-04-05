using LAB.DatabaseQuartz.Api.Api.BackgoundServices;
using LAB.DatabaseQuartz.Api.Domain.Services;
using LAB.DatabaseQuartz.Api.Domain.Services.Contracts;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Factories;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Repositories;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Repositories.Contracts;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Scheduler;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddTransient<IDomainService, DomainService>();

services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
services.AddSingleton<IJobFactory, JobFactory>();
services.AddSingleton<ICustomScheduler, CustomScheduler>();
services.AddSingleton<ConciliationJob>();
services.AddSingleton<EmailJob>();

services.AddTransient<IJobRepository, JobRepository>();

services.AddHostedService<QuartzBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();