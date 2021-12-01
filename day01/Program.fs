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
        | current_depth :: remaining_depths ->
            detect_drop remaining_depths current_depth
            + if current_depth > previous_depth
                 && previous_depth <> 0 then
                  1
              else
                  0
        | [] -> 0

    printfn $"single drops: %d{detect_drop contents 0}"


    let windower depths =
        match depths with
        | d1 :: d2 :: d3 :: tail -> (d2 :: d3 :: tail, d1 + d2 + d3)
        | _ -> (List.empty, 0)

    let rec detect_window depths previous_window =
        match windower depths with
        | ([], _) -> 0
        | (new_depths, current_window) ->
            detect_window new_depths current_window
            + if current_window > previous_window
                 && previous_window <> 0 then
                  1
              else
                  0

    printfn $"window drops: %d{detect_window contents 0}"

    0
