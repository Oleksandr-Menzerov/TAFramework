Feature: Pet details
  As a user
  I want to view the details of a pet proposal
  So that I can verify that the information displayed on the UI matches the created proposal.

Background:
  Given I have created a proposal with random values via API

@API @UI
#@Retry(2)
Scenario: Open scpecific pet proposal
  When I open the pet details page
  Then I see the pet details
  Then the created proposal details should match the pet details on the UI
