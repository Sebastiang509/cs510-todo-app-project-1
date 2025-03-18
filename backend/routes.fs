module Routes

open Giraffe
open WeatherService

let getWeatherHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let weatherJson = getWeatherData ()
        json weatherJson next ctx

let apiRoutes =
    choose [
        route "/todos" >=> getTodosHandler
        route "/weather" >=> getWeatherHandler
    ]

let webApp = GET >=> apiRoutes
