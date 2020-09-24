using AutoMapper;
using GloboTicket.Services.Marketing.Entities;
using GloboTicket.Services.Marketing.Repositories;
using GloboTicket.Services.Marketing.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboTicket.Services.Marketing.Worker
{
    public class TimedBasketChangeEventService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IBasketChangeEventService basketChangeEventService;
        private readonly BasketChangeEventRepository basketChangeEventRepository;
        private readonly IMapper mapper;
        private DateTime lastRun;

        public TimedBasketChangeEventService(IBasketChangeEventService basketChangeEventService, BasketChangeEventRepository basketChangeEventRepository, IMapper mapper)
        {
            this.basketChangeEventService = basketChangeEventService;
            this.basketChangeEventRepository = basketChangeEventRepository;
            this.mapper = mapper;
            lastRun = DateTime.Now;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var events = await basketChangeEventService.GetBasketChangeEvents(lastRun, 10);
            foreach (var basketChangeEvent in events)
            {
                await basketChangeEventRepository.AddBasketChangeEvent(mapper.Map<BasketChangeEvent>(basketChangeEvent));
            }
            lastRun = DateTime.Now;
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