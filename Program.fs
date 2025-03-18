open System
open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Newtonsoft.Json.Linq
open System.Net.Http

let mutable todos = ["Finish CS 510 project"; "Submit assignment"]

let getTodosHandler = fun (next : HttpFunc) (ctx : HttpContext) ->
    json todos next ctx

let addTodoHandler = fun (next : HttpFunc) (ctx : HttpContext) ->
    task {
        let! todo = ctx.BindJsonAsync<string>()
        todos <- todo :: todos
        return! json todos next ctx
    }

let getWeatherHandler = fun (next : HttpFunc) (ctx : HttpContext) ->
    task {
        use client = new HttpClient()
        let! response = client.GetStringAsync("https://api.open-meteo.com/v1/forecast?latitude=35.6895&longitude=139.6917&current_weather=true")
        let weatherData = JObject.Parse(response)
        return! json weatherData next ctx
    }

let webApp =
    choose [
        GET >=> choose [
            route "/todos" >=> getTodosHandler
            route "/weather" >=> getWeatherHandler
        ]
        POST >=> route "/todos" >=> addTodoHandler
    ]

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe(webApp)

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    let builder = WebApplication.CreateBuilder()
    builder.Services |> configureServices |> ignore
    let app = builder.Build()
    app |> configureApp
    app.Run()
    0
