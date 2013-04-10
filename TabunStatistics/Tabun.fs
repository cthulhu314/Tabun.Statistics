namespace Tabun.Statistics
module Tabun = 
    open System
    open HtmlAgilityPack
    open Common
    open FSharpx

    let getUsersFrom pages = 
        let client = new HtmlWeb()

        let getPage (i:int) = 
           let address = String.Format("http://tabun.everypony.ru/people/index/page{0}/?order=user_rating&order_way=desc",i)
           try 
               let page = client.Load address
               Some page.DocumentNode
           with 
           | e -> None

        let getUsersPagesFrom (page : HtmlNode) = 
            Option.maybe {
                let! usersUrls = page /!? "//div[@class=\"name no-realname\"]/p/a" 
                return  Seq.map (attr "href") usersUrls
            }

        let getUser (address:string) = 
            try
                let page = client.Load address
                Some page.DocumentNode
            with
            | e -> None

        let toSeq = function
            |Some seq -> seq
            |None -> Seq.empty

        pages 
        |> Seq.collect (getPage >> Option.joinedMap getUsersPagesFrom >> toSeq)
        |> Seq.map (getUser >> Option.joinedMap User.Parse)


