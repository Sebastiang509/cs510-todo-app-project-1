module Routes

open Giraffe
open WeatherService

let getWeatherHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let weatherJson = getWeatherData ()
        json weatherJson next ctx

let webApp =
    choose [
        GET >=> choose [
            route "/weather" >=> getWeatherHandler
        ]
    ]
