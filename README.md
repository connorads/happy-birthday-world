# Happy Birthday World

Get a personalised birthday countdown and happy birthday message 🎂

## Demo

Running on my Google Cloud Platform, please don't hammer it 😅

Environment  | Url           
-----------  |  -----------------------------------------------------------  |
Staging      | TBD
Production   | TBD

## Assumptions

Here are some assumptions that need to be validated by the "Product Owner" 👀

* Leap-year babies are born on February 29th. We currently assume a birthday of February 28th on non-leap years for leap-year babies. But some "leapers" celebrate on March 1st [*[Wikipedia]*](https://bit.ly/2ENDhFe)
* Dates of Birth in the future are invalid. We perform validation to ensure that the Date of Births entered on the API are not in the future because otherwise it's difficult to define the expected behaviour.
