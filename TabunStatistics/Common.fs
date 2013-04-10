namespace Tabun.Statistics

module Common = 
    open HtmlAgilityPack

    module Option = 
        let join x = 
            match x with
            | None -> None
            | Some x -> x  
                  
        let joinedMap f = function
            | Some arg -> 
                match f arg with
                |Some x -> Some x
                |None -> None
            | None -> None

    let (/) (node : HtmlNode) (xpath : string) = node.SelectSingleNode(xpath)

    let (/?) node xpath = 
        if node=null then
            None
        else
            match node / xpath with
            |null -> None
            |result -> Some result

    let selectSingleNode (xpath : string) (node : HtmlNode) = node.SelectSingleNode(xpath)

    let (/!) (node : HtmlNode) (xpath : string) = node.SelectNodes(xpath)

    let (/!?) node xpath = 
        if node=null then
            None
        else
            match node /! xpath with
            |null -> None
            |result when result.Count>0 -> Some result
            | _ -> None

    let selectNodes (xpath : string) (node : HtmlNode) = node.SelectNodes(xpath)

    let value (node : HtmlNode) = node.InnerText

    let attr (name: string) (node :  HtmlNode) = node.GetAttributeValue(name,"")