namespace Tabun.Statistics


type Sex = Man | Woman
with
    static member Parse (str:string) = 
        match str.Trim() with
        |"мужской" -> Some Man
        |"женский" -> Some Woman
        | _ -> None
    override this.ToString() = 
        match this with
        |Man -> "М"
        |Woman -> "Ж"