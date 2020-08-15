using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.Services.Marketing.Entities;
using GloboTicket.Services.Marketing.Repositories;
using GloboTicket.Services.Marketing.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GloboTicket.Services.Marketing.Worker
{
    public class TimedBasketChangeEventService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IBasketChangeEventService basketChangeEventService;
        private readonly IBasketChangeEventRepository basketChangeEventRepository;
        private readonly IMapper mapper;
        private DateTimeOffset lastRun;

        public TimedBasketChangeEventService(IBasketChangeEventService basketChangeEventService, IBasketChangeEventRepository basketChangeEventRepository, IMapper mapper)
        {
            this.basketChangeEventService = basketChangeEventService;
            this.basketChangeEventRepository = basketChangeEventRepository;
            this.mapper = mapper;
            lastRun = DateTimeOffset.Now;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var events = await basketChangeEventService.GetBasketChangeEvents(lastRun, 100);
            foreach (var basketChangeEvent in events)
            {
                await basketChangeEventRepository.AddBasketChangeEvent(mapper.Map<BasketChangeEvent>(basketChangeEvent));
            }
            lastRun = DateTimeOffset.Now;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}