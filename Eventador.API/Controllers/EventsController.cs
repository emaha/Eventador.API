﻿using Eventador.API.Models;
using Eventador.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Eventador.Domain;
using Eventador.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер событий
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EventsController : BaseController
    {
        private readonly IEventService _eventService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="eventService"></param>
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Получить по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<EventModel> GetById(long id)
        {
            var evnt = await _eventService.GetById(id);
            var model = EventModel.Create(evnt);

            return model;
        }

        /// <summary>
        /// Получить события в регионе (рядом)
        /// </summary>
        /// <returns></returns>
        [HttpGet("Region/{id}")]
        public async Task<SmallEventModel[]> GetEventsByRegion(long id)
        {
            var events = await _eventService.GetByRegion(id);

            Console.WriteLine($"Name: {Username}\tID: {UserId}");

            return events.Select(SmallEventModel.Create).ToArray();
        }

        /// <summary>
        /// Получить события по поисковому запросу
        /// </summary>
        /// <returns></returns>
        [HttpGet("Request")]
        public async Task<SmallEventModel[]> GetEventsBySearchRequest(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
            {
                var fakeResult = await _eventService.GetByRegion(1);
                return fakeResult.Select(SmallEventModel.Create).ToArray();
            }

            var events = await _eventService.GetBySearchRequest(request);
            return events.Select(SmallEventModel.Create).ToArray();
        }

        /// <summary>
        /// Получить события определённого пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("Author/{id}")]
        public async Task<SmallEventModel[]> GetEventsByAuthor(long id)
        {
            var events = await _eventService.GetByAuthorId(id);

            return events.Select(SmallEventModel.Create).ToArray();
        }

        /// <summary>
        /// Создание события
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> CreateEvent(EventCreateRequest request)
        {
            var createdEvent = Event.Create(request);
            createdEvent.AuthorId = UserId;
            await _eventService.Add(createdEvent);

            return createdEvent.Id;
        }

        /// <summary>
        /// Изменение события
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventUpdateRequest request)
        {
            var evnt = await _eventService.GetById(request.Id);
            if (evnt == null) return NotFound();

            evnt.Update(request);

            await _eventService.Update(evnt);

            return Ok();
        }

        /// <summary>
        /// Удаление события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(long id)
        {
            await _eventService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Активация события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/Activate")]
        public async Task<IActionResult> ActivateEvent(long id)
        {
            var evnt = await _eventService.GetById(id);
            if (evnt == null) return NotFound();

            evnt.Activate();
            await _eventService.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Завершение события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/Finish")]
        public async Task<IActionResult> FinishEvent(long id)
        {
            var evnt = await _eventService.GetById(id);
            if (evnt == null) return NotFound();

            evnt.Finish();
            await _eventService.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Приостановка события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/Suspend")]
        public async Task<IActionResult> SuspendEvent(long id)
        {
            var evnt = await _eventService.GetById(id);
            if (evnt == null) return NotFound();

            evnt.Suspend();
            await _eventService.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Отмена события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/Cancel")]
        public async Task<IActionResult> CancelEvent(long id)
        {
            var evnt = await _eventService.GetById(id);
            if (evnt == null) return NotFound();

            evnt.Cancel();
            await _eventService.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Заблокировать событие
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Block")]
        public async Task<IActionResult> BlockEvent(long id)
        {
            return Ok();
        }

        /// <summary>
        /// Разблокировать событие
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Unblock")]
        public async Task<IActionResult> UnblockEvent(long id)
        {
            return Ok();
        }
    }
}
