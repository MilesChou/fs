module Library.Crawler

open System.Net.Http
open System.Text
open System.Text.Json


type Stock =
    { Date: string
      CommKey: string
      CommName: string
      Type: string
      Weights: double
      Value: int
      Unit: string
      Amount: double
      Currency: string }

type Root = { Data: array<Stock>; Date: string }

let client: HttpClient = new HttpClient()

let createRequest id =
    let request: HttpRequestMessage =
        new HttpRequestMessage(HttpMethod.Post, "https://www.cmoney.tw/etf/ashx/e210.ashx")

    request.Content <-
        new StringContent(
            $"action=GetShareholdingDetails&stockId={id}",
            Encoding.UTF8,
            "application/x-www-form-urlencoded"
        )

    request

let craw id =
    let request = createRequest (id)

    let response =
        client.SendAsync(request)
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let stream =
        response
            .EnsureSuccessStatusCode()
            .Content.ReadAsStringAsync()

    let json =
        stream
        |> Async.AwaitTask
        |> Async.RunSynchronously

    JsonSerializer.Deserialize<Root>(json).Data
