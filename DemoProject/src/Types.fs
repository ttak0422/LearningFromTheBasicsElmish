module App.Types


open Global


type Msg =
    | CounterMsg of Counter.Types.Msg
    | FormMsg of Form.Types.Msg
    | HomeMsg of Home.Types.Msg

type Model =
    { CurrentPage : Page
      Counter : Counter.Types.Model
      Form : Form.Types.Model
      Home: Home.Types.Model }
