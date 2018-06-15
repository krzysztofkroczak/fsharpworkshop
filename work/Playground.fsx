#load "Work.fs" 

open Work

let c1= {Id=1; IsVip=false; Credit=30M}
let customer = increaseCredit (fun c->c.IsVip) c