namespace Group14.Rambo.Api.Extensions
{
    using Microsoft.AspNetCore.Builder;  
    using Microsoft.Extensions.DependencyInjection;  
    using Jobs;
    using Constants;
    using Quartz;  
    using Quartz.Impl;  
    using Quartz.Spi;  
    using System;  

    public static class QuartzExtensions  
    {  
        public static void AddQuartz(this IServiceCollection services, Type jobType)  
        {  
            services.Add(new ServiceDescriptor(typeof(IJob), jobType, ServiceLifetime.Transient));  
            services.AddSingleton<IJobFactory, ScheduledJobFactory>();  

            services.AddSingleton<IJobDetail>(provider => JobBuilder.Create<AnalyzePlantNeeds>()  
                    .WithIdentity(JobConstants.AnalyzePlantNeedsJob, JobConstants.AnalyticJobsGroup)  
                    .Build());  
  
            services.AddSingleton<ITrigger>(provider =>  
            {  
                return TriggerBuilder.Create()  
                    .WithIdentity(JobConstants.AnalyticJobsTrigger, JobConstants.AnalyticJobsGroup)  
                    .StartNow()  
                    .WithSimpleSchedule  
                    (s =>  
                        s.WithInterval(TimeSpan.FromSeconds(20))  
                            .RepeatForever()  
                    )  
                    .Build();  
            });  
  
            services.AddSingleton<IScheduler>(provider =>  
            {  
                var schedulerFactory = new StdSchedulerFactory();  
                var scheduler = schedulerFactory.GetScheduler().Result;  
                scheduler.JobFactory = provider.GetService<IJobFactory>();  
                scheduler.Start();  
                return scheduler;  
            });  
  
        }  
  
        public static void UseQuartz (this IApplicationBuilder app)  
        {  
            app.ApplicationServices.GetService<IScheduler>()  
                .ScheduleJob(app.ApplicationServices.GetService<IJobDetail>(),   
                    app.ApplicationServices.GetService<ITrigger>()  
                );  
        }  
    }  
}