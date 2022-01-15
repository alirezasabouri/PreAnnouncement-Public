using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PreAnnouncement.Domain;
using PreAnnouncement.Lambda.Common;
using PreAnnouncement.Misc;
using PreAnnouncement.Usecases;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PreAnnouncement.Lambda
{
    public class ProcessFunction
    {
        private ServiceCollection _serviceCollection;
        private ProcessPreAnnounementUsecase _requestProcessorUsecase;
        private ILogger _logger;

        public ProcessFunction(ProcessPreAnnounementUsecase usecase, ILogger logger)
        {
            Setup(usecase, logger);
        }

        public ProcessFunction()
        {
            ConfigureServices();
            using (var serviceProvider = _serviceCollection.BuildServiceProvider())
            {
                Setup(
                    serviceProvider.GetService<ProcessPreAnnounementUsecase>(),
                    serviceProvider.GetService<ILogger>()
                    );
            }
        }

        public async Task<Result<ReadOnlyCollection<PreAnnouncementProcessingResult>>> Handler(IEnumerable<PreAnnouncementRequest> preAnnouncementRequests, ILambdaContext context)
        {
            _logger.Information("Request received in PreAnnouncement-Process lambda with request {request}.", preAnnouncementRequests);
            try
            {
                var response = new List<PreAnnouncementProcessingResult>();
                foreach (var preAnnouncement in preAnnouncementRequests)
                {
                    var result = await _requestProcessorUsecase.ExecuteAsync(preAnnouncement);
                    response.Add(result);
                }

                return Result.Success(response.AsReadOnly());
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "An error occurred while trying to process PreAnnouncement-Process request");
                return Result<ReadOnlyCollection<PreAnnouncementProcessingResult>>.Fail(exception);
            }
        }

        private void Setup(ProcessPreAnnounementUsecase usecase, ILogger logger)
        {
            _requestProcessorUsecase = usecase;
            _logger = logger;
        }

        private void ConfigureServices()
        {
            _serviceCollection = new ServiceCollection();

            //register adapters

            _serviceCollection.AddCommonServices();
            _serviceCollection.AddTransient<ProcessPreAnnounementUsecase>();
        }


    }
}
