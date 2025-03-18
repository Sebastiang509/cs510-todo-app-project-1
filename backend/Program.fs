open System
open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection

let configureApp (app: IApplicationBuilder) =
    app.UseGiraffe(Routes.webApp)

let configureServices (services: IServiceCollection) =
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    printfn "Starting CS510 Todo App..."
    let builder = WebApplication.CreateBuilder()
    builder.Services |> configureServices |> ignore
    let app = builder.Build()
    printfn "Server running at http://localhost:5000"
    app |> configureApp
    app.Run()
