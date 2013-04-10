namespace Tabun.Statistics

module Parsing = 
        open System
        open FSharpx
        open Common
        open HtmlAgilityPack

        let value : HtmlNode option -> string option = Option.map Common.value
        let invariantParse s = Double.TryParse(s,Globalization.NumberStyles.Float,Globalization.CultureInfo.InvariantCulture)
        let fromTabunFloat (s:string) = 
            if s.StartsWith("+") then
                invariantParse <| s.Substring 1
            else 
                invariantParse s
        let toFloat = Option.joinedMap (Option.tryParseWith fromTabunFloat)
        let toDate = Option.joinedMap DateTime.parse
        let toYear (str:string) = 
            match str.Trim().Split(' ') with 
            | [|day;month;year|]  ->  
                let months = [| "января";"февраля";
                                "марта";"апреля";"мая";
                                "июня";"июля";"августа";
                                "сентября";"октября";"ноября";
                                "декабря" |]
                Option.maybe {
                    let! year = Int32.parse year
                    let! month = Array.tryFindIndex (fun x -> x = month) months
                    let! day = Int32.parse day
                    return new DateTime(year,month+1,day)
                }
            |_ -> None
        let sex = Option.joinedMap Sex.Parse
