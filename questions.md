Q1.
  this application take 10-12 hours to implementation in this solution something like authorization,database(e.g SQL) can added for better security and better management.

Q2. Add quartz job into solution to avoid many api call requests
```
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("SendEmailJob");
    q.AddJob<StoreCryptoInfoJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("StoreCryptoInfoJob-trigger")
        .WithCronSchedule(builder.Configuration.GetValue<string>("StoreCryptoInfoJobCron"))
    );
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
```

Q3.
  For performance track on the production i do some actions like checking the dependency(services or databases) for its health. add log for monitor times elapsed.
  in OMS(order management service) for market data that most be live this actions performed that contains refactoring,change database , etc

Q4.
  "C# in a nutshell"
    this book is about last changes in c#

Q5.
  it's very good but if i could connect to a technical guy this assessment it's be better.

Q5.
  {
    "name":"Mohammad Rostamdar",
    "Mobile":"09387050198",
    "Sex":"Male",
    "Hubbies: [
      "Footbal",
      "Movie",
      "Music"
      ]
  }
