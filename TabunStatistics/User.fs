namespace Tabun.Statistics
open System
open HtmlAgilityPack
open FSharpx
open Common

type User = { 
                Nickname : string; 
                Power : float;
                Karma : float;
                Name : string option; 
                Birthdate : DateTime option;
                Age : int option;
                Sex: Sex option;
            }
with
    static member Parse (page : HtmlNode) = 
        Option.maybe {
            let! name = page /? "//h2[@itemprop=\"nickname\"]" |> Parsing.value
            let! power = page /? "//div[@class=\"strength\"]/div" |> Parsing.value |> Parsing.toFloat 
            let! karma = page /? "//div[@class=\"vote-item vote-count\"]/span" |> Parsing.value |> Parsing.toFloat 
            let profileNodes = page /! "//ul[@class=\"profile-dotted-list\"]"
            let privateNode,activityNode = 
                if profileNodes.Count = 2 then
                    profileNodes.Item 0,profileNodes.Item 1
                else
                    null, profileNodes.Item 0
            let birthdate = privateNode /? "li[2]/strong" |> Parsing.value |> Option.joinedMap Parsing.toYear
            let differenceWithNow (x:DateTime) = DateTime.Now.Year - x.Year
            return {
                Nickname = name;
                Power = power;
                Karma = karma;
                Name = page /? "//p[@itemprop=\"name\"]" |> Parsing.value;
                Birthdate = birthdate
                Age = Option.map differenceWithNow birthdate
                Sex = privateNode /? "li[1]/strong" |> Parsing.value |> Parsing.sex
            }
        }