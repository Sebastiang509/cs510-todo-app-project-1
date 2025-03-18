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
    let builder = WebApplication.CreateBuilder()
    builder.Services |> configureServices |> ignore
    let app = builder.Build()
    app |> configureApp
    app.Run()
    0
