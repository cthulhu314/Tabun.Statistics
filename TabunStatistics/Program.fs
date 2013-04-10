namespace Tabun.Statistics

module Main = 
    open Tabun.Statistics
    open FSharpx
    open System

    let (|IsInt|_|) = Int32.parse

    [<EntryPoint>]
    let main argv = 
        
        let printUsers min max = 
            Console.WriteLine("{0},{1},{2},{3}","Age","Sex","Karma","Power")
            for maybeUser in Tabun.getUsersFrom [min..max] do 
                Option.maybe {
                    let! user = maybeUser
                    let! sex = user.Sex
                    let! age = user.Age
                    let karma = user.Karma.ToString(Globalization.CultureInfo.InvariantCulture)
                    let power = user.Power.ToString(Globalization.CultureInfo.InvariantCulture)
                    do Console.WriteLine("{0},{1},{2},{3}",age,sex,karma,power)
                } |> ignore

        match argv with
        |[| |] ->
            do printUsers 1 10
            0
        |[| IsInt t |] ->
            do printUsers 1 t
            0
        |[| IsInt f ; IsInt t |] ->
            do printUsers f t
            0
        | _ -> 1
