- [ ] Fix!!!: get name by species instead of by pokemon
  - ER: Pumpkaboo | AR: Pumpkaboo-average

- [ ] Add!!!: domain Relating is done by relationPolicy: p1.RelateWith(p2) --> strategy.Relate(p1, p2)
- [ ] Refactor: logic in controllers id done by domain: Choice, Result, etc.

- [ ] float relation values are truncated always the same way
  - AR: in first round, 216 in card result became 217 in total result...

- [-] Add: fake repo with cached pkmns
- [x] Add: repo cache!!!
- [x] Refactor: visual repo gets pararelly sprite and pkmn info
- [ ] Add: url fallbacks in case first one has not the requested pkmn

- [ ] Add: repo get by constructor a range of Id where to randomize from
- [ ] Refactor: IController
- [ ] Refactor: PkmnCard alpha control to view component "fadeable canvas group" or whatnot
- [ ] Add: Show/Hide slots panel when ContinueWithInMainThread it's available
  - SetActive() does not work when on threading...

- [ ] Add: name tags colored by pkmn type
  - If two types, maybe gradient?
- [ ] Fix!!!: result counter goes backwards instead of forwards