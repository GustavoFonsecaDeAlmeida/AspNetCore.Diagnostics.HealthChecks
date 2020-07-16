using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HealthChecks.Publisher.Splunk.DependencyInjection
{
    public static class SplunkHealthCheckBuilderExtensions
{
    const string NAME = "seq";
    /// <summary>
    ///     Add a health check publisher for Seq.
    /// </summary>
    /// <remarks>
    ///     For each <see cref="HealthReport" /> published a new metric value indicating the health check status (2 Healthy, 1
    ///     Degraded, 0 Unhealthy)  and the total time the health check took to execute on seconds.
    /// </remarks>
    /// <param name="builder">The <see cref="IHealthChecksBuilder" />.</param>
    /// <param name="setup">The Sql configuration options.</param>
    /// <param name="name"> The registration name. This is also the associated http client name if you use AddHttpClient </param>
    /// <returns>The <see cref="IHealthChecksBuilder" />.</returns>
    public static IHealthChecksBuilder AddSplunkPublisher(this IHealthChecksBuilder builder, Action<SplunkOptions> setup, string name = default)
    {
        builder.Services.AddHttpClient();

        var options = new SplunkOptions();
        setup?.Invoke(options);

        var registrationName = name ?? NAME;

        builder.Services.AddSingleton<IHealthCheckPublisher>(sp =>
        {
            return new SplunkPublisher(() => sp.GetRequiredService<IHttpClientFactory>().CreateClient(registrationName), options);
        });

        return builder;
    }
}
}