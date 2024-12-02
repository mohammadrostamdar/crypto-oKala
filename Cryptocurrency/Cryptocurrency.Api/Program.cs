using Cryptocurrency.Api.Infra.Jobs;
using Cryptocurrency.Application.Configurations;
using Cryptocurrency.Application.ExternalServices;
using Cryptocurrency.Application.QueryServices;
using Cryptocurrency.Application.Usecases.Qoutes.GetQuotes;
using Cryptocurrency.Domain.Entities.SymbolEntity.Services;
using Cryptocurrency.Infrastructure.ExternalServices;
using Cryptocurrency.Infrastructure.QueryServices;
using Cryptocurrency.Infrastructure.Repositories.SymbolRepositories;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();


var currencyConfig = new CurrencyConfiguration();
builder.Configuration.Bind("CurrencyConfig", currencyConfig);
builder.Services.AddSingleton(currencyConfig);


var defaultCryptoConfig = new DefaultCryptoCurrencyConfiguration
{
    DefaultCryptocurrencyName = builder.Configuration.GetValue<string>("DefaultCryptoCurrency:DefaultCryptoCurrencyName")
};

builder.Services.AddSingleton(defaultCryptoConfig);

var assemblies = new[]
{
    typeof(GetQuoteRequestHandler).Assembly
};
builder.Services.AddMediatR(configs =>
{
    configs.RegisterServicesFromAssemblies(assemblies);
});

builder.Services.AddCors(p => p.AddPolicy("MyCORS", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var coinMarketApiConfiguration = new CoinMarketApiConfiguration();
builder.Configuration.Bind("CoinMarketClient", coinMarketApiConfiguration);
builder.Services.AddSingleton(coinMarketApiConfiguration);

builder.Services.AddMemoryCache();


builder.Services.AddTransient<ICryptocurrencyExternalService, CryptocurrencyExternalService>();
builder.Services.AddSingleton<ISymbolRepository, SymbolRepository>();
builder.Services.AddTransient<ISymbolQueryService, SymbolQueryService>();

builder.Services.AddQuartz(q =>
{
    // Just use the name of your job that you created in the Jobs folder.
    var jobKey = new JobKey("SendEmailJob");
    q.AddJob<StoreCryptoInfoJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("StoreCryptoInfoJob-trigger")
        //This Cron interval can be described as "run every minute" (when second is zero)
        .WithCronSchedule(builder.Configuration.GetValue<string>("StoreCryptoInfoJobCron"))
        
    );
    
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var schedulerFactory = scope.ServiceProvider.GetRequiredService<ISchedulerFactory>();
    var scheduler = await schedulerFactory.GetScheduler();
    await scheduler.Start();

    // Schedule the job immediately  
    await scheduler.ScheduleJob(JobBuilder.Create<StoreCryptoInfoJob>().Build(), TriggerBuilder.Create().StartNow().Build());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("MyCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();