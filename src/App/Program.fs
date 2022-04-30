open Library.Crawler

[<EntryPoint>]
let main args =
    printfn "0050 成份股："
    printfn "------------"

    craw "0050"
    |> Array.iter (fun stock -> printfn $"%s{stock.CommKey} %s{stock.CommName} = %.2f{stock.Weights}")

    0
