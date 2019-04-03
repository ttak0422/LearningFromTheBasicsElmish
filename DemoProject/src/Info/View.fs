module Info.View


open Fable.Helpers.React.Props
module R = Fable.Helpers.React


let root =
  R.div
    [ ClassName "content" ]
    [ R.h1
        [ ]
        [ R.str "About page" ]
      R.p
        [ ]
        [ R.str "ElmもいいけどElmishもいいぞ!"
          R.br []
          R.str "随時更新中!!" ]]
