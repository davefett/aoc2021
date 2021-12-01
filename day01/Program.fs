open System
open System.IO

[<EntryPoint>]
let main argv =

    let tryParseInt s =
        try
            s |> int |> Some
        with
        | :? FormatException -> None

    let contents =
        let path = Path.Combine(".", "input1.txt")

        File.ReadAllLines path
        |> Array.choose tryParseInt
        |> List.ofArray

    let rec detect_drop depths previous_depth =
        match depths with
        | [] -> 0
        | current_depth :: remaining_depths ->
            detect_drop remaining_depths current_depth
            + if current_depth > previous_depth
                 && previous_depth <> 0 then
                  1
              else
                  0

    printfn $"drops: %d{detect_drop contents 0}"

    0
