module TodoService

open Giraffe
open Microsoft.AspNetCore.Http

let mutable todos = ["Finish CS 510 project"; "Submit assignment"]

let getTodosHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        json todos next ctx

let addTodoHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! todo = ctx.BindJsonAsync<string>()
            todos <- todo :: todos
            return! json todos next ctx
        }
