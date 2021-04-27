using MeticsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeticsManager.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public CrudController(ValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] int temperatureC, DateTime date)
        {
            _holder.AddNewWeather(temperatureC, date);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime startDate, DateTime endDate)
        {
            List<WeatherModel> listResults = _holder.GetWeatherList(startDate, endDate);
            if (listResults.Count == 0) NotFound();
            return Ok(listResults);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime dateToUpdate, [FromQuery] int newTemperatureC)
        {
            if (!_holder.ChangeTemperature(dateToUpdate, newTemperatureC))
                NotFound();

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime date)
        {
            _holder.DeleteData(date);
            return Ok();
        }
    }
}
