using MeticsManager.Models;
using System;
using System.Collections.Generic;

namespace MeticsManager
{
    public class WeatherController
    {
        private List<WeatherModel> _weatherDiary = new List<WeatherModel>();

        public WeatherController(List<WeatherModel> weatherDiary)
        {
            _weatherDiary = weatherDiary;
        }

        /// <summary>Добавить данные за новый день.</summary>
        /// <param name="temperatureC">Температура С.</param>
        /// <param name="date">Дата.</param>
        public void AddNewWeather(int temperatureC, DateTime date)
        {
            WeatherModel newDay = new WeatherModel(temperatureC, date);
            if(_weatherDiary.Count != 0)
            {
                foreach(WeatherModel day in _weatherDiary)
                {
                    if(day.Date == date)
                        throw new Exception("Данные за эту дату уже внесены.");
                }
            }

            _weatherDiary.Add(newDay);
        }

        /// <summary>Получить список погодных данных за промежуток.</summary>
        /// <param name="startDate">Дата начала.</param>
        /// <param name="endDate">Дата окончания.</param>
        /// <returns>Список с результатами поиска.</returns>
        public List<WeatherModel> GetWeatherList(DateTime startDate, DateTime endDate)
        {
            List<WeatherModel> list = new List<WeatherModel>();
            foreach(WeatherModel day in _weatherDiary)
            {
                if (day.Date >= startDate && day.Date <= endDate)
                    list.Add(day);
            }
            return list;
        }

        /// <summary>Изменить данные в заданную дату.</summary>
        /// <param name="dateToUpdate">Дата изменения.</param>
        /// <param name="newTemperatureC">Новое значение температуры.</param>
        /// <returns></returns>
        public bool ChangeTemperature(DateTime dateToUpdate, int newTemperatureC)
        {
            foreach (WeatherModel day in _weatherDiary)
            {
                if (day.Date >= dateToUpdate)
                {
                    day.TemperatureC = newTemperatureC;
                    return true;
                }
            }
            return false;
        }

        /// <summary>Удалить данные указанной даты.</summary>
        /// <param name="deletingDate">Удаляемая дата.</param>
        public void DeleteData(DateTime deletingDate)
        {
            foreach (WeatherModel day in _weatherDiary)
            {
                if (day.Date == deletingDate)
                {
                    _weatherDiary.Remove(day);
                    return;
                }
            }
        }
    }
}
