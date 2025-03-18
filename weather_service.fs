module WeatherService

open System
open System.Net.Http
open Newtonsoft.Json.Linq
open System.IO
open System.Text

let weatherFilePath = "weather_data.json"

// Open-Meteo API URL with Andover, KS coordinates
let apiUrl = "https://api.open-meteo.com/v1/forecast?latitude=37.7139&longitude=-97.1364&current_weather=true"

let fetchWeatherData () =
    async {
        use client = new HttpClient()
        try
            let! response = client.GetStringAsync(apiUrl) |> Async.AwaitTask
            let weatherData = JObject.Parse(response)

            // Save weather data to file
            File.WriteAllText(weatherFilePath, weatherData.ToString(Newtonsoft.Json.Formatting.Indented), Encoding.UTF8)

            printfn "Weather data updated successfully!"
        with
        | ex -> printfn "Failed to fetch weather data: %s" ex.Message
    }

// Function to read saved weather data
let getWeatherData () =
    if File.Exists(weatherFilePath) then
        File.ReadAllText(weatherFilePath)
    else
        "{}" // Return empty JSON if no data is available
