namespace AnalizaDanych.Model;

public class Datum
{
    public string date { get; set; }
    public double total_cases { get; set; }
    public double new_cases { get; set; }
    public double total_deaths { get; set; }
    public double new_deaths { get; set; }
    public double total_cases_per_million { get; set; }
    public double new_cases_per_million { get; set; }
    public double total_deaths_per_million { get; set; }
    public double new_deaths_per_million { get; set; }
    public double reproduction_rate { get; set; }
    public double positive_rate { get; set; }
    public double tests_per_case { get; set; }
    public double people_vaccinated { get; set; }
    public double people_fully_vaccinated { get; set; }
    public double people_vaccinated_per_hundred { get; set; }
    public double people_fully_vaccinated_per_hundred { get; set; }
    public double stringency_index { get; set; }
    public double hosp_patients { get; set; }
    public double hosp_patients_per_million { get; set; }
    public double total_tests { get; set; }
    public double total_tests_per_thousand { get; set; }
    public double excess_mortality_cumulative_absolute { get; set; }
    public double excess_mortality_cumulative { get; set; }
    public double excess_mortality { get; set; }
    public double excess_mortality_cumulative_per_million { get; set; }
}
