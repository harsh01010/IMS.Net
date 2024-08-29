using IMS.API.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace IMS.API
{
    public static  class DependencyInjection
    {

        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = JobKey.Create(nameof(ProductQuantityCheckJob));
                options.
                AddJob<ProductQuantityCheckJob>(jobKey)
                .AddTrigger(trigger =>
                trigger
                 .ForJob(jobKey)
                 .WithSimpleSchedule(schedule =>
                 schedule.WithIntervalInMinutes(5).RepeatForever()
                 ));
            });
            services.AddQuartzHostedService(options =>
              options.WaitForJobsToComplete = true
            );
        }
    }
}
    