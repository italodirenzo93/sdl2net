// Learn more about F# at http://fsharp.org

open System
open System.Drawing
open SDL2Net
open SDL2Net.Input
open SDL2Net.Video

[<EntryPoint>]
let main argv =
    use app = new SDLApplication()
    use window = new Window("Hello from F#!", 400, 300, 800, 600)
    use renderer = new Renderer(window)
    use input = new InputSystem(app)
    
    app.OnUpdate <- fun () ->
        renderer.DrawColor <- Color.CornflowerBlue
        renderer.Clear()
        renderer.Present()
    
    app.Run()
    
    0 // return an integer exit code
