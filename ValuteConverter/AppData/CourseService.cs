using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace ValuteConverter.AppData
{
    internal class CourseService
    {
        private ComboBox _sellValuteComboBox;
        private ComboBox _buyValuteComboBox;
        private TextBox _sellValuteTextBox;
        private TextBox _buyValuteTextBox;
        private TextBlock _sellRatioTextBlock;
        private TextBlock _buyRatioTextBlock;
        private TextBlock _updateDateTextBlock;

        public CourseService(ComboBox sellValuteComboBox, ComboBox buyValuteComboBox, TextBox sellValuteTextBox, TextBox buyValuteTextBox, TextBlock sellRatioTextBlock, TextBlock buyRatioTextBlock, TextBlock updateDateTextBlock)
        {
            _sellValuteComboBox = sellValuteComboBox;
            _buyValuteComboBox = buyValuteComboBox;
            _sellValuteTextBox = sellValuteTextBox;
            _buyValuteTextBox = buyValuteTextBox;
            _sellRatioTextBlock = sellRatioTextBlock;
            _buyRatioTextBlock = buyRatioTextBlock;
            _updateDateTextBlock = updateDateTextBlock;
        }

        public string RequestUrl { get; private set; } = "https://www.cbr-xml-daily.ru/daily_json.js";
        public Valute SellValute { get; private set; }
        public Valute BuyValute { get; private set; }
        public double SellAmount { get; private set; }
        public double BuyAmount { get; private set; }
        public double SellRatio { get; private set; }
        public double BuyRatio { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public Course Course { get; private set; }
        public List<Valute> Valutes { get; private set; }

        public void LoadCource()
        {
            try
            {
                // 1. Создаем HTTP-клиент для отправки запроса на веб-сервер
                HttpClient httpClient = new HttpClient();

                // 2. Создаем переменную для хранения ответа
                var response = httpClient.GetStringAsync(RequestUrl);

                // 3. Проводим десереализация ответа (из строки делаем объекты)
                // 3.1 Устанавливаем пакет Newtonsoft.Json
                // 3.2 Проверяем переменную response на наличие значения
                // 3.3 Вызываем метод DeserializeOblect() с указанием типа данных объекта 

                if (!string.IsNullOrEmpty(response.Result))
                {
                    // Получаем курс валют
                    Course = JsonConvert.DeserializeObject<Course>(response.Result);

                    // Получаем списки валют из курса
                    Valutes = Course.Valute.Values.ToList();

                    // Загружаем список валют в выпадающие списки
                    _sellValuteComboBox.ItemsSource = Valutes;
                    _sellValuteComboBox.ItemsSource = Valutes;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
