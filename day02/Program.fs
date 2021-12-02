open System
open System.IO
open System.Text.RegularExpressions

[<EntryPoint>]
let main argv =

    let matchCommand input =
        let m =
            Regex.Match(input, @"(forward|up|down)\s+(\d+)")
        // matching count to a set value feels wrong, is there a more f#'y way to do this?
        match m.Groups.Count with
        | 3 -> Some(m.Groups.[1] |> string, m.Groups.[2].Value |> int)
        | _ -> None

    let commands =
        let path = Path.Combine(".", "input02.txt")

        File.ReadAllLines path
        |> Array.choose matchCommand
        |> List.ofArray


    let rec runCommands cmds horiz vert =
        match cmds with
        | [] -> printfn $"horizontal: %d{horiz} * %d{vert} = %d{horiz * vert}"
        | (direction: string, dist: int) :: tail ->
            match direction with
            | "forward" -> runCommands tail (horiz + dist) vert
            | "up" -> runCommands tail horiz (vert - dist)
            | "down" -> runCommands tail horiz (vert + dist)
            | _ -> printfn "error"

    runCommands commands 0 0

    let rec runCommandsWithAim cmds aim horiz vert =
        match cmds with
        | [] -> printfn $"horizontal: %d{horiz} * %d{vert} = %d{horiz * vert}"
        | (direction: string, dist: int) :: tail ->
            match direction with
            | "forward" -> runCommandsWithAim tail aim (horiz + dist) (vert + (aim * dist))
            | "up" -> runCommandsWithAim tail (aim - dist) horiz vert
            | "down" -> runCommandsWithAim tail (aim + dist) horiz vert
            | _ -> printfn "error"

    runCommandsWithAim commands 0 0 0

    0
