Feature: Crud Proposals
    As a user,
    I want to be sure
    that I can create, update, and delete proposals
    so that I can manage pet proposals efficiently.

@API
Scenario: Create a new proposal
    As a user,
    I want to create a new proposal
    so that I can add it to the available proposals.
  Given I have created a proposal with the following details
    | Title                 | PetName | Prop[Вид тварини] | Prop[Різновид]    | Prop[Стать] | Price | Location | Age | AgeUnits | Summary                 |
    | Cute Dog needs family | Сірко   | Собаки            | Лабрадор-ретрівер | Хлопчик     | 100   | Харків   | 120 | 30       | Search for home for Rex |
  Then the proposal should be created successfully
  And the proposal details should match the input

@API
Scenario: Create a new proposal with random values
    As a user,
    I want to create a proposal with random values
    so that I can quickly test proposal creation with various data.
  Given I have created a proposal with random values
    | Title            | PetName           | Prop[Вид тварини, Різновид] | Prop[Стать]    | Price                 | Location     | Age | AgeUnits | Summary          |
    | Random[Sentence] | Random[FirstName] | Random[Specie,Breed]        | Random[Gender] | Random[Double(1,100)] | Random[City] | 365 | 365      | Random[Sentence] |
  Then the proposal should be created successfully
  And the proposal details should match the input

@API
Scenario: Update a proposal
    As a user,
    I want to update an existing proposal
    so that I can modify the details of a pet proposal.
  Given I have created a proposal with the following details
    | Title                    | PetName | Prop[Вид тварини] | Prop[Різновид] | Prop[Стать] | Price | Location | Age | AgeUnits | Summary                  |
    | Cute alpaca needs family | Alpi    | Інші              | Альпака        | Дівчинка    | 100   | New York | 365 | 365      | Search for home for Alpi |
  When I update the proposal with the following details
    | Title                  | PetName | Prop[Вид тварини] | Prop[Різновид] | Prop[Стать] | Price | Location   | Age | AgeUnits | Summary                   |
    | Cool lama needs family | Lemmy   | Інші              | Лама           | Хлопчик     | 1000  | Washington | 120 | 30       | Search for home for Lemmy |
  Then the proposal should be updated successfully
  And the proposal details should match the updated input

@API
Scenario: Delete a proposal
    As a user,
    I want to delete a proposal
    so that I can remove it from the available listings.
  Given I have created a proposal with the following details
    | Title                      | PetName | Prop[Вид тварини] | Prop[Різновид] | Prop[Стать] | Price | Location | Age | AgeUnits | Summary                   |
    | Cute fish sturved for love | Pan-pan | Риби              | Сом-панда      | Невідомо    | 80    | Chicago  | 365 | 365      | Search for home for fishy |
  When I delete the proposal
  Then the proposal should be deleted successfully
