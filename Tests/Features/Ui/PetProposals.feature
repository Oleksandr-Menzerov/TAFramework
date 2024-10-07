Feature: Pet proposals
  As a user
  I want to browse and sort pet proposals
  So that I can find the right pet and view its details.

Background:
  And I am on the main page

@UI
Scenario: Open the first pet proposal
  As a user
  I want to open a pet proposal
  So that I can see the detailed information about the pet
  When I click on the 'Оголошення' link
  Then I see the proposals
  When I click on the 1 pet card
  Then I see the pet details

@UI
Scenario: Sort proposals
  As a user
  I want to sort pet proposals by price
  So that I can easily compare pets within my budget
  When I click on the 'Оголошення' link
  Then I see the proposals
  When I sort proposals 'Від дешевих до дорогих'
  Then proposals should be sorted by 'Price' descending
  When I sort proposals 'Від дорогих до дешевих'
  Then proposals should be sorted by 'Price' ascending
