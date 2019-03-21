module Main

(*
    パースしない例(ナビゲーションもしてない)
    Elmishではパースとナビゲーションが密接な関係なのでこんなふうには書かない
    あまり良くないコードなのでHistoryAPIを使ったときの動きを見る以上のことはしないこと
*)

open Elmish
open Elmish.Browser.UrlParser
open Elmish.Browser.Navigation
open Elmish.React
open Fable.Import
open Fable.Helpers.React.Props
module R = Fable.Helpers.React



// MODEL
(* 形だけのモデル *)
type Page =
    | Home

let toPage = function
    | Home -> "/home"

type Model =
    { Page : Page }


// UPDATE
let update msg model =
    model, Cmd.none


// VIEW
let view model dispatch =
    let viewLinkInternal path =
        R.li []
            [ R.a [ Href path
                    OnClick ( fun e ->
                        e.preventDefault()
                        Browser.history.pushState ((), "", path) ) ]
                    [ R.str path ] ]
    let viewLinkExternal path =
        R.li [] [ R.a[ Href path ][ R.str path ] ]

    R.div []
        [ R.str "内部リンクを踏んでもサーバーへリクエストが行かない"
          R.ul []
              [ viewLinkInternal "/home"
                viewLinkInternal "/profile"
                viewLinkExternal "https://fable.io/"] ]


// NAVIGATE
let pageParser : Parser<Page->Page,Page> =
    oneOf []

let urlUpdate (result: Option<Page>) model =
    model, Cmd.none


let init result =
    let (model, cmd) = urlUpdate result { Page = Page.Home }
    model, cmd


Program.mkProgram init update view
|> Program.toNavigable (parsePath pageParser) urlUpdate
|> Program.withReact "elmish-app"
|> Program.run