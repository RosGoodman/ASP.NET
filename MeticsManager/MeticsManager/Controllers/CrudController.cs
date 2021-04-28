using MeticsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MeticsManager.Controllers
{
    [ApiController]
    [Route("api")]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;
        private WeatherController _controller;

        public CrudController(ValuesHolder holder)
        {
            this._holder = holder;
            _controller = new WeatherController(_holder.Weather);
        }

        [HttpPost("set")]
        public IActionResult Create([FromQuery] int temperatureC, [FromQuery] DateTime date)
        {
            WeatherInitializer.Seed(_holder);

            return Ok();
        }

        [HttpGet("get")]
        public IActionResult Read([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            List<WeatherModel> period = _controller.GetWeatherList(startDate, endDate);

            if (period.Count == 0)
                return NoContent();
            else
                return Ok(period);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime dateToUpdate, [FromQuery] int newTemperatureC)
        {
            if (!_controller.ChangeTemperature(dateToUpdate, newTemperatureC))
                NotFound();

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime date)
        {
            _controller.DeleteData(date);
            return Ok();
        }
    }
}
