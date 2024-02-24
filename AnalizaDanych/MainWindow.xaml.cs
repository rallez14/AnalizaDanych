using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using AnalizaDanych.Model;
using System.Collections.Generic;
using AnalizaDanych.Model;

namespace AnalizaDanych
{
    public partial class MainWindow : Window
    {
        private Root _data;
        private Dictionary<string, string> _dataTypes = new Dictionary<string, string>
        {
            {"total_cases", "Całkowita liczba przypadków"},
            {"new_cases", "Nowe przypadki"},
            {"total_deaths", "Całkowita liczba zgonów"},
            {"new_deaths", "Nowe zgony"},
            {"total_cases_per_million", "Całkowita liczba przypadków na milion"},
            {"new_cases_per_million", "Nowe przypadki na milion"},
            {"total_deaths_per_million", "Całkowita liczba zgonów na milion"},
            {"new_deaths_per_million", "Nowe zgony na milion"},
            {"reproduction_rate", "Wskaźnik reprodukcji"},
            {"positive_rate", "Wskaźnik pozytywnych testów"},
            {"tests_per_case", "Testy na przypadek"},
            {"people_vaccinated", "Zaszczepione osoby"},
            {"people_fully_vaccinated", "W pełni zaszczepione osoby"},
            {"people_vaccinated_per_hundred", "Zaszczepione osoby na sto"},
            {"people_fully_vaccinated_per_hundred", "W pełni zaszczepione osoby na sto"},
            {"stringency_index", "Indeks rygorystyczności"},
            {"hosp_patients", "Pacjenci w szpitalach"},
            {"hosp_patients_per_million", "Pacjenci w szpitalach na milion"},
            {"total_tests", "Całkowita liczba testów"},
            {"total_tests_per_thousand", "Całkowita liczba testów na tysiąc"},
            {"excess_mortality_cumulative_absolute", "Nadmierna śmiertelność kumulatywna absolutna"},
            {"excess_mortality_cumulative", "Nadmierna śmiertelność kumulatywna"},
            {"excess_mortality", "Nadmierna śmiertelność"},
            {"excess_mortality_cumulative_per_million", "Nadmierna śmiertelność kumulatywna na milion"}
        };


        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            InitializeUI();
        }

        private void LoadData()
        {
            DataService dataService = new DataService();
            _data = dataService.LoadData(@"C:\Users\rallez\Desktop\covid-data.json");
        }


        private void InitializeUI()
        {
            dataTypeComboBox.ItemsSource = _dataTypes.Values;
            startDatePicker.SelectedDate = _data.POL.data.Min(d => DateTime.Parse(d.date));
            endDatePicker.SelectedDate = _data.POL.data.Max(d => DateTime.Parse(d.date));
        }

        private void LoadChartData(object sender, RoutedEventArgs e)
        {
            if (dataTypeComboBox.SelectedItem == null || startDatePicker.SelectedDate == null || endDatePicker.SelectedDate == null) return;

            var selectedDataTypeKey = _dataTypes.FirstOrDefault(x => x.Value == dataTypeComboBox.SelectedItem.ToString()).Key;
            var startDate = startDatePicker.SelectedDate.Value;
            var endDate = endDatePicker.SelectedDate.Value;

            var filteredData = _data.POL.data
                .Where(d => DateTime.Parse(d.date) >= startDate && DateTime.Parse(d.date) <= endDate)
                .ToList();

            SeriesCollection series = new SeriesCollection();
            ChartValues<double> values = new ChartValues<double>();
            List<string> labels = new List<string>();

            foreach (var datum in filteredData)
            {
                var value = (double)typeof(Datum).GetProperty(selectedDataTypeKey).GetValue(datum);
                if (value > 0)
                {
                    values.Add(value);
                    labels.Add(DateTime.Parse(datum.date).ToString("yyyy-MM-dd"));
                }
            }

            if (values.Any())
            {
                series.Add(new LineSeries
                {
                    Title = _dataTypes[selectedDataTypeKey],
                    Values = values
                });

                covidChart.Series = series;
                covidChart.AxisX.Clear();
                covidChart.AxisX.Add(new Axis
                {
                    Labels = labels,
                    LabelsRotation = 45
                });

                covidChart.LegendLocation = LegendLocation.Right;
            }
            else
            {
                MessageBox.Show("Brak danych większych niż 0 dla wybranych kryteriów.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
