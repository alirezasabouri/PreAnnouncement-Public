using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using PreAnnouncement.Domain;
using PreAnnouncement.Lambda.Common;
using PreAnnouncement.Misc;
using PreAnnouncement.Usecases;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreAnnouncement.Lambda
{
    public class PersistFunction
    {
        private ServiceCollection _serviceCollection;
        private PersistsPreAnnouncementUsecase _persistsRequestUsecase;
        private ILogger _logger;

        public PersistFunction(PersistsPreAnnouncementUsecase usecase, ILogger logger)
        {
            Setup(usecase, logger);
        }

        public PersistFunction()
        {
            ConfigureServices();
            using (var serviceProvider = _serviceCollection.BuildServiceProvider())
            {
                Setup(
                    serviceProvider.GetService<PersistsPreAnnouncementUsecase>(),
                    serviceProvider.GetService<ILogger>()
                    );
            }
        }

        public async Task<Result<IEnumerable<PreAnnouncementProcessingResult>>> Handler(IEnumerable<PreAnnouncementProcessingResult> preAnnouncementResults, ILambdaContext context)
        {
            _logger.Information("Request received in PreAnnouncement-Persist lambda with request {request}.", preAnnouncementResults);
            try
            {
                foreach (var preAnnouncement in preAnnouncementResults)
                {
                    await _persistsRequestUsecase.ExecuteAsync(preAnnouncement);
                }

                return Result.Success(preAnnouncementResults);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "An error occurred while trying to process PreAnnouncement-Persist request");
                return Result<IEnumerable<PreAnnouncementProcessingResult>>.Fail(exception);
            }
        }

        private void Setup(PersistsPreAnnouncementUsecase usecase, ILogger logger)
        {
            _persistsRequestUsecase = usecase;
            _logger = logger;
        }

        private void ConfigureServices()
        {
            _serviceCollection = new ServiceCollection();

            //register adapters

            _serviceCollection.AddCommonServices();
            _serviceCollection.AddTransient<PersistsPreAnnouncementUsecase>();
        }


    }
}
